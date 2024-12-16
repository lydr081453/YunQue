using System.Data.Objects;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System;
using System.Linq;

namespace ESP.Data.Linq
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObjectContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class ObjectQueryDescriptor<TObjectContext, TEntity> : IQueryDescriptor
        where TObjectContext : ObjectContext
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        public ObjectQueryDescriptor()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object CreateContext()
        {
            return new DbConnectionHolder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IQueryable CreateQuery(object context)
        {
            DbConnectionHolder holder = (DbConnectionHolder)context;
            TObjectContext objectContext = holder.GetObjectContext<TObjectContext>();

            return objectContext.GetTable<TEntity>();
        }
    }

    /*
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate object CreateContextHandler(object parameters);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate IQueryable CreateQueryHandler(object context, object parameters);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObjectContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class DelegatedDescriptor<TObjectContext, TEntity> : IQueryDescriptor
        where TObjectContext : ObjectContext
        where TEntity : EntityObject
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createContextHandler"></param>
        /// <param name="createQueryHandler"></param>
        /// <param name="parameters"></param>
        public DelegatedDescriptor(CreateContextHandler createContextHandler, CreateQueryHandler createQueryHandler, object parameters)
        {
            this.CreateContextHandler = createContextHandler;
            this.CreateQueryHandler = createQueryHandler;
            this.Parameters = parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createContextHandler"></param>
        /// <param name="createQueryHandler"></param>
        public DelegatedDescriptor(CreateContextHandler createContextHandler, CreateQueryHandler createQueryHandler)
        {
            this.CreateContextHandler = createContextHandler;
            this.CreateQueryHandler = createQueryHandler;
            this.Parameters = null;        }



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="connectionStringSettingName"></param>
        //public ObjectQueryDescriptor(string connectionStringSettingName)
        //{
        //    this.ConnectionStringSettingName = connectionStringSettingName;
        //    this.QueryString = "[" + typeof(EntityObject).Name + "]";
        //    this.Parameters = new ObjectParameter[0];
        //}

        /// <summary>
        /// 
        /// </summary>
        public CreateContextHandler CreateContextHandler { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CreateQueryHandler CreateQueryHandler { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public object Parameters { get; private set; }

        #region IQueryDescriptor 成员

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object CreateContext()
        {
            return this.CreateContextHandler(this.Parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IQueryable CreateQuery(object context)
        {
            return this.CreateQueryHandler(context, this.Parameters);
        }

        #endregion
    }
    */
}
