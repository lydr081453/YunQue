using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common
{
    /// <summary>
    /// 定容量值唯一队列，超出容量则会执行出队列操作
    /// 判定入队列的值和对列内值是否相同的依据是ToString()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UniqueQueue<T>
    {
        //为了同步而用来锁定的对象
        private object obj = new object();
        //存储对象的数组列表（使用链表存储是否更好值得商榷）
        private T[] _array = null;
        //尾部指针
        private int _point = 0;
        /// <summary>
        /// 当前队列元素容量
        /// </summary>
        public int Count
        {
            get { return _point + 1; }
        }
        /// <summary>
        /// 获取当前元素队列
        /// </summary>
        public List<T> List
        {
            get
            {
                List<T> list = new List<T>();
                for (int i = 0; i <= _point; ++i)
                {
                    list.Add(_array[i]);
                }
                return list;
            }
        }
        /// <summary>
        /// 需要输入一个队列长度
        /// </summary>
        /// <param name="capacity"></param>
        public UniqueQueue(int capacity)
        {
            if (capacity < 1)
            {
                throw new Exception("不能输入容量小于1的队列");
            }
            _array = new T[capacity];
        }

        /// <summary>
        /// 入队列操作
        /// </summary>
        /// <param name="t">入队列对象</param>
        /// <returns>如果造成队列溢出，则返回溢出的对象，否则返回入队列对象</returns>
        public T Enqueue(T t)
        {
            //同一个对象的操作要同步
            lock (obj)
            {
                //查询t是否已经在当前队列中，如果在则取得t所处的位置
                int i = Have(t);
                //如果t就在队列的头部，则无需操作，直接返回
                if (i == 0) return t;
                //如果不存在t，则进行常规操作
                if (i < 0)
                {
                    T dt = t;
                    int n = 0;
                    if(_point == _array.Length)
                    {
                        _point = _array.Length;
                        dt = _array[_array.Length];
                        n = _point;
                    }
                    else n = ++_point;
                    for (; n > 0; n--)
                    {
                        _array[n] = _array[n - 1];
                    }
                    _array[0] = t;
                    return dt;
                }
                //如果已经存在t，需要从t所在的位置（i）开始移动队列，把（i-1）的对象移动到（i），以此类推，直到移动到头部，然后加入对象t到头部，尾部指针（_point）不移动
                int j = i;
                for (; j > 0; j--)
                {
                    _array[j] = _array[j - 1];
                }
                _array[0] = t;
                return t;
            }
        }
        /// <summary>
        /// 出队列操作，移动尾部指针向前
        /// </summary>
        /// <returns>返回出队列的对象，如果没有对象则返回一个默认值（default(T)）</returns>
        public T Dequeue()
        {
            if (_point == 0) return default(T);
            return _array[_point--];
        }

        /// <summary>
        /// 队列中是否包含t，如果包含则返回其所在的位置，如过不包含返回-1
        /// </summary>
        /// <param name="t">查询的值对象</param>
        /// <returns>如果包含则返回其所在的位置，如过不包含返回-1</returns>
        private int Have(T t)
        {
            for (int i = 0; i < _array.Length; ++i)
            {
                if (_array[i].ToString() == t.ToString())
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
