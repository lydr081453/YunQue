using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    public class AddressBookDataProvider
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<AddressItemInfo> GetData(int id, int parentId)
        {
            IList<AddressItemInfo> items = new List<AddressItemInfo>();
            string sql = @"SELECT i.Id,i.UserId,i.AddressBookId,i.UserName,i.ENName,i.Tel,i.Mobile,i.Position,i.DepartmentName,i.Email,i.DepartmentId
                           FROM sep_addressitem i
                           LEFT JOIN sep_Departments dep ON dep.DepartmentID = i.DepartmentID 
                           where AddressBookId=@id and dep.parentId in 
                           (select departmentId from sep_departments where parentId=@parentId)";
            SqlParameter param = new SqlParameter("@id", id);
            SqlParameter param2 = new SqlParameter("@parentId", parentId);
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql, param, param2);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;

            AddressItemInfo item = null;
            foreach (DataRow row in drs)
            {
                item = new AddressItemInfo();
                if (row["Id"].ToString().Trim() != "")
                {
                    item.Id = int.Parse(row["Id"].ToString());
                }
                if (row["userid"].ToString().Trim() != "")
                {
                    item.UserId = int.Parse(row["userid"].ToString());
                }
                if (row["AddressBookId"].ToString().Trim() != "")
                {
                    item.AddressBookId = int.Parse(row["AddressBookId"].ToString());
                }
                item.UserName = row["UserName"].ToString();
                item.ENName = row["ENName"].ToString();
                item.Tel = row["Tel"].ToString();
                item.Mobile = row["Mobile"].ToString();
                item.Position = row["Position"].ToString();
                item.DepartmentName = row["DepartmentName"].ToString();
                item.Email = row["Email"].ToString();
                if (row["DepartmentId"].ToString().Trim() != "")
                {
                    item.DepartmentId = int.Parse(row["DepartmentId"].ToString());
                }
                items.Add(item);
            }
            return items;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<AddressBookInfo> GetList(string where)
        {
            IList<AddressBookInfo> list = new List<AddressBookInfo>();
            string sql = "select * from sep_addressbook where 1=1 ";
            sql += where;
            sql += " order by createdate desc";
            DataSet ds = ESP.HumanResource.Common.DbHelperSQL.Query(sql);
            DataTable dt = ds.Tables[0];
            DataRowCollection drs = dt.Rows;
            foreach (DataRow row in drs)
            {
                AddressBookInfo item = new AddressBookInfo();
                if (row["Id"].ToString() != "")
                    item.Id = int.Parse(row["Id"].ToString());
                if (row["CreateId"].ToString() != "")
                    item.CreateId = int.Parse(row["CreateId"].ToString());
                item.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                item.Version = row["Version"].ToString();
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 添加通讯录
        /// </summary>
        /// <param name="addressBook">要添加的通讯录对象</param>
        /// <returns></returns>
        public int Add(AddressBookInfo addressBook)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_AddressBook(");
            strSql.Append("CreateId,CreateDate,Version)");
            strSql.Append(" values (");
            strSql.Append("@CreateId,getdate(),@Version)");
            strSql.Append("SELECT @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@CreateId", SqlDbType.Int),
					new SqlParameter("@Version", SqlDbType.NVarChar)};
            parameters[0].Value = addressBook.CreateId;
            parameters[1].Value = addressBook.Version;
            object obj = ESP.HumanResource.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            return 0;
        }
        /// <summary>
        /// 添加通讯录子项
        /// </summary>
        /// <param name="id">AddressBookId</param>
        /// <returns></returns>
        public bool AddItem(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_AddressItem(");
            strSql.Append("UserId,UserName,Position,ENName,Mobile,Tel,Email,DepartmentId,DepartmentName,AddressBookId) ");
            strSql.Append(@"SELECT u.userid as UserId,u.lastnamecn+u.firstnamecn as UserName,P.DepartmentPositionName as Position,u.lastnameen+u.firstnameen as ENName,e.MobilePhone as Mobile,e.Phone1 as Tel,e.internalemail as Email,dep.DepartmentID,dep.DepartmentName,@AddressBookId 
            FROM sep_users u
            LEFT JOIN sep_employees e on u.userid=e.userid  
            LEFT JOIN SEP_EmployeesInPositions eip ON u.userid = eip.userid
            LEFT JOIN SEP_DepartmentPositions p ON p.DepartmentPositionid=eip.Departmentpositionid
            LEFT JOIN sep_Departments dep ON dep.DepartmentID = eip.DepartmentID 
            WHERE e.status in (1,2,3,4,7,8) and u.isDeleted=0 AND dep.DepartmentName not like '%作废%'
            ORDER BY dep.DepartmentId asc,userId ");
            SqlParameter parameter = new SqlParameter("@AddressBookId", id);
            object obj = ESP.HumanResource.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
            if (obj != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteAll(int id)
        {
            string sql = "delete from sep_addressitem where addressbookid=@id;";
            sql += "delete from sep_addressbook where id=@id;";
            SqlParameter param = new SqlParameter("@id", id);
            try
            {
                int num = DbHelperSQL.ExecuteSql(sql, param);
                if (num > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
    }
}
