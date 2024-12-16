using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWorkFlowDAO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using WorkFlow.Model;
using WorkFlowLibary;
using System.Data;

namespace WorkFlowDAO
{
    public class ProcessInstanceDao : IProcessInstanceDao
    {
        public int update_process_state(long instanceid, int state)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string statecmd = "select PROCESSINSTANCESTATE from we_processinstances where INSTANCEID=" + instanceid + "";
            DbCommand dbCommand = db.GetSqlStringCommand(statecmd);
            int currentstate = Convert.ToInt32(db.ExecuteScalar(dbCommand));

            string sqlCommand;
            if (currentstate == WfStateContants.PROCESS_SUSPENDED)
            {
                sqlCommand = "update we_PROCESSINSTANCES set PROCESSINSTANCESTATE=previousstate,ENDDATE=getdate() where INSTANCEID=" + instanceid + "";
            }
            else
            {
                sqlCommand = "update we_PROCESSINSTANCES set previousstate=PROCESSINSTANCESTATE,PROCESSINSTANCESTATE=" + state + ",ENDDATE=getdate() where INSTANCEID=" + instanceid + "";
            }

            dbCommand = db.GetSqlStringCommand(sqlCommand);
            return db.ExecuteNonQuery(dbCommand);
        }

        public int update_workitem_state(long instanceid, int state)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string statecmd = "select [state] from we_workitems where workitemid=" + instanceid + "";
            DbCommand dbCommand = db.GetSqlStringCommand(statecmd);
            int currentstate = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            string sqlCommand;
            if (currentstate == WfStateContants.TASKSTATE_SUSPENDED)
            {
                sqlCommand = "update we_workitems set [state]=previousstate where workitemid=" + instanceid + "";
            }
            else
            {
                sqlCommand = "update we_workitems set previousstate=[state],[state]=" + state + " where workitemid=" + instanceid + "";
            }
            dbCommand = db.GetSqlStringCommand(sqlCommand);
            return db.ExecuteNonQuery(dbCommand);

        }

        public int save_processinstance(PROCESSINSTANCES p)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand;
            string sqlCommand = "insert into we_processinstances(processid,processname,initiatorid,initiatorname,initiatorname2,startdate," +
                                "parentprocessinstanceid,activeworkitemid,notifyparentprocess,parentaddress,processinstancestate,previousstate,activepersonid) " +
                                "values(" + p.PROCESSID + ",'" + p.PROCESSNAME + "'," + p.INITIATORID + ",'" + p.INITIATORNAME + "','" + p.INITIATORNAME2 + "','" + p.STARTDATE + "'," + p.PARENTPROCESSINSTANCEID + "" +
                                "," + p.ACTIVEWOEKITEMID + ",'" + p.NOTIFYPARENTPROCESS + "','" + p.PARENTADDRESS + "'," + p.PROCESSINSTANCESTATE + "," + p.PROCESSINSTANCESTATE + "," + p.ACTIVEPERSONID + ")";
            dbCommand = db.GetSqlStringCommand(sqlCommand);
            if (db.ExecuteNonQuery(dbCommand) == 1)
            {
                sqlCommand = "select instanceid from we_processinstances where processid=" + p.PROCESSID + " and processname='" + p.PROCESSNAME + "' and startdate='" + p.STARTDATE + "'";
                dbCommand = db.GetSqlStringCommand(sqlCommand);
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            else
                return 0;

        }

        public int save_workitem(WORKITEMS w)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "insert into we_workitems(instanceid,processid,processname,taskid,taskname,taskdisplayname,taskinstructions,participantid,participantname,participantname2,startdate," +
                                "[state],previousstate,childinstanceid,participanttype,remindercount,serialpointer,userdrivenflag,userdrivenpersonid,tasktype,roleid) values(" +
                                "" + w.PROCESSINSTANCES.Id + "," + w.PROCESSID + ",'" + w.PROCESSNAME + "'," + w.TASKID + ",'" + w.TASKNAME + "','" + w.TASKDISPLAYNAME + "'," +
                                "'" + w.TASKINSTRUCTIONS + "'," + w.PARTICIPANTID + ",'" + w.PARTICIPANTNAME + "','" + w.PARTICIPANTNAME2 + "','" + w.STARTDATE + "'," +
                                "" + w.STATE + "," + w.STATE + "," + w.CHILDINSTANCEID + "," + w.PARTICIPANTTYPE + "," + w.ReminderCount + "," + w.SERIALPOINTER + "," + w.UserDrivenFlag + "," + w.UserDrivenPersonID + "," + w.TASKTYPE + ",'" + w.RoleID + "')";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            int workitemid = 0;
            if (db.ExecuteNonQuery(dbCommand) == 1)
            {
                workitemid = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, "select workitemid from we_workitems where instanceid=" + w.PROCESSINSTANCES.Id + " and taskname='" + w.TASKNAME + "'"));

                for (int i = 0; i < w.PREVIOUSWORKITEMSs.Count; i++)
                {
                    sqlCommand = "insert into we_previousworkitems(INSTANCEID,WORKITEMID,PREVIOUWORKITEMID,PREPARTICIPANTID,PREPARTICIPANTNAME,PREPARTICIPANTNAME2,PREVIOUSTASKNAME) " +
                                 "values(" + w.PROCESSINSTANCES.Id + "," + workitemid + "," + w.PREVIOUSWORKITEMSs[i].PREVIOUSWORKITERMID + "," + w.PREVIOUSWORKITEMSs[i].PREPARTICIPANTID + "," +
                                 "'" + w.PREVIOUSWORKITEMSs[i].PREPARTICIPANTNAME + "','" + w.PREVIOUSWORKITEMSs[i].PREPARTICIPANTNAME2 + "','" + w.PREVIOUSWORKITEMSs[i].PREVIOUSTASKNAME + "')";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    db.ExecuteNonQuery(dbCommand);
                }

            }
            return workitemid;

        }

        public int update_workitem(WORKITEMS w)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "update we_workitems set instanceid=" + w.INSTANCEID + ",processid=" + w.PROCESSID + ",processname='" + w.PROCESSNAME + "',taskid=" + w.TASKID + "," +
                                "taskname='" + w.TASKNAME + "',taskdisplayname='" + w.TASKDISPLAYNAME + "',taskinstructions='" + w.TASKINSTRUCTIONS + "',participantid=" + w.PARTICIPANTID + "," +
                                "participantname='" + w.PARTICIPANTNAME + "',participantname2='" + w.PARTICIPANTNAME2 + "',startdate='" + w.STARTDATE + "'," +
                                "enddate='" + w.ENDDATE + "',[state]=" + w.STATE + ",childinstanceid=" + w.CHILDINSTANCEID + ",participanttype=" + w.PARTICIPANTTYPE + "," +
                                "remindercount=" + w.ReminderCount + ",serialpointer=" + w.SERIALPOINTER + ",userdrivenflag=" + w.UserDrivenFlag + ",userdrivenpersonid=" + w.UserDrivenPersonID + ",roleid='" + w.RoleID + "' where " +
                                "workitemid=" + w.Id + "";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            return db.ExecuteNonQuery(dbCommand);
        }

        public PROCESSINSTANCES load_processinstance(string instanceid)
        {
            PROCESSINSTANCES p = new PROCESSINSTANCES();

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "select processid,processname,initiatorid,initiatorname,initiatorname2,startdate,enddate," +
                                "parentprocessinstanceid,activeworkitemid,notifyparentprocess,parentaddress,processinstancestate,activepersonid from we_processinstances where instanceid=" + instanceid + "";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
            while (reader.Read())
            {
                p.Id = Convert.ToInt64(instanceid);
                p.PROCESSID = Convert.ToInt64(reader.GetValue(0));
                p.PROCESSNAME = reader.GetValue(1).ToString();
                p.INITIATORID = Convert.ToInt64(reader.GetValue(2));
                p.INITIATORNAME = reader.GetValue(3).ToString();
                p.INITIATORNAME2 = reader.GetValue(4).ToString();
                p.STARTDATE = Convert.ToDateTime(reader.GetValue(5));

                if (reader.GetValue(6) is System.DBNull)
                    p.ENDDATE = Convert.ToDateTime("0001-01-01");
                else
                    p.ENDDATE = Convert.ToDateTime(reader.GetValue(6));

                p.PARENTPROCESSINSTANCEID = Convert.ToInt64(reader.GetValue(7));
                p.ACTIVEWOEKITEMID = Convert.ToInt64(reader.GetValue(8));
                p.NOTIFYPARENTPROCESS = Convert.ToInt64(reader.GetValue(9));
                p.PARENTADDRESS = reader.GetValue(10).ToString();
                p.PROCESSINSTANCESTATE = Convert.ToInt64(reader.GetValue(11));
                p.ACTIVEPERSONID = Convert.ToInt64(reader.GetValue(12));


            }
            reader.Close();


            sqlCommand = "select workitemid,instanceid,processid,processname,taskid,taskdisplayname,taskinstructions,participantid,participantname," +
                         "taskname,startdate,enddate,state,childinstanceid,participanttype,tasktype from we_workitems where instanceid=" + instanceid + "";

            dbCommand = db.GetSqlStringCommand(sqlCommand);
            reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);

            while (reader.Read())
            {
                WORKITEMS wi = new WORKITEMS();
                wi.Id = Convert.ToInt64(reader.GetValue(0));
                wi.INSTANCEID = Convert.ToInt64(instanceid);
                wi.PROCESSID = Convert.ToInt64(reader.GetValue(2));
                wi.PROCESSNAME = reader.GetValue(3).ToString();

                wi.TASKID = Convert.ToInt64(reader.GetValue(4));
                wi.TASKDISPLAYNAME = reader.GetValue(5).ToString();
                wi.TASKINSTRUCTIONS = reader.GetValue(6).ToString();
                wi.PARTICIPANTID = Convert.ToInt64(reader.GetValue(7));
                wi.PARTICIPANTNAME = reader.GetValue(8).ToString();
                wi.TASKNAME = reader.GetValue(9).ToString();
                if (reader.GetValue(10) is System.DBNull)
                    wi.STARTDATE = Convert.ToDateTime("0001-01-01");
                else
                    wi.STARTDATE = Convert.ToDateTime(reader.GetValue(10));
                if (reader.GetValue(11) is System.DBNull)
                    wi.ENDDATE = Convert.ToDateTime("0001-01-01");
                else
                    wi.ENDDATE = Convert.ToDateTime(reader.GetValue(11));
                wi.STATE = Convert.ToInt32(reader.GetValue(12));
                wi.CHILDINSTANCEID = Convert.ToInt64(reader.GetValue(13));
                wi.PARTICIPANTTYPE = Convert.ToInt16(reader.GetValue(14));
                wi.TASKTYPE = Convert.ToInt32(reader.GetValue(15));
                wi.PROCESSINSTANCES = p;

                p.WORKITEMSs.Add(wi);
            }

            reader.Close();
            return p;

        }

        public int save_synchroactivity(WORKITEMS workitem)
        {
            return 1;
        }

        public int delete_synchroactivitylist(string instanceid, string taskname)
        {
            return 1;
        }

        public WORKITEMS load_workitem(String workitemid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "select workitemid,instanceid,processid,processname,taskid,taskdisplayname,taskinstructions,participantid,participantname,taskname,startdate,enddate,state,childinstanceid,participanttype,tasktype,roleid from we_workitems where workitemid=" + workitemid + "";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            WORKITEMS wi = new WORKITEMS();

            System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
            while (reader.Read())
            {
                wi.Id = Convert.ToInt64(reader.GetValue(0));
                wi.INSTANCEID = Convert.ToInt64(reader.GetValue(1));
                wi.PROCESSID = Convert.ToInt64(reader.GetValue(2));
                wi.PROCESSNAME = reader.GetValue(3).ToString();
                wi.TASKID = Convert.ToInt64(reader.GetValue(4));
                wi.TASKDISPLAYNAME = reader.GetValue(5).ToString();
                wi.TASKINSTRUCTIONS = reader.GetValue(6).ToString();
                wi.PARTICIPANTID = Convert.ToInt64(reader.GetValue(7));
                wi.PARTICIPANTNAME = reader.GetValue(8).ToString();
                wi.TASKNAME = reader.GetValue(9).ToString();
                wi.STARTDATE = Convert.ToDateTime(reader.GetValue(10));

                if (reader.GetValue(11) is System.DBNull)
                    wi.ENDDATE = Convert.ToDateTime("0001-01-01");
                else
                    wi.ENDDATE = Convert.ToDateTime(reader.GetValue(11));
                wi.STATE = Convert.ToInt32(reader.GetValue(12));
                wi.CHILDINSTANCEID = Convert.ToInt64(reader.GetValue(13));
                wi.PARTICIPANTTYPE = Convert.ToInt16(reader.GetValue(14));
                wi.TASKTYPE = Convert.ToInt32(reader.GetValue(15));
                wi.RoleID = reader.GetValue(16).ToString();
            }
            reader.Close();

            sqlCommand = "select INSTANCEID,WORKITEMID,PREVIOUSWORKITERMID,PREPARTICIPANTID,PREPARTICIPANTNAME,PREPARTICIPANTNAME2,PREVIOUSTASKNAME from we_previousworkitems where WORKITEMID=" + workitemid + "";

            dbCommand = db.GetSqlStringCommand(sqlCommand);
            reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);

            while (reader.Read())
            {
                PREVIOUSWORKITEMS pre = new PREVIOUSWORKITEMS();
                pre.INSTANCEID = reader.GetValue(0).ToString();
                pre.WORKITEMID = Convert.ToInt64(reader.GetValue(1));
                pre.PREVIOUSWORKITERMID = Convert.ToInt64(reader.GetValue(2));
                pre.PREPARTICIPANTID = Convert.ToInt64(reader.GetValue(3));
                pre.PREPARTICIPANTNAME = reader.GetValue(4).ToString();
                pre.PREPARTICIPANTNAME2 = reader.GetValue(5).ToString();
                pre.PREVIOUSTASKNAME = reader.GetValue(6).ToString();
                pre.WORKITEMS = wi;
            }
            reader.Close();
            return wi;
        }

        public int get_workitem_activity(long instanceid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "select count(*) from we_workitems where instanceid=" + instanceid + " and state<" + WfStateContants.TASKSTATE_CLOESED + "";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            return Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        public int get_workitem_total_activity(long instanceid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "select count(*) from we_workitems where instanceid=" + instanceid + "";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            return Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        public int get_workitem_state(string workitemid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "select state from we_workitems where workitemid=" + workitemid + "";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            return Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        public IList<PROCESSINSTANCES> getProcessFillPage()
        {
            IList<PROCESSINSTANCES> processes = new List<PROCESSINSTANCES>(); ;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "select instanceid,processname,initiatorname,startdate,enddate,PROCESSINSTANCESTATE from we_processinstances";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);

            PROCESSINSTANCES pro;

            while (reader.Read())
            {
                pro = new PROCESSINSTANCES();

                pro.Id = Convert.ToInt64(reader.GetValue(0));
                pro.PROCESSNAME = reader.GetValue(1).ToString();
                pro.INITIATORNAME = reader.GetValue(2).ToString();
                if (reader.GetValue(3) is System.DBNull)
                    pro.STARTDATE = Convert.ToDateTime("0001-01-01");
                else
                    pro.STARTDATE = Convert.ToDateTime(reader.GetValue(3));

                if (reader.GetValue(4) is System.DBNull)
                    pro.ENDDATE = Convert.ToDateTime("0001-01-01");
                else
                    pro.ENDDATE = Convert.ToDateTime(reader.GetValue(4));

                pro.PROCESSINSTANCESTATE = Convert.ToInt64(reader.GetValue(5));
                processes.Add(pro);

            }
            reader.Close();

            return processes;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceid"></param>
        /// <param name="workitemid"></param>
        /// <returns></returns>
        public int updateXorWorkItemActive(string instanceid, string workitemid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand;

            string sqlCommand = "update we_workitems set [state]=" + WfStateContants.TASKSTATE_ACTIVE + " where workitemid=" + workitemid + "";

            dbCommand = db.GetSqlStringCommand(sqlCommand);

            if (db.ExecuteNonQuery(dbCommand) == 1)
            {
                sqlCommand = "update we_workitems set [state]=" + WfStateContants.TASKSTATE_TERMINATED + " where instanceid=" + instanceid + " and workitemid!=" + workitemid + " and [state]!=" + WfStateContants.TASKSTATE_COMPLETED + "";
                dbCommand = db.GetSqlStringCommand(sqlCommand);
                return db.ExecuteNonQuery(dbCommand);
            }
            else
                return -1;

        }
        /// <summary>
        /// 获取会知人的列表
        /// </summary>
        /// <param name="modeltaskid">工作流模板的工作任务ID</param>
        /// <param name="notifyValue">会知任务的类型ID，9</param>
        /// <returns>返回对象列表</returns>
        public ArrayList getNorifyList(int modeltaskid, int notifyValue)
        {
            ArrayList notifylist = new ArrayList();
            Database db = DatabaseFactory.CreateDatabase();
            string statecmd = "select rolename from we_modeltask where taskname in (select transitionto from we_transition where modeltaskid=" + modeltaskid + ") and tasktype=" + notifyValue + "";
            DbCommand dbCommand = db.GetSqlStringCommand(statecmd);
            DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                notifylist.Add(dt.Rows[i][0].ToString());
            }

            return notifylist;
        }

        public IList<WorkFlow.Model.WORKITEMS> GetProcessRepresent(int instanceid, int processid)
        {
            IList<WorkFlow.Model.WORKITEMS> items;
            items = new List<WorkFlow.Model.WORKITEMS>();
            Database db = DatabaseFactory.CreateDatabase();
            string statecmd = "select taskid,taskname,rolename as roleid,'40' [state] from we_modeltask " +
 "where modelprocessid=" + processid + " and tasktype!=" + WorkFlowLibary.WfStateContants.TASKTYPE_NOTIFY + " and taskname<>'start'" +
 "and taskid not in(select taskid from we_workitems where instanceid=" + instanceid + " and processid=" + processid + " and tasktype!=" + WorkFlowLibary.WfStateContants.TASKTYPE_NOTIFY + " and taskname<>'start')" +
 " union " +
 "select taskid,taskdisplayname,roleid,[state] from we_workitems where instanceid=" + instanceid + " and processid=" + processid + " and tasktype!=" + WorkFlowLibary.WfStateContants.TASKTYPE_NOTIFY + " and taskname<>'start'";

            DbCommand dbCommand = db.GetSqlStringCommand(statecmd);
            DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WorkFlow.Model.WORKITEMS item = new WORKITEMS();
                item.TASKID = Convert.ToInt64(dt.Rows[i][0].ToString());
                item.TASKNAME = dt.Rows[i][1].ToString();
                item.RoleID = dt.Rows[i][2].ToString();
                item.STATE = Convert.ToInt32(dt.Rows[i][3].ToString());
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// 撤销时删除工作流的相关项目。
        /// </summary>
        /// <param name="processid">工作流模板的ID</param>
        /// <param name="instanceid">工作流实例的ID</param>
        /// <returns>成功返回1，失败返回0</returns>
        public int TerminateProcess(int processid, int instanceid)
        {
            int ret = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand;
            string sqlCommand = string.Empty;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    sqlCommand = "delete we_modelprocess where ModelProcessID=" + processid + "";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    ret = db.ExecuteNonQuery(dbCommand, transaction);
                    if (ret == 1)
                    {
                        sqlCommand = "delete we_modeltask where ModelProcessID=" + processid;
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }

                    sqlCommand = "delete we_PROCESSINSTANCES where INSTANCEID=" + instanceid + " and processid=" + processid + "";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    ret = db.ExecuteNonQuery(dbCommand, transaction);
                    if (ret == 1)
                    {
                        sqlCommand = "delete we_WORKITEMS where INSTANCEID=" + instanceid + " and processid=" + processid + "";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.ExecuteNonQuery(dbCommand, transaction);

                        sqlCommand = "delete we_WORKITEMDATA where INSTANCEID=" + instanceid + "";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    sqlCommand = "select RoleSetID from we_RoleSet where ModelProcessID=" + processid + "";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    object rolesetid = db.ExecuteScalar(dbCommand, transaction);
                    if (rolesetid != null)
                    {
                        sqlCommand = "delete we_Roles where RoleSetID=" + rolesetid.ToString() + "";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.ExecuteNonQuery(dbCommand, transaction);

                        sqlCommand = "delete we_RoleSet where RoleSetID=" + rolesetid.ToString() + "";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    // Commit the transaction
                    transaction.Commit();

                    ret = 1;
                }
                catch
                {
                    // Rollback transaction 
                    transaction.Rollback();
                    ret = 0;
                }
                connection.Close();

                return ret;
            }
        }

        /// <summary>
        /// 离职时人员变更
        /// </summary>
        /// <param name="OrderType">单据类型：PR,PA,PN,SR</param>
        /// <param name="processid">工作流模板ID</param>
        /// <param name="instanceid">工作流实例ID</param>
        /// <param name="originalUserID">原用户ID</param>
        /// <param name="NewUserID">新用户ID</param>
        /// <param name="originalUserName">原用户名称</param>
        /// <param name="newUserName">新用户名称</param>
        /// <returns></returns>
        public int UpdateRoleWhenLastDay(string OrderType, int processid, int instanceid, int originalUserID, int NewUserID, string originalUserName, string newUserName, System.Data.SqlClient.SqlTransaction trans)
        {
            int ret = 0;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = trans.Connection;
            cmd.Transaction = trans;
            string sqlCommand = string.Empty;
            //更新工作流模板里的role
            sqlCommand = "update we_modeltask set taskname=replace(taskname,'" + originalUserName + "','" + newUserName + "'),rolename=" + NewUserID + " where  modelprocessid=" + processid + " and rolename=" + originalUserID + "";
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();
            //更新工作流模板顺序里的from
            sqlCommand = "update we_transition set transitionname=replace(transitionname,'" + originalUserName + "','" + newUserName + "') where  modeltaskid in(select TaskID from we_modeltask where modelprocessid=" + processid + ") and transitionname like '%" + originalUserName + "%'";
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();
            //更新工作流模板顺序里的to
            sqlCommand = "update we_transition set transitionto=replace(transitionto,'" + originalUserName + "','" + newUserName + "') where  modeltaskid in(select TaskID from we_modeltask where modelprocessid=" + processid + ") and transitionto like '%" + originalUserName + "%'";
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();
            //更新工作流实例里的role
            sqlCommand = "update we_workitems set taskdisplayname=replace(taskdisplayname,'" + originalUserName + "','" + newUserName + "'),taskname=replace(taskname,'" + originalUserName + "','" + newUserName + "'),roleid=" + NewUserID + " where  instanceid=" + instanceid + " and roleid=" + originalUserID + "";
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();
            ret = 1;
            return ret;
        }
    }
}
