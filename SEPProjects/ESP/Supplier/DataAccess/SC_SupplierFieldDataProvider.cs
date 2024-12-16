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
    public class SC_SupplierFieldDataProvider
    {
        public int Add(SC_SupplierField model, SqlTransaction trans)
        {

            model.AddTime = DateTime.Now;
            model.EditTime = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO SC_SupplierField");
            strSql.Append(" (supplierId,cityName,fieldName,fieldType,fieldLevel,linkerName,linkerTel,roomCount,fieldAddress,editUserType,editUserId,addTime,addIp,editTime,editIp,length,width,height,area,TheaterNum,ClassroomNum,BanquetNum,CocktailNum,boardRoomNum,uShapedNum,ballRoomDesc,FieldStyle,CaseDesc,ApplyType,Price,supplierName,email,floor,XPoint,YPoint,MapLink,MapKey,IsActive)");
            strSql.Append(" VALUES");
            strSql.Append(" (@supplierId,@cityName,@fieldName,@fieldType,@fieldLevel,@linkerName,@linkerTel,@roomCount,@fieldAddress,@editUserType,@editUserId,@addTime,@addIp,@editTime,@editIp,@length,@width,@height,@area,@TheaterNum,@ClassroomNum,@BanquetNum,@CocktailNum,@boardRoomNum,@uShapedNum,@ballRoomDesc,@FieldStyle,@CaseDesc,@ApplyType,@Price,@supplierName,@email,@floor,@XPoint,@YPoint,@MapLink,@MapKey,@IsActive)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                           new SqlParameter("supplierId",SqlDbType.Int,4),
                           new SqlParameter("cityName",SqlDbType.NVarChar,50),
                           new SqlParameter("fieldName",SqlDbType.NVarChar,200),
                           new SqlParameter("fieldType",SqlDbType.NVarChar,50),
                           new SqlParameter("fieldLevel",SqlDbType.NVarChar,50),
                           new SqlParameter("linkerName",SqlDbType.NVarChar,200),
                           new SqlParameter("linkerTel",SqlDbType.NVarChar,200),
                           new SqlParameter("roomCount",SqlDbType.Int,4),
                           new SqlParameter("fieldAddress",SqlDbType.NVarChar,200),
                           new SqlParameter("editUserType",SqlDbType.NVarChar,20),
                           new SqlParameter("editUserId",SqlDbType.Int),
                           new SqlParameter("addTime",SqlDbType.DateTime),
                           new SqlParameter("addIp",SqlDbType.NVarChar,50),
                           new SqlParameter("editTime",SqlDbType.DateTime),
                           new SqlParameter("editIp",SqlDbType.NVarChar,50),
                           new SqlParameter("@length",SqlDbType.Decimal,9),
                            new SqlParameter("@width",SqlDbType.Decimal,9),
                            new SqlParameter("@height",SqlDbType.Decimal,9),
                            new SqlParameter("@area",SqlDbType.Decimal,9),
                            new SqlParameter("@TheaterNum",SqlDbType.Int),
                            new SqlParameter("@ClassroomNum",SqlDbType.Int),
                            new SqlParameter("@BanquetNum",SqlDbType.Int),
                            new SqlParameter("@CocktailNum",SqlDbType.Int),
                            new SqlParameter("@boardRoomNum",SqlDbType.Int),
                            new SqlParameter("@uShapedNum",SqlDbType.Int),
                            new SqlParameter("@ballRoomDesc",SqlDbType.NVarChar,1000),
                            new SqlParameter("@FieldStyle",SqlDbType.NVarChar,50),
                            new SqlParameter("@CaseDesc",SqlDbType.NVarChar,1000),
                            new SqlParameter("@ApplyType",SqlDbType.NVarChar,200),
                            new SqlParameter("@Price",SqlDbType.NVarChar,200),
                            new SqlParameter("@supplierName",SqlDbType.NVarChar,50),
                            new SqlParameter("@email",SqlDbType.NVarChar,50),
                            new SqlParameter("@floor",SqlDbType.NVarChar,200),
                            new SqlParameter("@XPoint",SqlDbType.NVarChar,200),
                            new SqlParameter("@YPoint",SqlDbType.NVarChar,200),
                            new SqlParameter("@MapLink",SqlDbType.NVarChar,200),
                            new SqlParameter("@MapKey",SqlDbType.NVarChar,200),
                            new SqlParameter("@IsActive",SqlDbType.Bit)
                                        };
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.CityName;
            parameters[2].Value = model.FieldName;
            parameters[3].Value = model.FieldType;
            parameters[4].Value = model.FieldLevel;
            parameters[5].Value = model.LinkerName;
            parameters[6].Value = model.LinkerTel;
            parameters[7].Value = model.RoomCount;
            parameters[8].Value = model.FieldAddress;
            parameters[9].Value = model.EditUserType;
            parameters[10].Value = model.EditUserId;
            parameters[11].Value = model.AddTime;
            parameters[12].Value = model.AddIp;
            parameters[13].Value = model.EditTime;
            parameters[14].Value = model.EditIp;
            parameters[15].Value = model.Length;
            parameters[16].Value = model.Width;
            parameters[17].Value = model.Height;
            parameters[18].Value = model.Area;
            parameters[19].Value = model.TheaterNum;
            parameters[20].Value = model.ClassroomNum;
            parameters[21].Value = model.BanquetNum;
            parameters[22].Value = model.CocktailNum;
            parameters[23].Value = model.BoardRoomNum;
            parameters[24].Value = model.UShapedNum;
            parameters[25].Value = model.BallRoomDesc;
            parameters[26].Value = model.FieldStyle;
            parameters[27].Value = model.CaseDesc;
            parameters[28].Value = model.ApplyType;
            parameters[29].Value = model.Price;
            parameters[30].Value = model.SupplierName;
            parameters[31].Value = model.Email;
            parameters[32].Value = model.Floor;
            
            parameters[33].Value = model.XPoint;
            parameters[34].Value = model.YPoint;
            parameters[35].Value = model.MapLink;
            parameters[36].Value = model.MapKey;
            parameters[37].Value = model.IsActive;

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

        public void Update(SC_SupplierField model, SqlTransaction trans)
        {
            model.EditTime = DateTime.Now;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("   UPDATE SC_SupplierField ");
            strSql.Append("   SET supplierId = @supplierId");
            strSql.Append("      ,cityName = @cityName");
            strSql.Append("      ,fieldName = @fieldName");
            strSql.Append("      ,fieldType = @fieldType");
            strSql.Append("      ,fieldLevel = @fieldLevel");
            strSql.Append("      ,linkerName = @linkerName");
            strSql.Append("      ,linkerTel = @linkerTel");
            strSql.Append("      ,roomCount = @roomCount");
            strSql.Append("      ,fieldAddress = @fieldAddress");
            strSql.Append("      ,editUserType = @editUserType");
            strSql.Append("      ,editUserId = @editUserId");
            strSql.Append("      ,addTime = @addTime");
            strSql.Append("      ,addIp = @addIp");
            strSql.Append("      ,editTime = @editTime");
            strSql.Append("      ,editIp = @editIp");
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
            strSql.Append("       ,FieldStyle = @FieldStyle");
            strSql.Append("       ,CaseDesc = @CaseDesc");
            strSql.Append("       ,ApplyType = @ApplyType");
            strSql.Append("       ,Price = @Price");
            strSql.Append("       ,supplierName=@supplierName");
            strSql.Append("       ,email=@email");
            strSql.Append("       ,floor=@floor");
            strSql.Append("       ,XPoint=@XPoint");
            strSql.Append("       ,YPoint=@YPoint");
            strSql.Append("       ,MapLink=@MapLink");
            strSql.Append("       ,MapKey=@MapKey");
            strSql.Append("       ,IsActive=@IsActive");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                           new SqlParameter("@supplierId",SqlDbType.Int,8),
                           new SqlParameter("@cityName",SqlDbType.NVarChar,200),
                           new SqlParameter("@fieldName",SqlDbType.NVarChar,200),
                           new SqlParameter("@fieldType",SqlDbType.NVarChar,200),
                           new SqlParameter("@fieldLevel",SqlDbType.NVarChar,200),
                           new SqlParameter("@linkerName",SqlDbType.NVarChar,200),
                           new SqlParameter("@linkerTel",SqlDbType.NVarChar,200),
                           new SqlParameter("@roomCount",SqlDbType.Int,4),
                           new SqlParameter("@fieldAddress",SqlDbType.NVarChar,200),
                           new SqlParameter("@editUserType",SqlDbType.NVarChar,200),
                           new SqlParameter("@editUserId",SqlDbType.Int),
                           new SqlParameter("@addTime",SqlDbType.DateTime),
                           new SqlParameter("@addIp",SqlDbType.NVarChar,50),
                           new SqlParameter("@editTime",SqlDbType.DateTime),
                           new SqlParameter("@editIp",SqlDbType.NVarChar,50),
                           new SqlParameter("@Id",SqlDbType.Int,4),
                           new SqlParameter("@length",SqlDbType.Decimal,9),
                            new SqlParameter("@width",SqlDbType.Decimal,9),
                            new SqlParameter("@height",SqlDbType.Decimal,9),
                            new SqlParameter("@area",SqlDbType.Decimal,9),
                            new SqlParameter("@TheaterNum",SqlDbType.Int),
                            new SqlParameter("@ClassroomNum",SqlDbType.Int),
                            new SqlParameter("@BanquetNum",SqlDbType.Int),
                            new SqlParameter("@CocktailNum",SqlDbType.Int),
                            new SqlParameter("@boardRoomNum",SqlDbType.Int),
                            new SqlParameter("@uShapedNum",SqlDbType.Int),
                            new SqlParameter("@ballRoomDesc",SqlDbType.NVarChar,1000),
                            new SqlParameter("@FieldStyle",SqlDbType.NVarChar,50),
                            new SqlParameter("@CaseDesc",SqlDbType.NVarChar,1000),
                            new SqlParameter("@ApplyType",SqlDbType.NVarChar,200),
                            new SqlParameter("@Price",SqlDbType.NVarChar,200),
                            new SqlParameter("@supplierName",SqlDbType.NVarChar,50),
                            new SqlParameter("@email",SqlDbType.NVarChar,50),
                            new SqlParameter("@floor",SqlDbType.NVarChar,20),
                            new SqlParameter("@XPoint",SqlDbType.NVarChar,200),
                            new SqlParameter("@YPoint",SqlDbType.NVarChar,200),
                            new SqlParameter("@MapLink",SqlDbType.NVarChar,200),
                            new SqlParameter("@MapKey",SqlDbType.NVarChar,200),
                            new SqlParameter("@IsActive",SqlDbType.Bit)
                                        };
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.CityName;
            parameters[2].Value = model.FieldName;
            parameters[3].Value = model.FieldType;
            parameters[4].Value = model.FieldLevel;
            parameters[5].Value = model.LinkerName;
            parameters[6].Value = model.LinkerTel;
            parameters[7].Value = model.RoomCount;
            parameters[8].Value = model.FieldAddress;
            parameters[9].Value = model.EditUserType;
            parameters[10].Value = model.EditUserId;
            parameters[11].Value = model.AddTime;
            parameters[12].Value = model.AddIp;
            parameters[13].Value = model.EditTime;
            parameters[14].Value = model.EditIp;
            parameters[15].Value = model.Id;
            parameters[16].Value = model.Length;
            parameters[17].Value = model.Width;
            parameters[18].Value = model.Height;
            parameters[19].Value = model.Area;
            parameters[20].Value = model.TheaterNum;
            parameters[21].Value = model.ClassroomNum;
            parameters[22].Value = model.BanquetNum;
            parameters[23].Value = model.CocktailNum;
            parameters[24].Value = model.BoardRoomNum;
            parameters[25].Value = model.UShapedNum;
            parameters[26].Value = model.BallRoomDesc;
            parameters[27].Value = model.FieldStyle;
            parameters[28].Value = model.CaseDesc;
            parameters[29].Value = model.ApplyType;
            parameters[30].Value = model.Price;
            parameters[31].Value = model.SupplierName;
            parameters[32].Value = model.Email;
            parameters[33].Value = model.Floor;
            parameters[34].Value = model.XPoint;
            parameters[35].Value = model.YPoint;
            parameters[36].Value = model.MapLink;
            parameters[37].Value = model.MapKey;
            parameters[38].Value = model.IsActive;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public SC_SupplierField GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from sc_supplierField where id=" + id);
            return ESP.Finance.Utility.CBO.FillObject<SC_SupplierField>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Delete(int id, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sc_supplierField");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id",SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public List<SC_SupplierField> GetList(int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from sc_supplierField where supplierId=" + supplierId);
            return ESP.Finance.Utility.CBO.FillCollection<SC_SupplierField>(DbHelperSQL.Query(strSql.ToString()));
        }

        public DataTable GetSupplierIds()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select distinct supplierId from sc_supplierField");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
    }
}
