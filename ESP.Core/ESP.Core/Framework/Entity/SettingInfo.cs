using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 设置信息
    /// </summary>
    public class SettingInfo
    {
        #region Private Members
        private string _ValueType = null;
        private string _SettingValue = null;
        private int _flag = 0;
        private object _TypedValue = null;
        private object TypedValue()
        {
            Type t = Type.GetType(_ValueType, false);
            if (t == null)
                return null;

            if (_SettingValue == null)
                return null;

            try
            {
                return Convert.ChangeType(_SettingValue, t);
            }
            catch(InvalidCastException)
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 设置值
        /// </summary>
        public string SettingValue
        {
            get { return _SettingValue; }
            set
            {
                _SettingValue = value;
                _flag |= 1;
                if (_flag == (1 | 2))
                {
                    _TypedValue = TypedValue();
                }
            }
        }

        /// <summary>
        /// 值类型
        /// </summary>
        public string ValueType
        {
            get { return _ValueType; }
            set
            {
                _ValueType = value;
                _flag |= 2;
                if (_flag == (1 | 2))
                {
                    _TypedValue = TypedValue();
                }
            }
        }

        /// <summary>
        /// 设置的标识
        /// </summary>
        public int DefinitionID { get; set; }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// 是否继承自公共设置
        /// </summary>
        public bool IsInherited { get; set; }


        /// <summary>
        /// 获取 T 类型的值
        /// </summary>
        /// <typeparam name="T">要获取的值的类型，如果该类型与ValueType不一致可能会引发异常。</typeparam>
        /// <returns>设置值的 T 型转换</returns>
        /// <exception cref="System.InvalidCastException">设置的值无法转换为 T 类型</exception>
        public T GetValue<T>()
        {
            return (T)_TypedValue;
        }

    }

    /// <summary>
    /// 设置项定义信息
    /// </summary>
    public class SettingDefinitionInfo
    {
        /// <summary>
        /// 设置定义的标识
        /// </summary>
        public int DefinitionID { get; set; }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string DefinitionName
        {
            get { return SettingName; }
            set { SettingName = value; }
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        public string SettingName { get; set; }


        /// <summary>
        /// 设置编辑器类型
        /// </summary>
        public SettingEditorType EditorType { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 验证表达式
        /// </summary>
        public string ValidationExpression { get; set; }

        /// <summary>
        /// 设置描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 设置针对的站点的ID，如果为0，则表示是公共设置
        /// </summary>
        public int WebSiteID { get; set; }

        /// <summary>
        /// 排序系数
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// 是否可重载
        /// </summary>
        public bool IsOverridable { get; set; }
    }

    /// <summary>
    /// 设置编辑器类型
    /// </summary>
    public enum SettingEditorType
    {
        /// <summary>
        /// 单行
        /// </summary>
        SingleLine = 1,

        /// <summary>
        /// 密码
        /// </summary>
        Password = 2,

        /// <summary>
        /// 多行
        /// </summary>
        MultiLine = 3
    }
}
