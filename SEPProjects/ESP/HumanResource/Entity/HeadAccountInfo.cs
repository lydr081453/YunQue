using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class HeadAccountInfo
    {
        public int Id { get; set; }
        public int ReplaceUserId { get; set; }
        public string ReplaceUserPosition { get; set; }
        public int GroupId { get; set; }
        public int CreatorId { get; set; }
        public string Creator { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public string Position { get; set; }
        public int PositionId { get; set; }
        public int BaseId { get; set; }
        public string BaseName { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public int InterviewVPId { get; set; }
        public int OfferLetterUserId { get; set; }
        public string CostUrl { get; set; }
        public bool IsAAD { get; set; }

        public string CustomerName { get; set; }
        public string NewBiz { get; set; }
        public string ReplaceReason { get; set; }
        public DateTime? DimissionDate { get; set; }
        public string Response { get; set; }
        public string Requestment { get; set; }
        public int TalentId { get; set; }
        /// <summary>
        /// 风控审核人
        /// </summary>
        public int RCUserId { get; set; }
    }
}
