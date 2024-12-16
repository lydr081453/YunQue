using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataForm
{
    /// <summary>
    /// 标准主数据表单接口
    /// </summary>
    public interface IMainDataFormEntity
    {
        /// <summary>
        /// 数据表单序号
        /// </summary>
        object DataFormID { get; set; }
        /// <summary>
        /// 数据表单名称
        /// </summary>
        string DataFormName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        int Creator { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        string CreatorName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修订人
        /// </summary>
        int LastModifier { get; set; }
        /// <summary>
        /// 最后修订人姓名
        /// </summary>
        string LastModifierName { get; set; }
        /// <summary>
        /// 最后修订时间
        /// </summary>
        DateTime LastModifierTime { get; set; }
        /// <summary>
        /// 最后修订人IP地址
        /// </summary>
        string LastModifierIPAddress { get; set; }
        /// <summary>
        /// 数据版本
        /// </summary>
        byte[] RowVersion { get; set; }
    }
}
