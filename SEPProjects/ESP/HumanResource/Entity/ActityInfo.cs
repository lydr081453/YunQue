using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 培训活动实体类（T_Actity）
    /// </summary>
    public class ActityInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 培训标题
        /// </summary>
        public string ActityTitle { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        public string ActityContent { get; set; }
        /// <summary>
        /// 培训时间
        /// </summary>
        public DateTime ActityTime { get; set; }
        /// <summary>
        /// 培训讲师
        /// </summary>
        public string Lecturer { get; set; }
    }
}
