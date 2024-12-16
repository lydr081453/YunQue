using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_SupplierManager
    {
        private readonly SC_SupplierDataProvider dal = new SC_SupplierDataProvider();
        public SC_SupplierManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Supplier model)
        {
            return dal.Add(model);
        }

        public int Submit(SC_Supplier model, List<SC_LinkMan> linker, SC_Log logmodel, ESP.Compatible.Employee currentUser)
        {
            return dal.Submit(model, linker, logmodel, currentUser);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateByOverrule(SC_Supplier model, SC_Log log)
        {

            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();


                try
                {

                    dal.Update(model, trans, conn);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    returnValue = 1;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_Supplier model)
        {
            dal.Update(model);
        }
        /// <summary>
        /// 更新一条数据,获得供应商的编号
        /// </summary>
        public int Update(SC_Supplier model, SC_Log log)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();


                try
                {
                    model.Supplier_code = getNewSupplierCode();
                    dal.Update(model, trans, conn);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    returnValue = 1;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }
            }
            return returnValue;
        }

        public int UpdateOrScore(SC_Supplier model, List<SC_SupplierScore> list)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // model.Supplier_code = getNewSupplierCode();                    
                    dal.Update(model, trans, conn);
                    foreach (SC_SupplierScore ss in list)
                    {
                        SC_SupplierScoreDataProvider.Add(ss, trans, conn);
                    }
                    returnValue = 1;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }

            }
            return returnValue;
        }

        public int UpdateAndAddLog(SC_Supplier model, SC_Log log)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    dal.Update(model, trans, conn);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    returnValue = 1;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }

            }
            return returnValue;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            dal.Delete(id);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id, SC_Log log)
        {
            int returnValue = 0;
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    dal.Delete(id, trans, conn);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    returnValue = 1;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    returnValue = 0;
                }

            }
            return returnValue;
        }

        /// <summary>
        /// 逻辑删除供应商
        /// </summary>
        /// <param name="id"></param>
        public bool LogicDelete(int id, SC_Log log)
        {
            using (SqlConnection conn = new SqlConnection(Common.DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    dal.LogicDelete(id, trans);
                    new SC_LogDataProvider().Add(log, trans, conn);
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ESP.Logging.Logger.Add(ex.ToString());
                    return false;
                }

            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Supplier GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public List<SC_Supplier> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }
        public List<SC_Supplier> GetListforgroup(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }

        public List<SC_Supplier> GetList(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetList(strWhere, parms);
        }

        public List<SC_Supplier> GetListByAuditor(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetListByAuditor(strWhere, parms);
        }

        public DataTable GetSupplierLinkField(string typeIds, string strWhere, List<SqlParameter> parms)
        {
            return dal.GetSupplierLinkField(typeIds, strWhere, parms);
        }

        public List<SC_Supplier> GetListByQualificationAuditor(string strWhere, List<SqlParameter> parms)
        {
            return dal.GetListByQualificationAuditor(strWhere, parms);
        }
        public List<SC_Supplier> GetListByName(string UserName)
        {
            string strWhere = string.Empty;
            strWhere += " LogName=@LogName";
            SqlParameter[] parameters = {
					new SqlParameter("@LogName", SqlDbType.NVarChar,50)};
            parameters[0].Value = UserName;

            return dal.GetList(strWhere, parameters);
        }
        public List<SC_Supplier> GetListBySupplierName(string SupplierNameCN)
        {
            string strWhere = string.Empty;
            strWhere += " supplier_name=@supplier_name and Status>1 and Status<>200 and Status <> 300";
            SqlParameter[] parameters = {
					new SqlParameter("@supplier_name", SqlDbType.NVarChar,500)};
            parameters[0].Value = SupplierNameCN;

            return dal.GetList(strWhere, parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public int Login(string Name, string Password)
        {
            int userid = 0;
            string strWhere = string.Empty;
            strWhere += " LogName=@LogName and Password=@Password";
            List<SC_Supplier> list;
            SqlParameter[] parameters = {
					new SqlParameter("@LogName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50)};
            parameters[0].Value = Name;
            parameters[1].Value = Password;
            list = dal.GetList(strWhere, parameters);
            if (list.Count > 0)
            {
                if (list[0].Status == State.SupplierStatus_save || list[0].Status == State.SupplierStatus_del)
                    userid = -1;
                else
                    userid = list[0].id;
            }
            return userid;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public string getNewSupplierCode()
        {
            string newsuppliercode = "";
            string datetime = DateTime.Now.ToString("yyMM");
            string old = new ESP.Supplier.DataAccess.SC_SupplierDataProvider().getSupplierCode(datetime);
            if (old == "")
            {
                newsuppliercode = "S" + datetime + "0001";
            }
            else
            {
                string nu = old.Substring(5, 4);
                int num = int.Parse(nu);
                newsuppliercode = "S" + datetime + (++num).ToString("0000");
            }
            return newsuppliercode;
        }

        /// <summary>
        /// 获取关联三级物料的供应商列表
        /// </summary>
        /// <param name="terms"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetSupplierListJoinSupplierType(string terms, List<SqlParameter> parms)
        {
            return dal.GetSupplierListJoinSupplierType(terms, parms);
        }

        public string ImportSupplier(SC_Supplier model, SC_LinkMan linkman, int num)
        {
            string returnValue = "";
            model.Supplier_code = getNewSupplierCode();
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //添加sc_supplier
                    int supplierId = dal.Add(model, trans);
                    //添加sc_linkman
                    if (linkman != null)
                    {
                        linkman.SupplierId = supplierId;
                        new SC_LinkManDataProvider().Add(linkman, trans);
                    }
                    //添加SC_SupplierSubsidiaryUsers
                    SC_SupplierSubsidiaryUsers usmodel = new SC_SupplierSubsidiaryUsers();
                    usmodel.SupplierID = supplierId;
                    usmodel.Name = model.supplier_name;
                    usmodel.LogName = model.LogName;
                    usmodel.Email = model.contact_Email;
                    usmodel.CreatedDate = usmodel.ModifiedDate = DateTime.Now;
                    usmodel.Password = model.Password;
                    usmodel.IsEffective = true;
                    usmodel.IsDel = false;
                    usmodel.IsAdmin = false;
                    usmodel.CreatedDate = DateTime.Now;
                    new SC_SupplierSubsidiaryUsersProvider().Add(usmodel, trans);
                    //添加SC_Suppliertype
                    List<string> fieldTypeIds = System.Configuration.ConfigurationManager.AppSettings["fieldTypeId"].ToString().Split(',').ToList();
                    for (int i = 0; i < fieldTypeIds.Count; i++)
                    {
                        int level2Id = int.Parse(fieldTypeIds[i]);
                        DataSet versionlist = new XML_VersionListDataProvider().GetList(" classId=" + level2Id);
                        for (int j = 0; j < versionlist.Tables[0].Rows.Count; j++)
                        {
                            SC_SupplierType st = new SC_SupplierType();
                            st.SupplierId = supplierId;
                            st.TypeId = int.Parse(versionlist.Tables[0].Rows[j]["id"].ToString());
                            st.CreatTime = DateTime.Now;
                            st.LastUpdateTime = DateTime.Now;
                            st.CreatedIP = model.CreatIP;
                            st.LastModifiedIP = model.CreatIP;
                            SC_SupplierTypeDataProvider.Add1(st, trans);
                        }
                    }
                    trans.Commit();
                    returnValue = "行号：" + num.ToString() + "，" + model.supplier_name + "导入成功！\r\n";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    returnValue = "行号：" + num.ToString() + "，" + model.supplier_name + "导入失败！原因：" + ex.Message + "\r\n";
                }
            }

            return returnValue;
        }

        #endregion  成员方法

    }

    public class SC_SupplierPriceFilesManager
    {
        private readonly SC_SupplierPriceFilesProvider dal = new SC_SupplierPriceFilesProvider();
        public SC_SupplierPriceFilesManager()
        {

        }

        public int Add(SC_SupplierPriceFiles model)
        {
            return dal.Add(model);
        }

        public int Delete(int id)
        {
            return dal.Delete(id);
        }

        public SC_SupplierPriceFiles GetModel(int id)
        {
            return dal.GetModel(id);
        }

        public List<SC_SupplierPriceFiles> GetList(int supplierId)
        {

            return dal.GetList(supplierId);
        }
    }
}
