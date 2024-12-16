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
    public class SC_FieldBallRoomDataProvider
    {
        public int Add(SC_FieldBallRoom model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO SC_FieldBallRoom");
            strSql.Append(" (fieldId,ballRoomName,length,width,height,area,TheaterNum,ClassroomNum,BanquetNum,CocktailNum,boardRoomNum,uShapedNum,ballRoomDesc)");
            strSql.Append(" VALUES");
            strSql.Append(" (@fieldId,@ballRoomName,@length,@width,@height,@area,@TheaterNum,@ClassroomNum,@BanquetNum,@CocktailNum,@boardRoomNum,@uShapedNum,@ballRoomDesc)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = {
                            new SqlParameter("@fieldId",SqlDbType.Int),
                            new SqlParameter("@ballRoomName",SqlDbType.NVarChar,50),
                            new SqlParameter("@length",SqlDbType.Float,8),
                            new SqlParameter("@width",SqlDbType.Float,8),
                            new SqlParameter("@height",SqlDbType.Float,8),
                            new SqlParameter("@area",SqlDbType.Float,8),
                            new SqlParameter("@TheaterNum",SqlDbType.Int),
                            new SqlParameter("@ClassroomNum",SqlDbType.Int),
                            new SqlParameter("@BanquetNum",SqlDbType.Int),
                            new SqlParameter("@CocktailNum",SqlDbType.Int),
                            new SqlParameter("@boardRoomNum",SqlDbType.Int),
                            new SqlParameter("@uShapedNum",SqlDbType.Int),
                            new SqlParameter("@ballRoomDesc",SqlDbType.NVarChar,1000)
                                        };
            parameters[0].Value = model.FieldId;
            parameters[1].Value = model.BallRoomName;
            parameters[2].Value = model.Length;
            parameters[3].Value = model.Width;
            parameters[4].Value = model.Height;
            parameters[5].Value = model.Area;
            parameters[6].Value = model.TheaterNum;
            parameters[7].Value = model.ClassroomNum;
            parameters[8].Value = model.BanquetNum;
            parameters[9].Value = model.CocktailNum;
            parameters[10].Value = model.BoardRoomNum;
            parameters[11].Value = model.UShapedNum;
            parameters[12].Value = model.BallRoomDesc;

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

        public void Update(SC_FieldBallRoom model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE SC_FieldBallRoom ");
            strSql.Append("    SET fieldId = @fieldId");
            strSql.Append("       ,ballRoomName = @ballRoomName");
            strSql.Append("       ,length = @length");
            strSql.Append("       ,width = @width");
            strSql.Append("       ,height = @height");
            strSql.Append("       ,area = @area");
            strSql.Append("       ,TheaterNum = @TheaterNum");
            strSql.Append("       ,ClassroomNum = @ClassroomNum");
            strSql.Append("       ,BanquetNum = @BanquetNum");
            strSql.Append("       ,CocktailNum = @CocktailNum");
            strSql.Append("       ,boardRoomNum = @boardRoomNum");
            strSql.Append("       ,uShapedNum = @uShapedNum");
            strSql.Append("       ,ballRoomDesc = @ballRoomDesc");
            strSql.Append("  WHERE id=@id");

            SqlParameter[] parameters = {
                            new SqlParameter("@fieldId",SqlDbType.Int),
                            new SqlParameter("@ballRoomName",SqlDbType.NVarChar,50),
                            new SqlParameter("@length",SqlDbType.Float,8),
                            new SqlParameter("@width",SqlDbType.Float,8),
                            new SqlParameter("@height",SqlDbType.Float,8),
                            new SqlParameter("@area",SqlDbType.Float,8),
                            new SqlParameter("@TheaterNum",SqlDbType.Int),
                            new SqlParameter("@ClassroomNum",SqlDbType.Int),
                            new SqlParameter("@BanquetNum",SqlDbType.Int),
                            new SqlParameter("@CocktailNum",SqlDbType.Int),
                            new SqlParameter("@boardRoomNum",SqlDbType.Int),
                            new SqlParameter("@uShapedNum",SqlDbType.Int),
                            new SqlParameter("@ballRoomDesc",SqlDbType.NVarChar,1000),
                            new SqlParameter("@id",SqlDbType.Int)
                                        };
            parameters[0].Value = model.FieldId;
            parameters[1].Value = model.BallRoomName;
            parameters[2].Value = model.Length;
            parameters[3].Value = model.Width;
            parameters[4].Value = model.Height;
            parameters[5].Value = model.Area;
            parameters[6].Value = model.TheaterNum;
            parameters[7].Value = model.ClassroomNum;
            parameters[8].Value = model.BanquetNum;
            parameters[9].Value = model.CocktailNum;
            parameters[10].Value = model.BoardRoomNum;
            parameters[11].Value = model.UShapedNum;
            parameters[12].Value = model.BallRoomDesc;
            parameters[13].Value = model.Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public SC_FieldBallRoom GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SC_FieldBallRoom where id=" + id);
            return ESP.Finance.Utility.CBO.FillObject<SC_FieldBallRoom>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<SC_FieldBallRoom> GetList(int fieldId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SC_FieldBallRoom where fieldId=" + fieldId);
            return ESP.Finance.Utility.CBO.FillCollection<SC_FieldBallRoom>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Delete(int id,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_FieldBallRoom");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id",SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}
