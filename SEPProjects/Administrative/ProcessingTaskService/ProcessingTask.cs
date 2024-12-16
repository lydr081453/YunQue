using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using System.Data;

namespace ProcessingTaskService
{
    /// <summary>
    /// 任务处理操作类
    /// </summary>
    public class ProcessingTask
    {
        #region 局部变量定义
        /// <summary>
        /// 日志记录对象
        /// </summary>
        private LogManager logger = new LogManager();
        #endregion

        public ProcessingTask()
        { 
        }

        /// <summary>
        /// 开始任务处理
        /// </summary>
        public void StartProcessingTask()
        {
            try
            {
                DataSet tasksds = GetWaitingTasks();
                if (tasksds != null && tasksds.Tables.Count > 0 && tasksds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in tasksds.Tables[0].Rows)
                    {
                        // 门禁系统中开通门禁或禁止门禁
                        if (int.Parse(dr["TaskType"].ToString()) == (int)TaskType.EnableCard)
                        {
                            DredgeTask(dr);
                        }
                        else if (int.Parse(dr["TaskType"].ToString()) == (int)TaskType.UnEnableCard)
                        {
                            DisableTask(dr);
                        }
                        // 考勤系统中标识该任务信息为无效的
                        ClearWaitingTasks(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Add("ProcessingTaskService：开通门禁权限时，系统出现异常, " + ex.Message + "/" + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 获得待处理任务信息
        /// </summary>
        /// <returns>返回一个待处理任务信息集合</returns>
        public DataSet GetWaitingTasks()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ADSqlConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT * FROM AD_WaitingTask WHERE Deleted=0 ORDER BY cardno ASC";
                    // 连接打卡服务器
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    adapter.SelectCommand = cmd;
                    // 读取打卡记录信息
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return ds;
        }

        #region 门禁系统中处理门卡权限
        /// <summary>
        /// 处理启用门卡任务
        /// </summary>
        public void DredgeTask(DataRow dr)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connsource"].ConnectionString))
            {
                DataSet ds = new DataSet();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 获得最大的ConsumerNo编号
                    string sql = "SELECT MAX(f_consumerno) FROM t_b_consumer ";
                    // 连接打卡服务器
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;

                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // 获得最大的ConsumerNO值
                        int ConsumerNO = int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1;
                        ds.Clear();

                        #region 插入用户信息
                        StringBuilder strsql = new StringBuilder();
                        strsql.Append("INSERT INTO t_b_Consumer(");
                        strsql.Append("f_ConsumerNO,f_ConsumerName,f_ConsumerGrade,f_GroupID,f_AttendEnabled,f_DoorEnabled,f_BeginYMD,f_EndYMD,f_Password,");
                        strsql.Append("f_PhotoPath,f_Note,f_PatrolEnabled,f_WorkNo,f_Title,f_Culture,f_Hometown,f_Birthday,f_Marriage,f_JoinDate,f_LeaveDate,");
                        strsql.Append("f_CertificateType,f_CertificateID,f_SocialInsuranceNo,f_Addr,f_Postcode,f_Sex,f_Nationality,f_Religion,f_EnglishName,");
                        strsql.Append("f_Mobile,f_HomePhone,f_Telephone,f_Email,f_Political,f_CorporationName,f_TechGrade,f_bShift,O_Carenum)");
                        strsql.Append(" VALUES( ");
                        strsql.Append(ConsumerNO + ",");
                        strsql.Append("'" + dr["UserName"].ToString() + "',");
                        strsql.Append("null, 0, 1, 1,");
                        strsql.Append("'" + DateTime.Now.ToString("yyyy-MM-dd") + "',");
                        strsql.Append("'2030-12-31 0:00:00',");
                        strsql.Append("'123456',");
                        strsql.Append("null,null, 0,");
                        strsql.Append("'" + dr["UserCode"].ToString() + "',");
                        strsql.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL);");

                        cmd.CommandText = strsql.ToString();
                        cmd.ExecuteNonQuery();
                        #endregion

                        #region 获得用户编号
                        int consumerId = 0;
                        string sqlConsumerId = "select CAST(@@IDENTITY AS int)";

                        cmd.CommandText = sqlConsumerId.ToString();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            consumerId = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);
                        }
                        reader.Close();
                        #endregion

                        #region 插入门卡号信息
                        StringBuilder sqlCardInfo = new StringBuilder();
                        sqlCardInfo.Append("INSERT INTO t_b_IDCard(f_CardNO,f_CardStatusDesc,f_ConsumerID,f_IssueDate,f_Deadline,f_LostDate,f_DeletedDate,f_StatusChangeDate) VALUES (");
                        sqlCardInfo.Append("'" + dr["CardNo"].ToString() + "',");
                        sqlCardInfo.Append("0,");
                        sqlCardInfo.Append(consumerId + ",");   // 用户信息编号
                        sqlCardInfo.Append("NULL,NULL,NULL,NULL,NULL);");

                        cmd.CommandText = sqlCardInfo.ToString();
                        cmd.ExecuteNonQuery();
                        #endregion

                        #region 插入门卡权限信息
                        StringBuilder sqlPrivilege = new StringBuilder();
                        if (dr["AreaID"].ToString() == "1")    // 等于1表示是北京用户
                        {
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("1,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("2,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("3,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("4,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("5,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("6,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("7,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("8,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("9,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("10,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("11,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("14,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("15,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("16,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("17,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("18,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("19,1," + consumerId + ");");
                        }
                        else if (dr["AreaID"].ToString() == "2")    // 等于2表示是上海用户
                        {
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("20,1," + consumerId + ");");
                        }
                        else if (dr["AreaID"].ToString() == "3")    // 等于3表示是广州用户
                        {
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("21,1," + consumerId + ");");
                        }
                        else
                        {
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("1,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("2,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("3,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("4,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("5,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("6,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("7,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("8,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("9,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("10,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("11,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("14,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("15,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("16,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("17,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("18,1," + consumerId + ");");
                            sqlPrivilege.Append("INSERT INTO t_d_Privilege(f_DoorID,f_ControlSegID,f_ConsumerID) VALUES(");
                            sqlPrivilege.Append("19,1," + consumerId + ");");
                        }
                        cmd.CommandText = sqlPrivilege.ToString();
                        cmd.ExecuteNonQuery();
                        
                        #endregion
                        trans.Commit();
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                // 上传门卡信息
                this.UploadPurview(dr);
                logger.Add("ProcessingTaskService：" + dr["UserName"].ToString() + "（" + dr["UserCode"].ToString() + "|" + dr["CardNo"].ToString() + "）的门卡开通成功。");
            }
        }

        /// <summary>
        /// 处理停用门卡任务
        /// </summary>
        /// <param name="dr"></param>
        public void DisableTask(DataRow dr)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connsource"].ConnectionString))
            {
                DataSet ds = new DataSet();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 通过员工编号获得用户编号
                    string sql = "SELECT f_ConsumerID FROM t_b_Consumer WHERE f_Workno='" + dr["UserCode"].ToString() + "'";
                    // 连接打卡服务器
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;

                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // 获得用户编号
                        int ConsumerId = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                        ds.Clear();

                        #region 删除门卡信息
                        StringBuilder sqlCardInfo = new StringBuilder();
                        sqlCardInfo.Append("DELETE FROM t_b_IDCard WHERE f_ConsumerID=" + ConsumerId + ";");

                        cmd.CommandText = sqlCardInfo.ToString();
                        cmd.ExecuteNonQuery();
                        #endregion

                        #region 插入门卡权限信息脚本
                        StringBuilder sqlPrivilege = new StringBuilder();
                        sqlPrivilege.Append("DELETE FROM t_d_Privilege WHERE f_ConsumerID=" + ConsumerId + ";");
                        
                        cmd.CommandText = sqlPrivilege.ToString();
                        cmd.ExecuteNonQuery();
                        #endregion

                        #region 删除用户信息
                        StringBuilder strsql = new StringBuilder();
                        strsql.Append("DELETE FROM t_b_Consumer WHERE f_ConsumerID=" + ConsumerId + ";");

                        cmd.CommandText = strsql.ToString();
                        cmd.ExecuteNonQuery();
                        #endregion
                        trans.Commit();

                        // 清理门卡信息
                        this.ClearPurview(dr);
                        logger.Add("ProcessingTaskService：" + dr["UserName"].ToString() + "（" + dr["UserCode"].ToString() + "|" + dr["CardNo"].ToString() + "）的门卡停用成功");
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 上传门卡权限
        /// </summary>
        public void UploadPurview(DataRow dr)
        {
            if (dr["AreaID"].ToString() == "1")    // 等于1表示是北京用户
            {
                for (int i = 0; i <= 8; i++)
                {
                    for (int j = 1; j <= ControllerDoors[i]; j++)
                    {
                        AddPurview(SerialNumber[i], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[i]);
                    }
                }
            }
            else if (dr["AreaID"].ToString() == "2")    // 等于2表示是上海用户
            {
                for (int j = 1; j <= ControllerDoors[9]; j++)
                {
                    AddPurview(SerialNumber[9], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[9]);
                }
            }
            else if (dr["AreaID"].ToString() == "3")    // 等于3表示是广州用户
            {
                for (int j = 1; j <= ControllerDoors[10]; j++)
                {
                    AddPurview(SerialNumber[10], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[10]);
                }
            }
            else
            {
                for (int i = 0; i <= 8; i++)
                {
                    for (int j = 1; j <= ControllerDoors[i]; j++)
                    {
                        AddPurview(SerialNumber[i], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 清除门卡信息
        /// </summary>
        /// <param name="dr"></param>
        public void ClearPurview(DataRow dr)
        {
            if (dr["AreaID"].ToString() == "1")    // 等于1表示是北京用户
            {
                for (int i = 0; i <= 8; i++)
                {
                    for (int j = 1; j <= ControllerDoors[i]; j++)
                    {
                        DeletePurview(SerialNumber[i], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[i]);
                    }
                }
            }
            else if (dr["AreaID"].ToString() == "2")    // 等于2表示是上海用户
            {
                for (int j = 1; j <= ControllerDoors[9]; j++)
                {
                    DeletePurview(SerialNumber[9], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[9]);
                }
            }
            else if (dr["AreaID"].ToString() == "3")    // 等于3表示是广州用户
            {
                for (int j = 1; j <= ControllerDoors[10]; j++)
                {
                    DeletePurview(SerialNumber[10], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[10]);
                }
            }
            else
            {
                for (int i = 0; i <= 8; i++)
                {
                    for (int j = 1; j <= ControllerDoors[i]; j++)
                    {
                        DeletePurview(SerialNumber[i], int.Parse(dr["CardNo"].ToString()), j, ControllersIp[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 添加门卡权限
        /// </summary>
        /// <param name="serialNumber">产品序列号</param>
        /// <param name="cardno">门卡号</param>
        /// <param name="doorNo">门号</param>
        /// <param name="ipAddr">控制器IP</param>
        public void AddPurview(long serialNumber, long cardno, int doorNo, string ipAddr)
        {
            WComm_UDP.WComm_Operate wudp = new WComm_UDP.WComm_Operate();
            long controllerSN = serialNumber;
            string strCmd, strFrame;
            string privilege = wudp.CardToStrHex(cardno);             //卡号
            privilege += wudp.NumToStrHex(doorNo, 1);                 //门号
            privilege += wudp.MSDateYmdToWCDateYmd("2009-07-01"); //起始日期
            privilege += wudp.MSDateYmdToWCDateYmd("2030-12-31");     //结束日期
            privilege += wudp.NumToStrHex(1, 1);                      //时段
            privilege += wudp.NumToStrHex(123456, 3);                 //密码
            privilege += wudp.NumToStrHex(0, 4);                      //备用
            // 添加修改权限
            strCmd = wudp.CreateBstrCommand(controllerSN, "0711" + wudp.NumToStrHex(0, 2) + privilege);
            strFrame = wudp.udp_comm(strCmd, ipAddr, 60000);
        }
        
        /// <summary>
        /// 清理门卡权限
        /// </summary>
        public void DeletePurview(long serialNumber, long cardno, int doorNo, string ipAddr)
        {
            WComm_UDP.WComm_Operate wudp = new WComm_UDP.WComm_Operate();
            long controllerSN = serialNumber;
            string strCmd, strFrame;
            string privilege = wudp.CardToStrHex(cardno);             //卡号
            privilege += wudp.NumToStrHex(doorNo, 1);                 //门号
            privilege += wudp.MSDateYmdToWCDateYmd("2009-07-01");     //起始日期
            privilege += wudp.MSDateYmdToWCDateYmd("2030-12-31");     //结束日期
            privilege += wudp.NumToStrHex(1, 1);                      //时段
            privilege += wudp.NumToStrHex(123456, 3);                 //密码
            privilege += wudp.NumToStrHex(0, 4);                      //备用
            //删除权限
            strCmd = wudp.CreateBstrCommand(controllerSN, "0811" + wudp.NumToStrHex(0, 2) + privilege);
            strFrame = wudp.udp_comm(strCmd, ipAddr, 60000);
        }

        #region 控制器信息
        /**
         * 控制器信息
         * 编号：1   产品序列号：37356  IP：172.16.4.2   控制通道：13层A区南门            13层B区南门         权限：北京全体员工
         * 编号：2   产品序列号：31266  IP：172.16.4.11  控制通道：13层B区东北通道门南门  13层B区西北通道门   权限：北京全体员工
         * 编号：3   产品序列号：51574  IP：172.16.4.3   控制通道：13层A区北门                                权限：北京全体员工
         * 编号：4   产品序列号：31262  IP：172.16.4.4   控制通道：13层C区南门            13层C区北门         权限：北京全体员工
         * 编号：5   产品序列号：37358  IP：172.16.4.8   控制通道：12层A区北门            12层B区东北通道门   权限：北京全体员工
         * 编号：6   产品序列号：37357  IP：172.16.4.9   控制通道：12层A区南门            12层B区南门         权限：北京全体员工
         * 编号：8   产品序列号：37364  IP：172.16.4.6   控制通道：14层C区北门            14层B区西北通道门   权限：北京全体员工
         * 编号：9   产品序列号：37278  IP：172.16.4.5   控制通道：14层A区北门            14层B区东北通道门   权限：北京全体员工
         * 编号：10  产品序列号：37363  IP：172.16.4.7   控制通道：14层A区南门            14层B区南门         权限：北京全体员工
         * 编号：11  产品序列号：55578  IP：192.168.0.110  控制通道：上海分公司大门                           权限：上海分公司全体员工
         * 编号：12  产品序列号：55553  IP：192.168.1.110  控制通道：广州分公司大门                           权限：广州分公司全体员工
         */
        #endregion

        /// <summary>
        /// 控制器IP
        /// </summary> 
        private string[] ControllersIp
        {
            get
            {
                return new string[] { 
                    "172.16.4.2", "172.16.4.11", "172.16.4.3", "172.16.4.4", "172.16.4.8", "172.16.4.9",
                    "172.16.4.6", "172.16.4.5", "172.16.4.7", "192.168.0.110", "192.168.1.110"
                };
            }
        }

        /// <summary>
        /// 产品序列号
        /// </summary>
        private long[] SerialNumber
        {
            get
            {
                return new long[] { 
                    37356, 31266,51574,31262,37358,37357,37364,37278,37363,55578,55553
                };
            }
        }

        /// <summary>
        /// 控制器的门禁数量
        /// </summary>
        public int[] ControllerDoors
        {
            get
            {
                return new int[] { 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 1 };
            }
        }
        #endregion


        #region 考勤系统处理任务信息
        /// <summary>
        /// 清理考勤系统中任务信息内容
        /// </summary>
        /// <param name="dr"></param>
        public void ClearWaitingTasks(DataRow dr)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ADSqlConnection"].ConnectionString))
            {
                conn.Open();
                try
                {
                    string sql = "DELETE AD_WaitingTask WHERE id=" + dr["ID"].ToString();
                    // 连接打卡服务器
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion
    }
}
