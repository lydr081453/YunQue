using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESP.Media.Entity;
using System.Collections;
using ESP.Media.Access.Utilities;
using System.Data.SqlClient;
using ESP.Media.BusinessLogic;

namespace ESP.Media.BusinessLogic
    {
        public class ProductlinesManager
        {
            public ProductlinesManager()
            {

            }

            public static List<QueryProductLineInfo> GetAllObjectList(string term, Hashtable ht)
            {
                DataTable dt = GetList(term, ht);
                var query = from productline in dt.AsEnumerable() select new QueryProductLineInfo(productline);
                List<QueryProductLineInfo> items = new List<QueryProductLineInfo>();
                foreach (QueryProductLineInfo item in query)
                {
                    items.Add(item);
                }
                return items;
            }

            public static DataTable GetAllList()
            {
                return GetList(null, null);
            }

            public static DataTable GetList(string term, Hashtable ht)
            {
                if (term == null)
                    term = string.Empty;
                if (ht == null)
                {
                    ht = new Hashtable();
                }
                if (!ht.Contains("@del"))
                {
                    ht.Add("@del", (int)Global.FiledStatus.Del);
                }
                if (term.IndexOf("order by") < 0)
                {
                    term += " and del!=@del order by a.Productlineid desc";
                }
                else
                    term = " del!=@del and " + term;


                return ESP.Media.DataAccess.ProductlinesDataProvider.QueryInfo(term, ht);
            }

            private static DataTable query(string term, Hashtable ht)
            {
                string sql = @"select a.productlineid as productlineid,a.productlinename as productlinename,
                            a.productlinedescription as productlinedescription,a.productlinetitle as productlinetitle,
                            a.clientid as clientid,a.currentversion as currentversion,a.status as status,
                            a.createdbyuserid as createdbyuserid,a.createdip as createdip,a.createddate as createddate,
                            a.lastmodifiedbyuserid as lastmodifiedbyuserid,a.lastmodifiedip as lastmodifiedip,
                            a.lastmodifieddate as lastmodifieddate,
                            b.clientcfullname as clientcfullname,b.clientcshortname as clientcshortname,
                            b.clientefullname as clientefullname,b.clienteshortname as clienteshortname
                            from Media_productlines as a 
                            left join Media_Clients as b on a.clientid = b.clientid
                            where 1=1 {0} order by a.Productlineid desc";
                if (term == null)
                    term = string.Empty;
                if (ht == null)
                {
                    ht = new Hashtable();
                }
                if (!ht.Contains("@del"))
                {
                    ht.Add("@del", (int)Global.FiledStatus.Del);
                }
                term += " and a.del!=@del ";
                sql = string.Format(sql, term);

                return clsSelect.QueryBySql(sql, ESP.Media.Access.Utilities.Common.DictToSqlParam(ht));
            }

            public static DataTable GetListWithClient(string term, Hashtable ht)
            {

                return query(term, ht);
            }

            public static ProductlinesInfo GetModel(int id)
            {
                return ESP.Media.DataAccess.ProductlinesDataProvider.Load(id);
            }

            public static int Add(ProductlinesInfo obj, string filename, int userid, out string errmsg)
            {
                using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        string term = "ProductLineName=@ProductLineName AND del!=@del";
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@ProductLineName", SqlDbType.NVarChar);
                        param[0].Value = obj.Productlinename;
                        param[1] = new SqlParameter("@del", SqlDbType.Int);
                        param[1].Value = (int)Global.FiledStatus.Del;
                        DataTable dt = ESP.Media.DataAccess.ProductlinesDataProvider.QueryInfo(trans, term, param);
                        if (dt.Rows.Count > 0)
                        {
                            errmsg = "产品线名称已存在!";
                            trans.Rollback();
                            conn.Close();
                            return -1;
                        }
                        errmsg = string.Empty;
                        //if (filedata != null)
                        //{
                        //    obj.Productlinetitle = CommonManager.SaveFile(ConfigManager.ProductLineLogoPath,filename, filedata, true);
                        //}

                        if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                        {
                            obj.Productlinetitle = filename.ToString();
                        }
                        obj.Currentversion = CommonManager.GetLastVersion("ProductLine", obj.Productlineid, trans);
                        int ret = ESP.Media.DataAccess.ProductlinesDataProvider.insertinfo(obj, trans);
                        //OperatelogManager.add((int)Global.SysOperateType.Add, (int)Global.Tables.ProductLine, userid, trans);//添加产品线日志
                        obj.Productlineid = ret;
                        obj.Lastmodifiedbyuserid = userid;
                        obj.Lastmodifiedip = obj.Lastmodifiedip;
                        obj.Lastmodifieddate = DateTime.Now.ToString();
                        SaveHist(trans, obj, userid);
                        trans.Commit();
                        conn.Close();
                        return ret;
                    }
                    catch (Exception exception)
                    {
                        trans.Rollback();
                        conn.Close();
                        errmsg = exception.Message;
                        return -2;
                    }
                }
            }

            public static int Update(ProductlinesInfo obj, string filename, int userid, out string errmsg)
            {
                using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        string term = "ProductLineName=@ProductLineName AND ProductLineID!=@ProductLineID AND del!=@del";
                        SqlParameter[] param = new SqlParameter[3];
                        param[0] = new SqlParameter("@ProductLineName", SqlDbType.NVarChar);
                        param[0].Value = obj.Productlinename;
                        param[1] = new SqlParameter("@ProductLineID", SqlDbType.Int);
                        param[1].Value = obj.Productlineid;
                        param[2] = new SqlParameter("@del", SqlDbType.Int);
                        param[2].Value = (int)Global.FiledStatus.Del;
                        DataTable dt = ESP.Media.DataAccess.ProductlinesDataProvider.QueryInfo(term, param);
                        if (dt.Rows.Count > 0)
                        {
                            errmsg = "产品线名称已存在!";
                            trans.Rollback();
                            conn.Close();
                            return -1;
                        }
                        errmsg = string.Empty;
                        //if (filedata != null)
                        //{
                        //    obj.Productlinetitle = CommonManager.SaveFile(ConfigManager.ProductLineLogoPath, filename, filedata, true);
                        //}
                        if (!string.IsNullOrEmpty(filename) && filename.Length > 0)
                        {
                            obj.Productlinetitle = filename.ToString();
                        }
                        obj.Currentversion = CommonManager.GetLastVersion("ProductLine", obj.Productlineid, trans);
                        if (ESP.Media.DataAccess.ProductlinesDataProvider.updateInfo(trans, null, obj, string.Empty, null))
                        {
                            obj.Lastmodifiedbyuserid = userid;
                            obj.Lastmodifiedip = obj.Lastmodifiedip;
                            obj.Lastmodifieddate = DateTime.Now.ToString();
                            SaveHist(trans, obj, userid);
                            //OperatelogManager.add((int)Global.SysOperateType.Edit, (int)Global.Tables.ProductLine, userid, trans);//更新产品线日志
                            trans.Commit();
                            conn.Close();
                            return 1;
                        }
                        else
                        {
                            errmsg = "修改失败!";
                            conn.Close();
                            return -3;
                        }
                    }
                    catch (Exception exception)
                    {
                        trans.Rollback();
                        conn.Close();
                        errmsg = exception.Message;
                        return -2;
                    }
                }
            }

            public static int Delete(ProductlinesInfo obj, out string errmsg)
            {
                errmsg = "删除成功!";
                try
                {
                    obj.Del = (int)Global.FiledStatus.Del;
                    if (ESP.Media.DataAccess.ProductlinesDataProvider.updateInfo(null, obj, string.Empty, null))
                    {
                        return 1;
                    }
                    else
                    {
                        errmsg = "删除失败!";
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    errmsg = exception.Message;
                    return -2;
                }
            }

            public static int DeleteRelation(int productlineid, out string errmsg)
            {
                ProductlinesInfo obj = GetModel(productlineid);
                return DeleteRelation(obj, out errmsg);
            }

            public static int DeleteRelation(ProductlinesInfo obj, out string errmsg)
            {
                errmsg = "删除成功!";
                try
                {
                    obj.Clientid = 0;
                    if (ESP.Media.DataAccess.ProductlinesDataProvider.updateInfo(null, obj, string.Empty, null))
                    {
                        return 1;
                    }
                    else
                    {
                        errmsg = "删除失败!";
                        return -3;
                    }
                }
                catch (Exception exception)
                {
                    errmsg = exception.Message;
                    return -2;
                }
            }

            public static DataTable GetHistListByClientID(int productlineID)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", SqlDbType.Int);
                param[0].Value = productlineID;
                param[1] = new SqlParameter("@del", SqlDbType.Int);
                param[1].Value = (int)Global.FiledStatus.Del;
                return ESP.Media.DataAccess.ProductlineshistDataProvider.QueryInfo("del!=@del and productlineID=@id ", param);
            }

            public static ProductlineshistInfo GetHistModel(int id)
            {
                if (id <= 0) return new ProductlineshistInfo();
                return ESP.Media.DataAccess.ProductlineshistDataProvider.Load(id);
            }

            private static void SaveHist(SqlTransaction trans, ProductlinesInfo obj, int userid)
            {
                ProductlineshistInfo hist = new ProductlineshistInfo();
                hist.Lastmodifiedbyuserid = userid;
                hist.Lastmodifiedip = obj.Lastmodifiedip;
                hist.Lastmodifieddate = DateTime.Now.ToString();
                hist.Productlineid = obj.Productlineid;
                hist.Clientid = obj.Clientid;
                hist.Productlinename = obj.Productlinename;
                hist.Productlinetitle = obj.Productlinetitle;
                hist.Productlinedescription = obj.Productlinedescription;
                hist.Createdbyuserid = obj.Lastmodifiedbyuserid;
                hist.Createddate = obj.Lastmodifieddate;
                hist.Createdip = obj.Lastmodifiedip;
                hist.Status = obj.Status;
                hist.Version = CommonManager.GetLastVersion("ProductLine", obj.Productlineid, trans);
                ESP.Media.DataAccess.ProductlineshistDataProvider.insertinfo(hist, trans);
            }
            //关联产品线
            public static int LinkToPoductline(int[] productlineIds, int cleintId, out string errmsg)
            {
                if (productlineIds.Length <= 0)
                {
                    errmsg = "没有关联产品线";
                    return -1;
                }
                errmsg = string.Empty;
                string where = "WHERE ProductLineID in({0}) ";
                string ids = string.Empty;
                foreach (int i in productlineIds)
                {
                    ids += i.ToString() + ",";
                }
                where = string.Format(where, ids.Trim(','));
                string sql = string.Format("UPDATE media_ProductLines SET ClientID={0} {1}", cleintId, where);
                try
                {
                    return clsUpdate.funUpdate(sql);
                }
                catch (Exception exception)
                {
                    errmsg = exception.Message;
                    return -2;
                }
            }

        }
    }