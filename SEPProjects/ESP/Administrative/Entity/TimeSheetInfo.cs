using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public class TimeSheetInfo
    {
        public int Id { get; set; }
        public decimal Hours { get; set; }
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string WorkItem { get; set; }
        public DateTime CreateDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string FileUrl { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string IP { get; set; }
        public int Status { get; set; }
        public int CommitId { get; set; }
        public int TypeId { get; set; }
        public bool IsChecked { get; set; }
        public bool IsBillable { get; set; }
    }

    public class MonthInfo
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
        public string ClassName { get; set; }

        public int TimeSheetCount { get; set; }
        public decimal TimeSheetHours { get; set; }

        public int ChiDao { get; set; }
        public int ZaoTui { get; set; }
        public int KuangGong { get; set; }
        public int QingJia { get; set; }
        public int ChuChai { get; set; }
        public int WaiChu { get; set; }
        public int JiaBan { get; set; }
        public int TiaoXiu { get; set; }

        public TimeSheetCommitInfo TimeSheetCommitModel { get; set; }

        public int UserId { get; set; }
    }
}
