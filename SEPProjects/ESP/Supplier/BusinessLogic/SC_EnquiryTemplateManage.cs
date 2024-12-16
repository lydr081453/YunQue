using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_EnquiryTemplateManager
    {
        private static readonly SC_EnquiryTemplateDataProvider dal = new SC_EnquiryTemplateDataProvider();
        public SC_EnquiryTemplateManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SC_EnquiryTemplate model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SC_EnquiryTemplate model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SC_EnquiryTemplate GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SC_EnquiryTemplate> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SC_EnquiryTemplate> DataTableToList(DataTable dt)
        {
            List<SC_EnquiryTemplate> modelList = new List<SC_EnquiryTemplate>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_EnquiryTemplate model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_EnquiryTemplate();
                    model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.Xml = dt.Rows[n]["Xml"].ToString();
                    model.TypeID = int.Parse(dt.Rows[n]["TypeID"].ToString());
                    model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
                    model.MessageId = int.Parse(dt.Rows[n]["MessageId"].ToString());

                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }

                    if (dt.Rows[n]["UpdateTime"].ToString() != "")
                    {
                        model.UpdateTime = DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public static SC_EnquiryTemplate GetModelByMessageId(int MessageId)
        {
            DataSet ds = dal.GetList(string.Format("MessageId={0}",MessageId));

            return getModelByDataTable(ds.Tables[0]);
        }

        /// <summary>
        /// 将DataTable 转换为 SC_EnquiryTemplate
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static SC_EnquiryTemplate getModelByDataTable(DataTable dt)
        {
            SC_EnquiryTemplate model = new SC_EnquiryTemplate();
            if (dt!=null)
            {
                model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                model.Name = dt.Rows[0]["Name"].ToString();
                model.Xml = dt.Rows[0]["Xml"].ToString();
                model.TypeID = int.Parse(dt.Rows[0]["TypeID"].ToString());
                model.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                model.IsDelete = int.Parse(dt.Rows[0]["IsDelete"].ToString());
                model.MessageId = int.Parse(dt.Rows[0]["MessageId"].ToString());
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法


        #region 数据读取

        /// <summary>
        /// 初始化表头
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DataTable getHeaderTable(int typeId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            dt.Columns.Add("Use");

            DataLib.Model.VersionList tableModel = new DataLib.BLL.VersionList().GetModel(typeId);
            List<DataLib.Model.TableModel> ModelList = new DataLib.BLL.TableManage().LoadXML(tableModel.XML, tableModel.TableName);
            if (ModelList != null)
            {
                foreach (DataLib.Model.TableModel sm in ModelList)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = sm.cnDescription;

                    switch (sm.Control.ToLower())
                    {
                        case "":
                            dr["Type"] = "";
                            break;
                        case "datetime":
                            dr["Type"] = "datetime";
                            break;
                        case "integral":
                            dr["Type"] = "integral";
                            break;
                        default:
                            dr["Type"] = "";
                            break;
                    }


                    if (sm.ID == "Price")
                    {
                        dr["Use"] = "1";
                        dr["Type"] = "integral";
                    }
                    else
                    {
                        dr["Use"] = "0";
                    }

                    dt.Rows.Add(dr);
                }

                DataRow dr1 = dt.NewRow();
                dr1["Name"] = "数量";
                dr1["Type"] = "integral";
                dr1["Use"] = "0";
                dt.Rows.Add(dr1);
            }
            else
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = "项目名称";
                dr1["Type"] = "";
                dr1["Use"] = "0";
                dt.Rows.Add(dr1);

                DataRow dr2 = dt.NewRow();
                dr2["Name"] = "单位";
                dr2["Type"] = "";
                dr2["Use"] = "0";
                dt.Rows.Add(dr2);

                DataRow dr3 = dt.NewRow();
                dr3["Name"] = "数量";
                dr3["Type"] = "integral";
                dr3["Use"] = "0";
                dt.Rows.Add(dr3);

                DataRow dr4 = dt.NewRow();
                dr4["Name"] = "单价";
                dr4["Type"] = "integral";
                dr4["Use"] = "1";
                dt.Rows.Add(dr4);
            }

            return dt;
        }

        /// <summary>
        /// 初始化报价项
        /// </summary>
        /// <param name="sdt"></param>
        /// <returns></returns>
        public static DataTable getBodyTable(DataTable sdt, int typeId)
        {
            //标准报价模版
            DataTable dt = new DataTable();
            DataColumn colNumber = new DataColumn("ID");
            colNumber.AutoIncrement = true;//设置是否为自增列
            colNumber.AutoIncrementSeed = 1;//设置自增初始值
            colNumber.AutoIncrementStep = 1;//设置每次子增值
            dt.Columns.Add(colNumber);

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                dt.Columns.Add(sdt.Rows[i]["Name"].ToString());
            }

            DataTable mdt = getsubtable(typeId);
            if (mdt.Rows.Count > 0)
            {
                List<int> ids = new List<int>();
                for (int i = 0; i < mdt.Rows.Count; i++)
                {
                    bool mb = true;
                    foreach (int m in ids)
                    {
                        if (m == int.Parse(mdt.Rows[i]["ParentID"].ToString()))
                        {
                            mb = false;
                            break;
                        }
                    }
                    if (mb)
                    {
                        ids.Add(int.Parse(mdt.Rows[i]["ParentID"].ToString()));
                    }
                }


                for (int i = 0; i < mdt.Rows.Count; i++)
                {
                    DataRow dr = mdt.Rows[i];
                    string name = dr["Name"].ToString();
                    string unit = dr["Unit"].ToString();
                    int mid = int.Parse(dr["ID"].ToString());
                    int parentid = int.Parse(dr["ParentID"].ToString());
                    name = name.Replace(",", "，").Replace("|", "，");
                    unit = unit.Replace(",", "，").Replace("|", "，");

                    DataRow mdr = dt.NewRow();

                    bool mb = false;

                    if (string.IsNullOrEmpty(unit))
                    {
                        mb = true;
                    }
                    else
                    {
                        mb = false;
                    }

                    if (mb)
                    {
                        mdr["单价"] = name;
                        mdr["数量"] = "-999";
                    }
                    else
                    {
                        mdr[1] = name;
                        mdr["单位"] = unit;
                        mdr["数量"] = 1;
                    }

                    dt.Rows.Add(mdr);
                }
            }
            return dt;
        }


        #region 读取系统标准报价项
        private static DataTable getsubtable(int typeId)
        {
            DataTable dt = new DataTable();
            DataColumn colNumber = new DataColumn("ID");
            colNumber.AutoIncrement = true;//设置是否为自增列
            colNumber.AutoIncrementSeed = 1;//设置自增初始值
            colNumber.AutoIncrementStep = 1;//设置每次子增值
            dt.Columns.Add(colNumber);
            dt.Columns.Add("Name");
            dt.Columns.Add("ParentID");
            dt.Columns.Add("Unit");

            DataTable mdt = dt.Clone();

            DataLib.Model.VersionList tableModel = new DataLib.Model.VersionList();
            DataLib.BLL.VersionList bll = new DataLib.BLL.VersionList();
            tableModel = bll.GetModel(typeId);

            #region 判断是否有数据
            string sql = string.Format(" 1=1 and TableName='{0}' and Version='{1}' and ParentName like '{2}%' order by len(parentName)", tableModel.TableName, tableModel.Version, "Type");
            DataTable mt = new DataLib.DAL.LinkTable().GetList(sql).Tables[0];

            if (mt.Rows.Count > 0)
            {

                for (int i = 0; i < mt.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = mt.Rows[i]["Value"].ToString();
                    dr["ParentID"] = 0;
                    dr["Unit"] = mt.Rows[i]["Unit"].ToString();
                    dt.Rows.Add(dr);
                }

                //修改ParentID
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < mt.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["Name"] == mt.Rows[j]["Value"])
                        {
                            dt.Rows[i]["ParentID"] = getSite(mt.Rows[j]["ParentName"].ToString(), dt).ToString();
                        }
                    }
                }

            }
            #endregion

            tableSort(dt, "0", ref mdt);
            return mdt;
        }

        private static void tableSort(DataTable dt, string depid, ref DataTable mdt)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "ParentID=" + depid;

            string strID;
            foreach (DataRowView dr in dv)
            {
                strID = dr["ID"].ToString();
                if (strID != "")
                {
                    DataView sdv = dt.DefaultView;
                    sdv.RowFilter = "ParentID=" + strID;

                    DataRow mdr = mdt.NewRow();
                    mdr["Name"] = dr["Name"].ToString();
                    mdr["Unit"] = dr["Unit"].ToString();
                    mdr["ParentID"] = dr["ParentID"].ToString();
                    mdt.Rows.Add(mdr);
                    tableSort(dt, strID, ref mdt);
                }
            }
        }
        private static int getSite(string str, DataTable dt)
        {
            int re = 0;
            string[] s = str.Split('_');
            if (s.Length > 1)
            {
                re = s.Length - 1;
                //得到对应ID
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select(string.Format("Name='{0}'", s[re]));
                    re = int.Parse(dr[0]["id"].ToString());
                }
            }

            return re;
        }
        #endregion

        #region 获取指定列是否为供应商填写
        public static bool getSupplierInput(string xml, string name)
        {
            bool mb = false;

            if (!string.IsNullOrEmpty(xml))
            {
                string[] str = xml.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string[] p = str[i].Split('|');
                    if (p[0].ToLower() == name.ToLower())
                    {
                        if (p[2] == "1")
                        {
                            mb = true;
                        }
                        break;
                    }
                }

            }
            return mb;
        }

        public static bool getSupplierInput(int modelId, string name)
        {
            SC_EnquiryTemplate model = SC_EnquiryTemplateManager.GetModel(modelId);
            return getSupplierInput(model.Xml, name);
        }

        public static string getSupplierInputText(string xml, string name, string viewStr)
        {
            if (getSupplierInput(xml, name))
            {
                viewStr = "<img src='/public/images/icon/lock.png' title='此项由供应商填写' style='cursor:pointer;'>";
            }
            return viewStr;
        }

        public static List<int> getSupplierInput(string xml)
        {
            List<int> ml = new List<int>();
            if (!string.IsNullOrEmpty(xml))
            {
                string[] str = xml.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string[] p = str[i].Split('|');
                    if (p[2] == "1")
                    {
                        ml.Add(i);
                    }
                }

            }
            return ml;
        }

        public static List<int> getSupplierInput(int modelId)
        {
            SC_EnquiryTemplate model = SC_EnquiryTemplateManager.GetModel(modelId);
            return getSupplierInput(model.Xml);
        }

        public static List<int> getSupplierInput(DataTable dt)
        {
            string str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = dt.Rows[i]["Name"].ToString() + "|" + dt.Rows[i]["Type"].ToString() + "|" + dt.Rows[i]["Use"].ToString();
                str += "," + s;
            }
            str = str.Substring(1);
            return getSupplierInput(str);
        }

        #endregion


        #region 获取单价、数量的位置

        public static void getPCSite(int mid, ref int priceSite, ref int countSite)
        {
            SC_EnquiryTemplate tmodel = SC_EnquiryTemplateManager.GetModel(mid);
            getPCSite(tmodel.Xml, ref priceSite, ref countSite);
        }

        public static void getPCSite(DataTable dt, ref int priceSite, ref int countSite)
        {
            string str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = dt.Rows[i]["Name"].ToString() + "|" + dt.Rows[i]["Type"].ToString() + "|" + dt.Rows[i]["Use"].ToString();
                str += "," + s;
            }
            str = str.Substring(1);
            getPCSite(str, ref priceSite, ref countSite);
        }

        public static void getPCSite(string modelXml, ref int priceSite, ref int countSite)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(modelXml))
            {
                string[] str = modelXml.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string[] p = str[i].Split('|');
                    switch (p[0])
                    {
                        case "数量":
                            {
                                countSite = i;
                            }
                            break;
                        case "单价":
                            {
                                priceSite = i;
                            }
                            break;
                    }
                }
            }
        }

        #endregion

        #region 获取供应商填写字段列表
        public static List<string[]> getSupplierInputTable(string xml)
        {
            List<string[]> ml = new List<string[]>();

            if (!string.IsNullOrEmpty(xml))
            {
                string[] str = xml.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string[] ms = new string[3];
                    string[] p = str[i].Split('|');
                    if (p[2] == "1")
                    {
                        ms[0] = i.ToString();
                        ms[1] = p[1];
                        ms[2] = p[0];
                        ml.Add(ms);
                    }
                }

            }
            return ml;
        }

        public static List<string[]> getSupplierInputTable(int modelId)
        {
            SC_EnquiryTemplate model = SC_EnquiryTemplateManager.GetModel(modelId);
            return getSupplierInputTable(model.Xml);
        }

        public static List<string[]> getSupplierInputTable(DataTable dt)
        {
            string str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = dt.Rows[i]["Name"].ToString() + "|" + dt.Rows[i]["Type"].ToString() + "|" + dt.Rows[i]["Use"].ToString();
                str += "," + s;
            }
            str = str.Substring(1);
            return getSupplierInputTable(str);
        }
        #endregion

        #endregion
    }
}
