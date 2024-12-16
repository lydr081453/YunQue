using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;
using System.Security.Cryptography;

namespace ESP.HumanResource.BusinessLogic
{
    public class UserValidateManager
    {
        private readonly static UserValidateDataProvider dal=new UserValidateDataProvider();
		public UserValidateManager()
		{}
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
		public static int Add(UserValidateInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static void Update(UserValidateInfo model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static UserValidateInfo GetModel(int ID)
		{
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<UserValidateInfo> GetModelList(string strWhere)
		{
            DataSet ds = dal.GetList(strWhere);
            List<UserValidateInfo> modelList = new List<UserValidateInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UserValidateInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new UserValidateInfo();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(ds.Tables[0].Rows[n]["UserID"].ToString());
                    }
                    model.Pwd = ds.Tables[0].Rows[n]["Pwd"].ToString();
                    if (ds.Tables[0].Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[n]["CreatedDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["CreatedUserID"].ToString() != "")
                    {
                        model.CreatedUserID = int.Parse(ds.Tables[0].Rows[n]["CreatedUserID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["ModifiedDate"].ToString() != "")
                    {
                        model.ModifiedDate = DateTime.Parse(ds.Tables[0].Rows[n]["ModifiedDate"].ToString());
                    }
                    model.ModifiedUserID = ds.Tables[0].Rows[n]["ModifiedUserID"].ToString();
                    if (ds.Tables[0].Rows[n]["LogonDate"].ToString() != "")
                    {
                        model.LogonDate = DateTime.Parse(ds.Tables[0].Rows[n]["LogonDate"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
		}

        public bool IsUserValidate(int userid, string pwd)
        {
            UserValidateInfo info = dal.GetModel(userid,GetMD5(pwd));
            if (info == null)
                return false;
            else
                return true;
        }

        public bool AddOrUpdate(int userid)
        {
            try
            {
                List<UserValidateInfo> list = GetModelList(" userid="+userid);

                if (list == null || list.Count < 1)
                {
                    UserValidateInfo newinfo = new UserValidateInfo();
                    newinfo.UserID = userid;
                    newinfo.LogonDate = DateTime.Now;
                    newinfo.CreatedDate = DateTime.Now;
                    newinfo.ModifiedUserID = userid.ToString();
                    newinfo.ModifiedDate = DateTime.Now;
                    newinfo.CreatedUserID = userid;
                    string pwd = Guid.NewGuid().ToString();
                    if (pwd.IndexOf("-") > -1)
                    {
                        pwd = pwd.Substring(0, pwd.IndexOf("-"));
                    }
                  //  ESP.HumanResource.Common.SendMailHelper.SendMailToUser(userid, pwd);
                    newinfo.Pwd = GetMD5(pwd);
                    Add(newinfo);
                }
                else
                {
                    UserValidateInfo model = list[0];
                    string pwd = Guid.NewGuid().ToString();
                    if (pwd.IndexOf("-") > -1)
                    {
                        pwd = pwd.Substring(0, pwd.IndexOf("-"));
                    }
                    model.Pwd = GetMD5(pwd);
                   // ESP.HumanResource.Common.SendMailHelper.SendMailToUser(userid, pwd);
                    dal.Update(model);
                    ESP.Logging.Logger.Add("Update UserValidateInfo is success.", "HumanResource", ESP.Logging.LogLevel.Information);

                }
                return true;
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.ToString(), "HumanResource", ESP.Logging.LogLevel.Error, ex);
                return false;
            }
        }

        private string GetMD5(string input)
        {
            MD5 md5 = MD5.Create();
            string result = "";
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            for (int i = 0; i < data.Length; i++)
            {
                result += data[i].ToString("x2");
            }
            return result;
        }



		#endregion  成员方法
    }
}
