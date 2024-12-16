using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.ComponentModel;
using System.Data.Common;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace ESP.Data.Linq
{
    /// <summary>
    /// 查询描述接口。
    /// </summary>
    public interface IQueryDescriptor
    {
        /// <summary>
        /// 创建一个查询上下文实例。
        /// </summary>
        /// <returns></returns>
        object CreateContext();

        /// <summary>
        /// 创建查询对象。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        /// <returns>当前查询描述对象表描述的查询。</returns>
        IQueryable CreateQuery(object context);
    }



    /// <summary>
    /// 延迟查询控制类。
    /// </summary>
    public static class DelayedQuery
    {
        /// <summary>
        /// 伪查询类， 用于进行延迟查询。
        /// </summary>
        /// <typeparam name="T">数据源中的数据项的类型。</typeparam>
        class FakeDataQuery<T> : IQueryable<T>, IQueryProvider, IListSource
        {
            /// <summary>
            /// 强制创建一个值的类型与表达式类型不一致的常量表达式。
            /// </summary>
            /// <typeparam name="TExpression">表达式的类型。</typeparam>
            /// <param name="obj">表达式的值。</param>
            /// <returns>常量表达式， 其值的实际类型可能与该表达式的类型并不匹配。</returns>
            static ConstantExpression CreateConstantExpressionWithoutValidation<TExpression>(object obj)
            {
                ConstantExpression exp = Expression.Constant(default(TExpression), typeof(TExpression));
                typeof(ConstantExpression).GetField("value", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(exp, obj);
                return exp;
            }

            /// <summary>
            /// 创建一个伪查询类。
            /// </summary>
            /// <param name="descriptor">对查询的描述。</param>
            public FakeDataQuery(IQueryDescriptor descriptor)
                : this(descriptor, CreateConstantExpressionWithoutValidation<IQueryable<T>>(descriptor))
            {
            }

            /// <summary>
            /// 创建一个伪查询类。
            /// </summary>
            /// <param name="descriptor">初始查询的描述。</param>
            /// <param name="expression">查询表达式。</param>
            FakeDataQuery(IQueryDescriptor descriptor, System.Linq.Expressions.Expression expression)
            {
                _descriptor = descriptor;
                _expression = expression;
            }

            /// <summary>
            /// 查询表达式。
            /// </summary>
            private System.Linq.Expressions.Expression _expression;

            /// <summary>
            /// 初始查询的描述。
            /// </summary>
            private IQueryDescriptor _descriptor;

            /// <summary>
            /// 获取当前查询的结果。
            /// </summary>
            /// <returns>查询结果。</returns>
            List<T> GetResult()
            {
                object context = null;
                IQueryable realQuery = CreateRealQuery(this._expression, ref context);

                try
                {
                    return new List<T>((IEnumerable<T>)realQuery);
                }
                finally
                {
                    if (context != null && context is IDisposable)
                    {
                        ((IDisposable)context).Dispose();
                        context = null;
                    }
                }
            }

            #region IEnumerable<T> 成员

            /// <summary>
            /// 获取枚举器。
            /// </summary>
            /// <returns>枚举器。</returns>
            public IEnumerator<T> GetEnumerator()
            {
                return GetResult().GetEnumerator();
            }

            #endregion

            #region IEnumerable 成员

            /// <summary>
            /// 获取枚举器。
            /// </summary>
            /// <returns>枚举器。</returns>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetResult().GetEnumerator();
            }

            #endregion

            #region IQueryable 成员

            /// <summary>
            /// 获取元素类型。
            /// </summary>
            public Type ElementType
            {
                get { return typeof(T); }
            }

            /// <summary>
            /// 获取查询表达式。
            /// </summary>
            public System.Linq.Expressions.Expression Expression
            {
                get { return _expression; }
            }

            /// <summary>
            /// 获取提供程序。
            /// </summary>
            public IQueryProvider Provider
            {
                get { return this; }
            }

            #endregion

            #region IQueryProvider 成员

            /// <summary>
            /// 用指定的查询表达式创建新的查询实例。
            /// </summary>
            /// <typeparam name="TElement">新实例的数据项的类型。</typeparam>
            /// <param name="expression">新查询的查询表达式。</param>
            /// <returns>新的查询对象实例。</returns>
            public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
            {
                if (expression == null)
                {
                    throw new ArgumentNullException("expression");
                }
                if (!typeof(IQueryable<TElement>).IsAssignableFrom(expression.Type))
                {
                    throw new ArgumentNullException("expression");
                }
                return new FakeDataQuery<TElement>(_descriptor, expression);
            }

            /// <summary>
            /// 用指定的查询表达式创建新的查询实例。
            /// </summary>
            /// <param name="expression">新查询的查询表达式。</param>
            /// <returns>新的查询对象实例。</returns>
            public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
            {
                if (expression == null)
                {
                    throw new ArgumentNullException("expression");
                }
                Type elementType = GetElementType(expression.Type);
                Type type2 = typeof(IQueryable<>).MakeGenericType(new Type[] { elementType });
                if (!type2.IsAssignableFrom(expression.Type))
                {
                    throw new ArgumentNullException("expression");
                }
                return (IQueryable)Activator.CreateInstance(typeof(FakeDataQuery<>).MakeGenericType(new Type[] { elementType }), new object[] { _descriptor, expression });
            }

            /// <summary>
            /// 执行查询表达式并返回结果不支持）。
            /// </summary>
            /// <typeparam name="TResult"></typeparam>
            /// <param name="expression"></param>
            /// <returns></returns>
            public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// 执行查询表达式并返回结果不支持）。
            /// </summary>
            /// <param name="expression"></param>
            /// <returns></returns>
            public object Execute(System.Linq.Expressions.Expression expression)
            {
                throw new NotImplementedException();
            }

            #endregion

            /// <summary>
            /// 创建实际执行查询的类的实例。
            /// </summary>
            /// <param name="expression">查询表达式。</param>
            /// <param name="context">查询上下文。</param>
            /// <returns>实际执行查询的类的实例。</returns>
            private IQueryable CreateRealQuery(Expression expression, ref object context)
            {
                ConstantExpression constant;
                MethodCallExpression methodCall;
                Expression[] methodArguments;
                object value;
                IQueryable query = null;

                try
                {
                    ExpressionType nodeType = expression.NodeType;
                    switch (nodeType)
                    {
                        case ExpressionType.Constant:
                            constant = (System.Linq.Expressions.ConstantExpression)expression;
                            value = constant.Value;
                            if (value != null && value is IQueryDescriptor)
                            {
                                IQueryDescriptor desc = (IQueryDescriptor)constant.Value;
                                if (context == null)
                                {
                                    context = desc.CreateContext();
                                }
                                return desc.CreateQuery(context);
                            }
                            break;
                        case ExpressionType.Call:
                            methodCall = (MethodCallExpression)expression;
                            methodArguments = new Expression[methodCall.Arguments.Count];
                            for (int i = 0; i < methodCall.Arguments.Count; i++)
                            {
                                Expression exp = methodCall.Arguments[i];
                                IQueryable q = CreateRealQuery(exp, ref context);
                                if (q != null)
                                {
                                    methodArguments[i] = q.Expression;
                                    if (query == null)
                                        query = q;
                                }
                                else
                                    methodArguments[i] = exp;
                            }
                            return query.Provider.CreateQuery(Expression.Call(methodCall.Object, methodCall.Method, methodArguments));
                        default:
                            break;
                    }
                }
                catch
                {
                    if (context != null && context is IDisposable)
                    {
                        ((IDisposable)context).Dispose();
                        context = null;
                    }
                    throw;
                }
                return null;
            }


            #region IListSource 成员

            /// <summary>
            /// 获取当前列表源是否包含列表的集合。
            /// </summary>
            public bool ContainsListCollection
            {
                get
                {
                    return false;
                }
            }

            /// <summary>
            /// 获取当前列表源中的列表。
            /// </summary>
            /// <returns></returns>
            public IList GetList()
            {
                return this.GetResult();
            }

            #endregion
        }

        /// <summary>
        /// 辅助类，目的不明。
        /// </summary>
        /// <param name="seqType"></param>
        /// <returns></returns>
        private static Type FindIEnumerable(Type seqType)
        {
            if ((seqType != null) && (seqType != typeof(string)))
            {
                if (seqType.IsArray)
                {
                    return typeof(IEnumerable<>).MakeGenericType(new Type[] { seqType.GetElementType() });
                }
                if (seqType.IsGenericType)
                {
                    foreach (Type type in seqType.GetGenericArguments())
                    {
                        Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
                        if (type2.IsAssignableFrom(seqType))
                        {
                            return type2;
                        }
                    }
                }
                Type[] interfaces = seqType.GetInterfaces();
                if ((interfaces != null) && (interfaces.Length > 0))
                {
                    foreach (Type type3 in interfaces)
                    {
                        Type type4 = FindIEnumerable(type3);
                        if (type4 != null)
                        {
                            return type4;
                        }
                    }
                }
                if ((seqType.BaseType != null) && (seqType.BaseType != typeof(object)))
                {
                    return FindIEnumerable(seqType.BaseType);
                }
            }
            return null;
        }

        /// <summary>
        /// 辅助类，目的不明。
        /// </summary>
        /// <param name="seqType"></param>
        /// <returns></returns>
        private static Type GetElementType(Type seqType)
        {
            Type type = FindIEnumerable(seqType);
            if (type == null)
            {
                return seqType;
            }
            return type.GetGenericArguments()[0];
        }


        /// <summary>
        /// 创建延迟查询类实例，该查询实例在创建时不需要查询上下文对象， 可以
        /// 随时执行查询，而不必被限制在查询上下文的生命周期内。
        /// </summary>
        /// <typeparam name="T">要查询的数据源中的数据项的类型。</typeparam>
        /// <param name="query">对描述的查询。</param>
        /// <returns>查询类实例。</returns>
        /// <remarks>
        /// 因为创建查询类实例时并不需要查询上下文对象， 不会像一般查询类实例
        /// 一样只能在其查询上下文对象的生命期内使用， 所以可以用来作为方法的
        /// 返回值，并在任务时候执行查询。
        /// </remarks>
        public static IQueryable<T> Create<T>(IQueryDescriptor query)
        {
            return new FakeDataQuery<T>(query);
        }

        ///// <summary>
        ///// 创建延迟查询类实例。
        ///// </summary>
        ///// <typeparam name="TContext">要查询的ObjectContext的类型。</typeparam>
        ///// <typeparam name="TEntity">要查询的实体的类型。</typeparam>
        ///// <param name="queryString">查询字符串。</param>
        ///// <returns>延迟查询实例。</returns>
        //public static IQueryable<TEntity> Create<TContext, TEntity>(string queryString)
        //    where TContext : ObjectContext
        //    where TEntity : class
        //{
        //    ObjectQueryDescriptor<TContext, TEntity> qd = new ObjectQueryDescriptor<TContext, TEntity>(queryString);
        //    return new FakeDataQuery<TEntity>(qd);
        //}

        /// <summary>
        /// 创建延迟查询类实例。
        /// </summary>
        /// <typeparam name="TEntity">要查询的实体的类型。</typeparam>
        /// <returns>延迟查询实例。</returns>
        public static IQueryable<TEntity> Create<TEntity>()
            where TEntity : class
        {
            ObjectQueryDescriptor<ObjectContext, TEntity> qd = new ObjectQueryDescriptor<ObjectContext,TEntity>();
            return new FakeDataQuery<TEntity>(qd);
        }


        ///// <summary>
        ///// 创建延迟查询类实例。
        ///// </summary>
        ///// <typeparam name="TEntity">要查询的实体的类型。</typeparam>
        ///// <param name="queryString">查询字符串。</param>
        ///// <returns>延迟查询实例。</returns>
        //public static IQueryable<TEntity> Create<TEntity>(string queryString)
        //    where TEntity : class
        //{
        //    ObjectQueryDescriptor<ObjectContext, TEntity> qd = new ObjectQueryDescriptor<ObjectContext, TEntity>(queryString);
        //    return new FakeDataQuery<TEntity>(qd);
        //}
    }
}
