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
    public class Triplet<TFirst, TSecond, TThird>
    {
        /// <summary>
        /// 默认构造方法
        /// </summary>
        public Triplet()
        {
        }

        /// <summary>
        /// 构造一个三元对象
        /// </summary>
        /// <param name="first">第一个元素</param>
        /// <param name="second">第二个元素</param>
        /// <param name="third">第三个元素</param>
        public Triplet(TFirst first, TSecond second, TThird third)
        {
            this.First = first;
            this.Second = second;
            this.Third = third;
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
    }
}
