using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 问题表实体类(T_Question)
    /// </summary>
    public class QuestionInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int TitleId { get; set; }
        public int Status { get; set; }
    }
}
