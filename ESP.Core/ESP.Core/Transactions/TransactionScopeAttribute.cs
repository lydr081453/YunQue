//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using PostSharp.Laos;
//using System.Transactions;
//namespace ESP.Transactions
//{
//    /// <summary>
//    /// transactionScope 跨事务操作属性标签
//    /// </summary>
//    [Serializable]
//    public sealed class TransactionScopeAttribute : OnMethodBoundaryAspect
//    {
//        //[NonSerialized]
//        //TransactionScope _scope = null;

//        /// <summary>
//        /// 已重载。Method executed before the body of methods to which this aspect is applied.
//        /// </summary>
//        /// <param name="eventArgs"></param>
//        public override void OnEntry(MethodExecutionEventArgs eventArgs)
//        {
//            if (System.Transactions.Transaction.Current == null)
//            {
//                TransactionOptions transactionOptions = new TransactionOptions();
//                TransactionScopeOption transactionScopeOption = TransactionScopeOption.Required;
//                EnterpriseServicesInteropOption interopOption = EnterpriseServicesInteropOption.Automatic;
//                eventArgs.MethodExecutionTag = new TransactionScope(transactionScopeOption, transactionOptions, interopOption);
//            }
//        }

//        /// <summary>
//        /// 在方法的结束位置注入
//        /// </summary>
//        /// <param name="eventArgs"></param>
//        public override void OnExit(MethodExecutionEventArgs eventArgs)
//        {
//            TransactionScope scope = eventArgs.MethodExecutionTag as TransactionScope;
//            if (scope == null)
//                return;

//            using (scope)
//            {
//                if (eventArgs.Exception == null)
//                    scope.Complete();
//            }
//        }
//    }
//}
