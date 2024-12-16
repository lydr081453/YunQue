using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP
{
    /// <summary>
    /// 用于存储三个相关元素的类。
    /// </summary>
    /// <typeparam name="TFirst">第一个元素的类型</typeparam>
    /// <typeparam name="TSecond">第二个元素的类型</typeparam>
    /// <typeparam name="TThird">第三个元素的类型</typeparam>
    /// <typeparam name="TFourth">第四个元素的类型</typeparam>
    public class Quaternary<TFirst, TSecond, TThird, TFourth>
    {
                /// <summary>
        /// 默认构造方法
        /// </summary>
        public Quaternary()
        {
        }

        /// <summary>
        /// 构造一个三元对象
        /// </summary>
        /// <param name="first">第一个元素</param>
        /// <param name="second">第二个元素</param>
        /// <param name="third">第三个元素</param>
        /// <param name="fourth">第四个元素</param>
        public Quaternary(TFirst first, TSecond second, TThird third, TFourth fourth)
        {
            this.First = first;
            this.Second = second;
            this.Third = third;
            Fourth = fourth;
        }

        /// <summary>
        /// 获取或设置第一个元素
        /// </summary>
        public TFirst First { get; set; }

        /// <summary>
        /// 获取或设置第二个元素
        /// </summary>
        public TSecond Second { get; set; }

        /// <summary>
        /// 获取或设置第三个元素
        /// </summary>
        public TThird Third { get; set; }


        /// <summary>
        /// 获取或设置第四个元素
        /// </summary>
        public TFourth Fourth { get; set; }
    }
}
