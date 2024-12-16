using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Reflection;

namespace ESP
{
    /// <summary>
    /// 树型数据结构
    /// </summary>
    /// <typeparam name="T">树节点的值的类型</typeparam>
    public class Tree<T> : IEnumerable, IEnumerable<Tree<T>> where T : class
    {
        #region Fields
        private ArrayList _children;
        private Tree<T> _parent;
        private object _Key;
        private string _Text;
        private T _Value;
        #endregion

        #region Private Methods
        private void NodeToXml(XmlWriter writer, Tree<T> node, PropertyInfo[] properties)
        {
            writer.WriteStartElement("Tree");
            foreach (PropertyInfo p in properties)
            {
                object val = p.GetValue(node.Value, null);
                if (val != null)
                    writer.WriteAttributeString(p.Name, val.ToString());
            }
            if (node.Count > 0)
            {
                for (int i = 0; i < node.Count; i++)
                {
                    NodeToXml(writer, node[i], properties);
                }
            }
            writer.WriteEndElement();
        }
        #endregion

        #region Properties

        /// <summary>
        /// 父节点
        /// </summary>
        public Tree<T> Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// 按索引获取子节点
        /// </summary>
        /// <param name="index">子节点的索引</param>
        /// <returns>子节点</returns>
        public Tree<T> this[int index]
        {
            get { return this._children[index] as Tree<T>; }
        }

        /// <summary>
        /// 当前节点的子节点的数量
        /// </summary>
        public int Count
        {
            get { return this._children.Count; }
        }

        /// <summary>
        /// 树节点的键
        /// </summary>
        public object Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        /// <summary>
        /// 树节点的文本
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        /// <summary>
        /// 树节点的值
        /// </summary>
        public T Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        #endregion

        #region public method
        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="node">要删除的子节点</param>
        public void RemoveChild(Tree<T> node)
        {
            this._children.Remove(node);
            node._parent = null;
        }

        /// <summary>
        /// 删除指定位置的子节点
        /// </summary>
        /// <param name="index">要删除的子节点的位置索引</param>
        public void RemoveChildAt(int index)
        {
            Tree<T> node = this._children[index] as Tree<T>;
            this._children.RemoveAt(index);
            node._parent = null;
        }

        /// <summary>
        /// 添加一个子节点
        /// </summary>
        /// <param name="node">要添加的子节点</param>
        public void AddChild(Tree<T> node)
        {
            this.InsertChild(this._children.Count, node);
        }

        /// <summary>
        /// 插入一个子节点
        /// </summary>
        /// <param name="index">插入位置</param>
        /// <param name="node">要插入的子节点</param>
        public void InsertChild(int index, Tree<T> node)
        {
            if (node._parent != null)
                node._parent._children.Remove(node);

            this._children.Insert(index, node);

            node._parent = this;
        }

        /// <summary>
        /// 生成Xml
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            StringWriter innerWriter = new StringWriter();
            XmlWriter writer = new System.Xml.XmlTextWriter(innerWriter);

            NodeToXml(writer, this, properties);

            writer.Flush();
            writer.Close();

            return innerWriter.GetStringBuilder().ToString();
        }

        #endregion

        #region Constructor
        /// <summary>
        /// 构造一个树节点
        /// </summary>
        /// <param name="key">节点的键</param>
        /// <param name="text">节点的文本</param>
        /// <param name="value">节点的值</param>
        public Tree(object key, string text, T value)
        {
            _parent = null;
            _children = new ArrayList();
            _Key = key;
            _Text = text;
            _Value = value;
        }
        #endregion

        #region Inner TreeEnumerator Class
        private class TreeEnumerator : IEnumerator<Tree<T>>
        {
            Tree<T> _owner = null;
            int _index = -1;

            public TreeEnumerator(Tree<T> owner)
            {
                _owner = owner;
            }

            #region IEnumerator 成员

            public Tree<T> Current
            {
                get
                {
                    if (_index < 0 || _index >= _owner._children.Count)
                        return null;

                    return _owner._children[_index] as Tree<T>;
                }
            }

            public bool MoveNext()
            {
                if (_index >= _owner._children.Count - 1)
                {
                    return false;
                }
                _index++;
                return true;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {
            }


            #endregion


            #region IEnumerator 成员

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            #endregion
        }
        #endregion

        #region IEnumerable 成员



        #endregion

        #region IEnumerable<T> 成员

        /// <summary>
        /// 获取子节点的枚举器
        /// </summary>
        /// <returns>枚举器对象</returns>
        public IEnumerator<Tree<T>> GetEnumerator()
        {
            return new TreeEnumerator(this);
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TreeEnumerator(this);
        }

        #endregion
    }


}
