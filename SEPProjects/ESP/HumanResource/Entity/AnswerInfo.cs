using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 答案表实体类(T_Answer)
    /// </summary>
    public class AnswerInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 答案分数
        /// </summary>
        public int Score { get; set; }
        public string AnswerText { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AnswerTime { get; set; }
        public string Ip { get; set; }
    }
}
