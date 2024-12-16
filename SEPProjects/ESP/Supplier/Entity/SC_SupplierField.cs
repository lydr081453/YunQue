using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierField
    {
        private int id;
        public int Id { get { return id; } set { id = value; } }

        private int supplierid;
        public int SupplierId { get { return supplierid; } set { supplierid = value; } }

        private string cityname;
        public string CityName { get { return cityname; } set { cityname = value; } }

        private string fieldname;
        public string FieldName { get { return fieldname; } set { fieldname = value; } }

        private string fieldtype;
        public string FieldType { get { return fieldtype; } set { fieldtype = value; } }

        private string fieldlevel;
        public string FieldLevel { get { return fieldlevel; } set { fieldlevel = value; } }

        private string linkername;
        public string LinkerName { get { return linkername; } set { linkername = value; } }

        private string linkertel;
        public string LinkerTel { get { return linkertel; } set { linkertel = value; } }

        private int roomcount;
        public int RoomCount { get { return roomcount; } set { roomcount = value; } }

        private string fieldaddress;
        public string FieldAddress { get { return fieldaddress; } set { fieldaddress = value; } }

        private string editusertype;
        public string EditUserType { get { return editusertype; } set { editusertype = value; } }

        private int edituserid;
        public int EditUserId { get { return edituserid; } set { edituserid = value; } }

        private DateTime addtime;
        public DateTime AddTime { get { return addtime; } set { addtime = value; } }

        private string addip;
        public string AddIp { get { return addip; } set { addip = value; } }

        private DateTime edittime;
        public DateTime EditTime { get { return edittime; } set { edittime = value; } }

        private string editip;
        public string EditIp { get { return editip; } set { editip = value; } }

        private decimal length;
        public decimal Length { get { return length; } set { length = value; } }

        private decimal width;
        public decimal Width { get { return width; } set { width = value; } }

        private decimal height;
        public decimal Height { get { return height; } set { height = value; } }

        private decimal area;
        public decimal Area { get { return area; } set { area = value; } }

        private int theaternum;
        public int TheaterNum { get { return theaternum; } set { theaternum = value; } }

        private int classroomnum;
        public int ClassroomNum { get { return classroomnum; } set { classroomnum = value; } }

        private int banquetnum;
        public int BanquetNum { get { return banquetnum; } set { banquetnum = value; } }

        private int cocktailnum;
        public int CocktailNum { get { return cocktailnum; } set { cocktailnum = value; } }

        private int boardroomnum;
        public int BoardRoomNum { get { return boardroomnum; } set { boardroomnum = value; } }

        private int ushapednum;
        public int UShapedNum { get { return ushapednum; } set { ushapednum = value; } }

        private string ballroomdesc;
        public string BallRoomDesc { get { return ballroomdesc; } set { ballroomdesc = value; } }

        private string fieldstyle;
        public string FieldStyle { get { return fieldstyle; } set { fieldstyle = value; } }

        private string casedesc;
        public string CaseDesc { get { return casedesc; } set { casedesc = value; } }

        private string applytype;
        public string ApplyType { get { return applytype; } set { applytype = value; } }

        private string price;
        public string Price { get { return price; } set { price = value; } }

        private string suppliername;
        public string SupplierName { get { return suppliername; } set { suppliername = value; } }

        private string email;
        public string Email { get { return email; } set { email = value; } }

        private string floor;
        public string Floor { get { return floor; } set { floor = value; } }

        private string xpoint;
        public string XPoint { get { return xpoint; } set { xpoint = value; } }
        private string ypoint;
        public string YPoint { get { return ypoint; } set { ypoint = value; } }
        private string maplink;
        public string MapLink { get { return maplink; } set { maplink = value; } }
        private string mapkey;
        public string MapKey { get { return mapkey; } set { mapkey = value; } }
        private bool iactive;
        public bool IsActive { get { return iactive; } set { iactive = value; } }
    }
}
