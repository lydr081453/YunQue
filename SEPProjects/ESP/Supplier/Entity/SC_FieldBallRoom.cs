using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_FieldBallRoom
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public string BallRoomName { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Area { get; set; }
        public int TheaterNum { get; set; }
        public int ClassroomNum { get; set; }
        public int BanquetNum { get; set; }
        public int CocktailNum { get; set; }
        public int BoardRoomNum { get; set; }
        public int UShapedNum { get; set; }
        public string BallRoomDesc { get; set; }
    }
}
