using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_FieldImgShunyaUserAddedDataProvider
    {
        public int Add(SC_FieldImgShunyaUserAdded model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO SC_FieldImgShunyaUserAdded ");
            strSql.Append(" (tempfieldId,largeImg,samllImg,ImgDesc) ");
            strSql.Append(" VALUES ");
            strSql.Append(" (@fieldId,@largeImg,@samllImg,@ImgDesc) ");

            SqlParameter[] parameters = { 
                           new SqlParameter("@tempfieldId",SqlDbType.Int),
                           new SqlParameter("@largeImg",SqlDbType.NVarChar,200),
                           new SqlParameter("@samllImg",SqlDbType.NVarChar,200),
                           new SqlParameter("ImgDesc",SqlDbType.NVarChar,1000)
                                        };
            parameters[0].Value = model.TempFieldId;
            parameters[1].Value = model.LargeImg;
            parameters[2].Value = model.SamllImg;
            parameters[3].Value = model.ImgDesc;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public List<SC_FieldImgShunyaUserAdded> GetList(int fieldId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SC_FieldImgShunyaUserAdded where tempfieldId=" + fieldId);
            return ESP.Finance.Utility.CBO.FillCollection<SC_FieldImgShunyaUserAdded>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Delete(int id, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_FieldImgShunyaUserAdded");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id",SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}
