using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 分数规则表实体类(T_ScoreStandard)
    /// </summary>
    public class ScoreStandardInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 规则描述
        /// </summary>
        public string ScoreDesc { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public string Score{ get; set; }
    }
}
