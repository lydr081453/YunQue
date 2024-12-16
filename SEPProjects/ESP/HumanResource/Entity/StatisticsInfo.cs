using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class StatisticsInfo
    {
        public StatisticsInfo()
        { }

        private string compayName;
        private string departmentName;
        private string groupName;
        private int mensNumByDep = 0;
        private double mensRateByDep = 0;
        private double mensRateByCo = 0;
        private int ladysNumByDep = 0;
        private double ladysRateByDep = 0;
        private double ladysRateByCo = 0;
        private int unknownsNumByDep = 0;
        private double unknownsRateByDep = 0;
        private double unknownsRateByCo = 0;
        private int groupID = 0;
        private int departmentID = 0;
        private int compayID = 0;
        private int marriedNumByDep = 0;
        private double marriedRateByDep = 0;
        private double marriedRateByCo = 0;
        private int unmarriedNumByDep = 0;
        private double unmarriedRateByDep = 0;
        private double unmarriedRateByCo = 0;
        private int unknow2NumByDep = 0;
        private double unknow2RateByDep = 0;
        private double unknow2RateByCo = 0;
        private int l60NumByDep = 0;
        private double l60RateByDep = 0;
        private double l60RateByCo = 0;
        private int y60NumByDep = 0;
        private double y60RateByDep = 0;
        private double y60RateByCo = 0;
        private int y70NumByDep = 0;
        private double y70RateByDep = 0;
        private double y70RateByCo = 0;
        private int y80NumByDep = 0;
        private double y80RateByDep = 0;
        private double y80RateByCo = 0;
        private int y90NumByDep = 0;
        private double y90RateByDep = 0;
        private double y90RateByCo = 0;
        private int y2kNumByDep = 0;
        private double y2kRateByDep = 0;
        private double y2kRateByCo = 0;
        private int unknow3NumByDep = 0;
        private double unknow3RateByDep = 0;
        private double unknow3RateByCo = 0;


        /// <summary>
        ///公司名称
        /// </summary>
        public string CompayName
        {
            get { return compayName; }
            set { compayName = value; }
        }

        /// <summary>
        ///部门名称
        /// </summary>
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        /// <summary>
        ///组别名称
        /// </summary>
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        /// <summary>
        ///部门内男性人数
        /// </summary>
        public int MensNumByDep
        {
            get { return mensNumByDep; }
            set { mensNumByDep = value; }
        }

        /// <summary>
        ///部门内男性比例
        /// </summary>
        public double MensRateByDep
        {
            get { return mensRateByDep; }
            set { mensRateByDep = value; }
        }

        /// <summary>
        ///集团内男性比例
        /// </summary>
        public double MensRateByCo
        {
            get { return mensRateByCo; }
            set { mensRateByCo = value; }
        }

        /// <summary>
        ///部门内女性人数
        /// </summary>
        public int LadysNumByDep
        {
            get { return ladysNumByDep; }
            set { ladysNumByDep = value; }
        }

        /// <summary>
        ///部门内女性比例
        /// </summary>
        public double LadysRateByDep
        {
            get { return ladysRateByDep; }
            set { ladysRateByDep = value; }
        }

        /// <summary>
        ///集团内女性比例
        /// </summary>
        public double LadysRateByCo
        {
            get { return ladysRateByCo; }
            set { ladysRateByCo = value; }
        }

        /// <summary>
        ///部门内未知性别人数
        /// </summary>
        public int UnknownsNumByDep
        {
            get { return unknownsNumByDep; }
            set { unknownsNumByDep = value; }
        }

        /// <summary>
        ///部门内未知性别比例
        /// </summary>
        public double UnknownsRateByDep
        {
            get { return unknownsRateByDep; }
            set { unknownsRateByDep = value; }
        }

        /// <summary>
        ///集团内未知性别比例
        /// </summary>
        public double UnknownsRateByCo
        {
            get { return unknownsRateByCo; }
            set { unknownsRateByCo = value; }
        }

        /// <summary>
        ///组别ID
        /// </summary>
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        /// <summary>
        ///部门ID
        /// </summary>
        public int DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        /// <summary>
        ///集团ID
        /// </summary>
        public int CompayID
        {
            get { return compayID; }
            set { compayID = value; }
        }


        /// <summary>
        ///部门内已婚人数
        /// </summary>
        public int MarriedNumByDep
        {
            get { return marriedNumByDep; }
            set { marriedNumByDep = value; }
        }

        /// <summary>
        ///部门内已婚比例
        /// </summary>
        public double MarriedRateByDep
        {
            get { return marriedRateByDep; }
            set { marriedRateByDep = value; }
        }

        /// <summary>
        ///集团内已婚比例
        /// </summary>
        public double MarriedRateByCo
        {
            get { return marriedRateByCo; }
            set { marriedRateByCo = value; }
        }

        /// <summary>
        ///部门内未婚人数
        /// </summary>
        public int UnmarriedNumByDep
        {
            get { return unmarriedNumByDep; }
            set { unmarriedNumByDep = value; }
        }

        /// <summary>
        ///部门内未婚比例
        /// </summary>
        public double UnmarriedRateByDep
        {
            get { return unmarriedRateByDep; }
            set { unmarriedRateByDep = value; }
        }

        /// <summary>
        ///集团内未婚比例
        /// </summary>
        public double UnmarriedRateByCo
        {
            get { return unmarriedRateByCo; }
            set { unmarriedRateByCo = value; }
        }

        /// <summary>
        ///部门内未知婚否人数
        /// </summary>
        public int Unknow2NumByDep
        {
            get { return unknow2NumByDep; }
            set { unknow2NumByDep = value; }
        }

        /// <summary>
        ///部门内未知婚否比例
        /// </summary>
        public double Unknow2RateByDep
        {
            get { return unknow2RateByDep; }
            set { unknow2RateByDep = value; }
        }

        /// <summary>
        ///集团内未知婚否比例
        /// </summary>
        public double Unknow2RateByCo
        {
            get { return unknow2RateByCo; }
            set { unknow2RateByCo = value; }
        }

        /// <summary>
        ///部门内60年龄段前人数
        /// </summary>
        public int L60NumByDep
        {
            get { return l60NumByDep; }
            set { l60NumByDep = value; }
        }

        /// <summary>
        ///部门内60年代前比例
        /// </summary>
        public double L60RateByDep
        {
            get { return l60RateByDep; }
            set { l60RateByDep = value; }
        }

        /// <summary>
        ///集团内60年代前比例
        /// </summary>
        public double L60RateByCo
        {
            get { return l60RateByCo; }
            set { l60RateByCo = value; }
        }

        /// <summary>
        ///部门内60年代人数
        /// </summary>
        public int Y60NumByDep
        {
            get { return y60NumByDep; }
            set { y60NumByDep = value; }
        }

        /// <summary>
        ///部门内60年代比例
        /// </summary>
        public double Y60RateByDep
        {
            get { return y60RateByDep; }
            set { y60RateByDep = value; }
        }

        /// <summary>
        ///集团内60年代比例
        /// </summary>
        public double Y60RateByCo
        {
            get { return y60RateByCo; }
            set { y60RateByCo = value; }
        }

        /// <summary>
        ///部门内70年代人数
        /// </summary>
        public int Y70NumByDep
        {
            get { return y70NumByDep; }
            set { y70NumByDep = value; }
        }

        /// <summary>
        ///部门内70年代比例
        /// </summary>
        public double Y70RateByDep
        {
            get { return y70RateByDep; }
            set { y70RateByDep = value; }
        }

        /// <summary>
        ///集团内70年代比例
        /// </summary>
        public double Y70RateByCo
        {
            get { return y70RateByCo; }
            set { y70RateByCo = value; }
        }

        /// <summary>
        ///部门内80年代人数
        /// </summary>
        public int Y80NumByDep
        {
            get { return y80NumByDep; }
            set { y80NumByDep = value; }
        }

        /// <summary>
        ///部门内80年代比例
        /// </summary>
        public double Y80RateByDep
        {
            get { return y80RateByDep; }
            set { y80RateByDep = value; }
        }

        /// <summary>
        ///集团内80年代比例
        /// </summary>
        public double Y80RateByCo
        {
            get { return y80RateByCo; }
            set { y80RateByCo = value; }
        }

        /// <summary>
        ///部门内90年代人数
        /// </summary>
        public int Y90NumByDep
        {
            get { return y90NumByDep; }
            set { y90NumByDep = value; }
        }

        /// <summary>
        ///部门内90年代比例
        /// </summary>
        public double Y90RateByDep
        {
            get { return y90RateByDep; }
            set { y90RateByDep = value; }
        }

        /// <summary>
        ///集团内90年代比例
        /// </summary>
        public double Y90RateByCo
        {
            get { return y90RateByCo; }
            set { y90RateByCo = value; }
        }

        /// <summary>
        ///部门内90年代后人数
        /// </summary>
        public int Y2kNumByDep
        {
            get { return y2kNumByDep; }
            set { y2kNumByDep = value; }
        }

        /// <summary>
        ///部门内90年代后比例
        /// </summary>
        public double Y2kRateByDep
        {
            get { return y2kRateByDep; }
            set { y2kRateByDep = value; }
        }

        /// <summary>
        ///集团内90年代后比例
        /// </summary>
        public double Y2kRateByCo
        {
            get { return y2kRateByCo; }
            set { y2kRateByCo = value; }
        }

        /// <summary>
        ///部门内未知年代人数
        /// </summary>
        public int Unknow3NumByDep
        {
            get { return unknow3NumByDep; }
            set { unknow3NumByDep = value; }
        }

        /// <summary>
        ///部门内未知年代比例
        /// </summary>
        public double Unknow3RateByDep
        {
            get { return unknow3RateByDep; }
            set { unknow3RateByDep = value; }
        }

        /// <summary>
        ///集团内未知年代比例
        /// </summary>
        public double Unknow3RateByCo
        {
            get { return unknow3RateByCo; }
            set { unknow3RateByCo = value; }
        }

        public void PopupData(IDataReader r)
        {
            this.compayName = r["level1"].ToString();
            this.departmentName = r["level2"].ToString();
            this.groupName = r["level3"].ToString();
            if (r["Male"].ToString() != "")
            {
                this.mensNumByDep = int.Parse(r["Male"].ToString());
            }
            if (r["Female"].ToString() != "")
            {
                this.ladysNumByDep = int.Parse(r["Female"].ToString());
            }
            if (r["unknow"].ToString() != "")
            {
                this.unknownsNumByDep = int.Parse(r["unknow"].ToString());
            }
            if (r["level3Id"].ToString() != "")
                this.groupID = int.Parse(r["level3Id"].ToString());
            if (r["level2Id"].ToString() != "")
                this.departmentID = int.Parse(r["level2Id"].ToString());
            if (r["level1Id"].ToString() != "")
                this.compayID = int.Parse(r["level1Id"].ToString());

        }

        public void PopupData2(IDataReader r)
        {
            this.compayName = r["level1"].ToString();
            this.departmentName = r["level2"].ToString();
            this.groupName = r["level3"].ToString();
            if (r["level3Id"].ToString() != "")
                this.groupID = int.Parse(r["level3Id"].ToString());
            if (r["level2Id"].ToString() != "")
                this.departmentID = int.Parse(r["level2Id"].ToString());
            if (r["level1Id"].ToString() != "")
                this.compayID = int.Parse(r["level1Id"].ToString());
            if (r["Married"].ToString() != "")
                this.marriedNumByDep = int.Parse(r["Married"].ToString());
            if (r["Unmarried"].ToString() != "")
                this.unmarriedNumByDep = int.Parse(r["Unmarried"].ToString());
            if (r["unknow2"].ToString() != "")
                this.unknow2NumByDep = int.Parse(r["unknow2"].ToString());

        }

        public void PopupData3(IDataReader r)
        {
            this.compayName = r["level1"].ToString();
            this.departmentName = r["level2"].ToString();
            this.groupName = r["level3"].ToString();
            if (r["level3Id"].ToString() != "")
                this.groupID = int.Parse(r["level3Id"].ToString());
            if (r["level2Id"].ToString() != "")
                this.departmentID = int.Parse(r["level2Id"].ToString());
            if (r["level1Id"].ToString() != "")
                this.compayID = int.Parse(r["level1Id"].ToString());
            if (r["lessThan60"].ToString() != "")
                this.l60NumByDep = int.Parse(r["lessThan60"].ToString());
            if (r["year60To70"].ToString() != "")
                this.y60NumByDep = int.Parse(r["year60To70"].ToString());
            if (r["year70To80"].ToString() != "")
                this.y70NumByDep = int.Parse(r["year70To80"].ToString());
            if (r["year80To90"].ToString() != "")
                this.y80NumByDep = int.Parse(r["year80To90"].ToString());
            if (r["year90To2000"].ToString() != "")
                this.y90NumByDep = int.Parse(r["year90To2000"].ToString());
            if (r["greaterThan2000"].ToString() != "")
                this.y2kNumByDep = int.Parse(r["greaterThan2000"].ToString());
            if (r["unknow3"].ToString() != "")
                this.unknow3NumByDep = int.Parse(r["unknow3"].ToString());

        }

    }
}
