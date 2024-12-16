using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace AdminForm.Data
{
    internal class SelectCommandBuilder : CommandBuilder
    {
        public SelectCommandBuilder(string commandTextTemplate, TableMappings tableMappings, IEnumerable<LambdaExpression> predicate, LambdaExpression[] orderby)
            : base(commandTextTemplate, tableMappings)
        {
            _Predicate = predicate;
            _OrderBy = orderby;
        }

        LambdaExpression[] _OrderBy;
        IEnumerable<LambdaExpression> _Predicate;

        protected override void ProcessToken(StringBuilder builder, string token)
        {

            if (PredicatePlaceHolder == token)
                TranslatePredicate(builder, _Predicate);
            else if (OrderByPlaceHolder == token)
                TranslateOrderBy(builder, _OrderBy);
        }
    }

    internal class DeleteCommandBuilder : CommandBuilder
    {
        public DeleteCommandBuilder(string commandTextTemplate, TableMappings tableMappings, LambdaExpression predicate)
            : base(commandTextTemplate, tableMappings)
        {
            _Predicate = predicate;
        }

        LambdaExpression _Predicate;

        protected override void ProcessToken(StringBuilder builder, string token)
        {
            if (PredicatePlaceHolder == token)
                TranslatePredicate(builder, _Predicate);
        }
    }

    internal class InsertCommandBuilder<T> : CommandBuilder
    {
        public InsertCommandBuilder(string tableName, T record, bool selectIdentity, Expression<Func<T, KeySelector>>[] exclude)
            : base("@@@INSERT@@@", null)
        {
            this._TableName = QuoteName(tableName);
            this._Record = record;
            this._SelectIdentity = selectIdentity;
            this._Exclude = exclude;
        }

        Expression<Func<T, KeySelector>>[] _Exclude;
        string _TableName;
        object _Record;
        bool _SelectIdentity;

        protected override void ProcessToken(StringBuilder builder, string token)
        {
            var values = new List<object>();

            var properties = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

            var excludeNames = new HashSet<string>();

            if (this._Exclude != null)
            {
                foreach (var keySelector in _Exclude)
                {
                    var body = keySelector.Body;
                    var updateCall = (MethodCallExpression)body;

                    if (updateCall.Method.DeclaringType != typeof(UpdateExtensions))
                    {
                        throw new InvalidCastException();
                    }

                    var member = (MemberExpression)updateCall.Arguments[0];
                    excludeNames.Add(member.Member.Name);
                }

            }

            builder.Append("INSERT INTO ").Append(_TableName).Append(" (");

            for (var i = properties.Count - 1; i >= 0; i--)
            {
                System.ComponentModel.PropertyDescriptor p = properties[i];
                if (!excludeNames.Contains(p.Name))
                {
                    values.Add(p.GetValue(_Record));
                    builder.Append("[").Append(p.Name).Append("],");
                }
            }
            builder.Length--;

            builder.Append(") VALUES (");

            for (var i = 0; i < values.Count; i++)
            {
                var v = values[i];
                BuildConstant(builder, v);
                builder.Append(",");
            }
            builder.Length--;

            builder.Append(")");

            if (this._SelectIdentity)
                builder.Append(" SELECT SCOPE_IDENTITY()");
        }
    }

    internal class UpdateAllColumnsCommandBuilder<T> : CommandBuilder
    {
        LambdaExpression[] _KeySelectors;
        Expression<Func<T, KeySelector>>[] _Exclude;
        string _TableName;
        object _Record;

        public UpdateAllColumnsCommandBuilder(string tableName, T record, Expression<Func<T, KeySelector>>[] keySelectors, Expression<Func<T, KeySelector>>[] exclude)
            : base("@@@UPDATE@@@", null)
        {
            this._KeySelectors = keySelectors;
            this._TableName = QuoteName(tableName);
            this._Record = record;
            this._Exclude = exclude;
        }

        protected override void ProcessToken(StringBuilder builder, string token)
        {
            var keyNames = new HashSet<string>();
            var excludeNames = new HashSet<string>();

            var keyProperties = new List<System.ComponentModel.PropertyDescriptor>();

            foreach (var keySelector in _KeySelectors)
            {
                var body = keySelector.Body;
                var updateCall = (MethodCallExpression)body;

                if (updateCall.Method.DeclaringType != typeof(UpdateExtensions))
                {
                    throw new InvalidCastException();
                }

                var member = (MemberExpression)updateCall.Arguments[0];
                keyNames.Add(member.Member.Name);
            }

            if (this._Exclude != null)
            {
                foreach (var keySelector in _Exclude)
                {
                    var body = keySelector.Body;
                    var updateCall = (MethodCallExpression)body;

                    if (updateCall.Method.DeclaringType != typeof(UpdateExtensions))
                    {
                        throw new InvalidCastException();
                    }

                    var member = (MemberExpression)updateCall.Arguments[0];
                    excludeNames.Add(member.Member.Name);
                }

            }

            var properties = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

            builder.Append("UPDATE ").Append(_TableName).Append(" SET ");


            for (var i = properties.Count - 1; i >= 0; i--)
            {
                System.ComponentModel.PropertyDescriptor p = properties[i];
                if (keyNames.Contains(p.Name))
                {
                    keyProperties.Add(p);
                }
                else if (!excludeNames.Contains(p.Name))
                {
                    builder.Append("[").Append(p.Name).Append("]=");
                    BuildConstant(builder, p.GetValue(_Record));
                    builder.Append(",");
                }
            }
            builder.Length--;

            builder.Append(" WHERE ");

            for (var i = keyProperties.Count - 1; i >= 0; i--)
            {
                var p = keyProperties[i];
                builder.Append("[").Append(p.Name).Append("]=");
                BuildConstant(builder, p.GetValue(_Record));
                builder.Append(" AND ");
            }

            builder.Length -= " AND ".Length;
        }

    }

    internal class UpdateCommandBuilder : CommandBuilder
    {
        LambdaExpression[] _Columns;
        LambdaExpression _Predicate;

        public UpdateCommandBuilder(string commandTextTemplate, TableMappings tableMappings, LambdaExpression predicate, LambdaExpression[] columns)
            : base(commandTextTemplate, tableMappings)
        {
            _Predicate = predicate;
            _Columns = columns;
        }

        protected override void ProcessToken(StringBuilder builder, string token)
        {
            if (PredicatePlaceHolder == token)
                TranslatePredicate(builder, _Predicate);
            else if (UpdatePlaceHolder == token)
                TranslateUpdate(builder, _Predicate, _Columns);
        }

    }

    public class CommandInfo
    {
        internal CommandInfo(string commandText, System.Data.IDataParameter[] parameters)
        {
            CommandText = commandText;
            Parameters = parameters;
        }

        public string CommandText { get; private set; }
        public System.Data.IDataParameter[] Parameters { get; private set; }
    }

    public abstract class CommandBuilder
    {
        public static CommandInfo BuildSelectCommand(string commandTextTemplate, LambdaExpression predicate)
        {
            return BuildSelectCommand(commandTextTemplate, predicate, null, null);
        }

        public static CommandInfo BuildSelectCommand(string commandTextTemplate, LambdaExpression predicate, LambdaExpression[] orderby)
        {
            return BuildSelectCommand(commandTextTemplate, predicate, orderby, null);
        }

        public static CommandInfo BuildSelectCommand<T>(string commandTextTemplate, Expression<Func<T, bool>> predicate, params Expression<Func<T, OrderBy>>[] orderby)
        {
            return BuildSelectCommand(commandTextTemplate, predicate, orderby, null);
        }

        public static CommandInfo BuildSelectCommand(string commandTextTemplate, LambdaExpression predicate, TableMappings tableMappings)
        {
            return BuildSelectCommand(commandTextTemplate, predicate, null, tableMappings);
        }

        public static CommandInfo BuildSelectCommand(string commandTextTemplate, LambdaExpression predicate, LambdaExpression[] orderby, TableMappings tableMappings)
        {
            List<LambdaExpression> ps = new List<LambdaExpression>();
            if(predicate != null)
                ps.Add(predicate);
            return BuildSelectCommand(commandTextTemplate, ps, orderby);
        }

        public static CommandInfo BuildSelectCommand(string commandTextTemplate, IEnumerable<LambdaExpression> predicate, LambdaExpression[] orderby)
        {
            return BuildSelectCommand(commandTextTemplate, predicate, orderby, null);
        }

        public static CommandInfo BuildSelectCommand(string commandTextTemplate, IEnumerable<LambdaExpression> predicate, LambdaExpression[] orderby, TableMappings tableMappings)
        {
            SelectCommandBuilder builder = new SelectCommandBuilder(commandTextTemplate, tableMappings, predicate, orderby);
            builder.Build();
            return new CommandInfo(builder._CommandText, builder._CommandParameters.ToArray());
        }

        public static CommandInfo BuildUpdateCommand<T>(string commandTextTemplate, Expression<Func<T, bool>> predicate, params Expression<Func<T, Update>>[] update)
        {
            return BuildUpdateCommand(commandTextTemplate, update, predicate, null);
        }

        public static CommandInfo BuildUpdateCommand<T>(string commandTextTemplate, Expression<Func<T, Update>>[] update, LambdaExpression predicate)
        {
            return BuildUpdateCommand(commandTextTemplate, update, predicate, null);
        }

        public static CommandInfo BuildUpdateCommand<T>(string commandTextTemplate, Expression<Func<T, Update>>[] update, LambdaExpression predicate, TableMappings tableMappings)
        {
            var builder = new UpdateCommandBuilder(commandTextTemplate, tableMappings, predicate, update);
            builder.Build();
            return new CommandInfo(builder._CommandText, builder._CommandParameters.ToArray());
        }

        public static CommandInfo BuildUpdateCommand<T>(string tableName, T record, params Expression<Func<T, KeySelector>>[] keySelectors)
        {
            return BuildUpdateCommand(tableName, record, keySelectors, new Expression<Func<T, KeySelector>>[0]);
        }

        public static CommandInfo BuildUpdateCommand<T>(string tableName, T record, Expression<Func<T, KeySelector>>[] keySelectors, params Expression<Func<T, KeySelector>>[] exclude)
        {
            var builder = new UpdateAllColumnsCommandBuilder<T>(tableName, record, keySelectors, exclude);
            builder.Build();
            return new CommandInfo(builder._CommandText, builder._CommandParameters.ToArray());
        }


        public static CommandInfo BuildDeleteCommand(string commandTextTemplate, LambdaExpression predicate)
        {
            return BuildDeleteCommand(commandTextTemplate, predicate, null);
        }

        public static CommandInfo BuildDeleteCommand(string commandTextTemplate, LambdaExpression predicate, TableMappings tableMappings)
        {
            var builder = new DeleteCommandBuilder(commandTextTemplate, tableMappings, predicate);
            builder.Build();
            return new CommandInfo(builder._CommandText, builder._CommandParameters.ToArray());
        }

        public static CommandInfo BuildInsertCommand<T>(string tableName, T record, bool selectIdentity)
        {
            return BuildInsertCommand<T>(tableName, record, selectIdentity, new Expression<Func<T, KeySelector>>[0]);
        }

        public static CommandInfo BuildInsertCommand<T>(string tableName, T record, bool selectIdentity, params Expression<Func<T, KeySelector>>[] exclude)
        {
            var builder = new InsertCommandBuilder<T>(tableName, record, selectIdentity, exclude);
            builder.Build();
            return new CommandInfo(builder._CommandText, builder._CommandParameters.ToArray());
        }


        public const string DefaultParameterNamePrefix = "__p_";
        public const string PlaceHolderDelimiter = "@@@";
        public const string PredicatePlaceHolder = "WHERE";
        public const string OrderByPlaceHolder = "ORDERBY";
        public const string UpdatePlaceHolder = "UPDATE";

        TableMappings _TableMappings;
        int _ParameterIndex;
        string _ParameterNamePrefix;
        string _CommandTextTemplate;


        List<System.Data.IDataParameter> _CommandParameters = new List<System.Data.IDataParameter>();
        string _CommandText = null;


        protected abstract void ProcessToken(StringBuilder builder, string token);


        protected CommandBuilder(string commandTextTemplate, TableMappings tableMappings)
        {
            _ParameterIndex = 0;
            _ParameterNamePrefix = DefaultParameterNamePrefix;

            this._TableMappings = tableMappings == null ? new TableMappings() : tableMappings;

            _CommandTextTemplate = commandTextTemplate;
        }

        private void Build()
        {
            if (_CommandTextTemplate == null)
                return;

            StringBuilder builder = new StringBuilder();

            int startPosition = 0;

            while (startPosition >= 0)
            {
                string placeHolder = GetNextPlaceHolder(builder, ref startPosition);
                ProcessToken(builder, placeHolder);
            }
            _CommandText = builder.ToString();
        }


        protected void TranslateOrderBy(StringBuilder builder, LambdaExpression[] orderby)
        {
            if (orderby == null || orderby.Length == 0)
            {
                builder.Append("1");
                return;
            }
            //, Dictionary<string, string> parameterMappings
            var expression = orderby[0];
            BuildOrderBy(builder, expression);
            for (int i = 1; i < orderby.Length; i++)
            {
                builder.Append(",");
                BuildOrderBy(builder, orderby[i]);
            }
        }

        protected void TranslatePredicate(StringBuilder builder, IEnumerable<LambdaExpression> predicate)
        {
            if (predicate == null || predicate.Count() == 0)
            {
                builder.Append("(1=1)");
                return;
            }
            int i = 0;
            foreach (var pre in predicate)
            { 
                Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings2(pre.Parameters);
                if (i != 0)
                {
                    builder.Append(" and ");
                }
                var body = pre.Body;
                TranslateExpression(builder, body, parameterMappings);
                i++;
            }
            //Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings2(predicate.Parameters);

            //var body = predicate.Body;

            ////Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings(predicate.Parameters);

            //TranslateExpression(builder, body, parameterMappings);
        }

        protected void TranslatePredicate(StringBuilder builder, LambdaExpression predicate)
        {
            if (predicate == null)
            {
                builder.Append("(1=1)");
                return;
            }

            Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings2(predicate.Parameters);

            var body = predicate.Body;

            //Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings(predicate.Parameters);

            TranslateExpression(builder, body, parameterMappings);
        }

        protected void TranslateUpdate(StringBuilder builder, LambdaExpression predicate, LambdaExpression[] _Columns)
        {
            //Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings(predicate.Parameters);

            var expression = _Columns[0];
            BuildUpdate(builder, expression);
            for (int i = 1; i < _Columns.Length; i++)
            {
                builder.Append(",");
                BuildUpdate(builder, _Columns[i]);
            }
        }

        private void BuildUpdate(StringBuilder builder, LambdaExpression lambdaExpression)
        {
            var body = lambdaExpression.Body;
            var updateCall = (MethodCallExpression)body;

            if (updateCall.Method.DeclaringType != typeof(UpdateExtensions))
            {
                throw new InvalidCastException();
            }

            var unary = (UnaryExpression)updateCall.Arguments[0];
            var member = (MemberExpression)unary.Operand;
            builder.Append("[").Append(member.Member.Name).Append("]=");
            TranslateExpression(builder, updateCall.Arguments[1], new Dictionary<string, string>());
            //BuildConstant(builder, updateCall.Arguments[1]);
        }

        private void BuildOrderBy(StringBuilder builder, LambdaExpression expression)
        {
            Dictionary<string, string> parameterMappings = GetExpressionParameterTableNameMappings2(expression.Parameters);
            var body = (MethodCallExpression)expression.Body;

            if (body.Method.DeclaringType != typeof(OrderByExtensions))
                throw new InvalidCastException();

            if (body.Arguments.Count != 1)
                throw new InvalidCastException();

            var arg0 = body.Arguments[0];
            if (arg0.NodeType == ExpressionType.Convert)
            {
                arg0 = ((System.Linq.Expressions.UnaryExpression)arg0).Operand;
            }

            var orderby = (MemberExpression)arg0;

            var tableExpression = (ParameterExpression)orderby.Expression;
            string column = orderby.Member.Name;

            var tableName = parameterMappings[tableExpression.Name];

            if (tableName != null)
            {
                builder.Append(QuoteName(tableName));
                builder.Append(".");
            }

            builder.Append("[").Append(column).Append("]");

            string methodName = body.Method.Name;

            if (methodName == "Ascending")
            {
                builder.Append(" ASC");
            }
            else if (methodName == "Descending")
            {
                builder.Append(" DESC");
            }
        }

        private string GetNextPlaceHolder(StringBuilder builder, ref int startIndex)
        {
            int index = _CommandTextTemplate.IndexOf(PlaceHolderDelimiter, startIndex);
            if (index < 0)
            {
                builder.Append(_CommandTextTemplate.Substring(startIndex));
                startIndex = -1;
                return null;
            }

            builder.Append(_CommandTextTemplate.Substring(startIndex, index - startIndex));
            startIndex = index;

            index += PlaceHolderDelimiter.Length;

            int index2 = _CommandTextTemplate.IndexOf(PlaceHolderDelimiter, index);
            if (index2 < 0)
            {
                builder.Append(_CommandTextTemplate.Substring(startIndex));
                startIndex = -1;
                return null;
            }

            string placeHolder = _CommandTextTemplate.Substring(index, index2 - index);

            startIndex = index2 + PlaceHolderDelimiter.Length;
            if (startIndex == _CommandTextTemplate.Length)
                startIndex = -1;

            return placeHolder;
        }

        protected Dictionary<string, string> GetExpressionParameterTableNameMappings2(IEnumerable<ParameterExpression> parameters)
        {
            Dictionary<string, string> parameterMappings = new Dictionary<string, string>();

            if (parameters == null)
                return parameterMappings;

            foreach (var p in parameters)
            {
                var tableName = _TableMappings.GetTableName(p.Type);
                parameterMappings.Add(p.Name, tableName);
            }
            return parameterMappings;
        }

        private void TranslateExpression(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            var nodeType = expression.NodeType;

            switch (nodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    BuildBinary(builder, expression, parameterMappings, "AND");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    BuildBinary(builder, expression, parameterMappings, "OR");
                    break;
                case ExpressionType.Equal:
                    BuildBinary(builder, expression, parameterMappings, "=");
                    break;
                case ExpressionType.LessThan:
                    BuildBinary(builder, expression, parameterMappings, "<");
                    break;
                case ExpressionType.LessThanOrEqual:
                    BuildBinary(builder, expression, parameterMappings, "<=");
                    break;
                case ExpressionType.GreaterThan:
                    BuildBinary(builder, expression, parameterMappings, ">");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    BuildBinary(builder, expression, parameterMappings, ">=");
                    break;
                case ExpressionType.NotEqual:
                    BuildBinary(builder, expression, parameterMappings, "<>");
                    break;

                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    BuildBinary(builder, expression, parameterMappings, "+");
                    break;
                case ExpressionType.Divide:
                    BuildBinary(builder, expression, parameterMappings, "/");
                    break;
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    BuildBinary(builder, expression, parameterMappings, "*");
                    break;
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    BuildBinary(builder, expression, parameterMappings, "-");
                    break;

                case ExpressionType.Not:
                    BuildNot(builder, expression, parameterMappings);
                    break;
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    BuildNegate(builder, expression, parameterMappings);
                    break;

                case ExpressionType.ExclusiveOr:
                    BuildBinary(builder, expression, parameterMappings, "~");
                    break;

                case ExpressionType.Coalesce:
                    BuildCoalesce(builder, expression, parameterMappings);
                    break;
                case ExpressionType.MemberAccess:
                    BuildMemberAccess(builder, expression, parameterMappings);
                    break;
                case ExpressionType.Constant:
                    BuildConstant(builder, expression);
                    break;
                case ExpressionType.Call:
                    BuildCall(builder, expression, parameterMappings);
                    break;
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    BuildConvert(builder, expression, parameterMappings);
                    break;
                default:
                    BuildConstant(builder, GetExpressionValue(expression));
                    break;
            }
        }

        private void BuildConvert(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            var unary = (UnaryExpression)expression;
            var operand = unary.Operand;
            if (operand is MemberExpression)
            {
                var member = (MemberExpression)operand;
                if (member.Expression is ParameterExpression)
                {
                    BuildMemberAccess(builder, member, parameterMappings);
                    return;
                }
            }

            BuildConstant(builder, GetExpressionValue(unary));
        }

        private void BuildNegate(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            builder.Append("(-");
            TranslateExpression(builder, expression, parameterMappings);
            builder.Append(")");
        }

        private void BuildNot(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            builder.Append("(NOT ");
            TranslateExpression(builder, expression, parameterMappings);
            builder.Append(")");
        }

        private void BuildCall(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            var callExpression = (MethodCallExpression)expression;
            var method = callExpression.Method;
            if (method.DeclaringType == typeof(string) && (method.Name == "Contains" || method.Name == "StartWith" || method.Name == "EndWith"))
            {
                BuildStringLike(builder, callExpression.Object, callExpression.Arguments[0], method.Name, parameterMappings);
            }
            else if (method.Name == "Contains" && method.DeclaringType == typeof(System.Linq.Enumerable))
            {
                BuildIn(builder, callExpression.Arguments[0], callExpression.Arguments[1], parameterMappings);
            }
            else
            {
                BuildConstant(builder, GetExpressionValue(expression));
            }
        }

        private void BuildIn(StringBuilder builder, Expression targetObject, Expression parameter, Dictionary<string, string> parameterMappings)
        {
            var enumerable = GetExpressionValue(targetObject) as System.Collections.IEnumerable;

            List<object> elements = new List<object>();

            if (enumerable != null)
            {
                foreach (var i in enumerable)
                {
                    elements.Add(i);
                }
            }

            if (elements.Count == 0)
            {
                builder.Append("(1<>1)");
                return;
            }

            builder.Append("(");

            TranslateExpression(builder, parameter, parameterMappings);
            builder.Append(" IN (");
            BuildConstant(builder, elements[0]);

            for (int i = 1; i < elements.Count; i++)
            {
                builder.Append(",");
                BuildConstant(builder, elements[i]);
            }

            builder.Append("))");
        }

        private void BuildStringLike(StringBuilder builder, Expression targetObject, Expression parameter, string methedName, Dictionary<string, string> parameterMappings)
        {
            builder.Append("(");
            TranslateExpression(builder, targetObject, parameterMappings);
            builder.Append(" LIKE (");

            if (methedName == "StartWith" || methedName == "Contains")
                builder.Append("'%' + ");

            TranslateExpression(builder, parameter, parameterMappings);

            if (methedName == "EndWith" || methedName == "Contains")
                builder.Append(" + '%'");

            builder.Append("))");
        }

        private void BuildMemberAccess(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            var memberExpression = (MemberExpression)expression;
            var targetExpression = memberExpression.Expression;
            if (targetExpression is ParameterExpression)
            {
                var parameter = (ParameterExpression)targetExpression;
                var tableName = parameterMappings[parameter.Name];
                if (tableName != null)
                {
                    builder.Append(QuoteName(tableName));
                    builder.Append(".");
                }
                builder.Append("[").Append(memberExpression.Member.Name).Append("]");
            }
            else
            {
                BuildConstant(builder, GetExpressionValue(expression));
            }
        }

        private void BuildCoalesce(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings)
        {
            var binaryExpression = (BinaryExpression)expression;
            builder.Append("ISNULL(");
            TranslateExpression(builder, binaryExpression.Left, parameterMappings);
            builder.Append(",");
            TranslateExpression(builder, binaryExpression.Right, parameterMappings);
            builder.Append(")");
        }

        private object GetExpressionValue(Expression expression)
        {
            var lambada = System.Linq.Expressions.Expression.Lambda(expression);
            var obj = lambada.Compile().DynamicInvoke();
            return obj;
        }

        private void BuildBinary(StringBuilder builder, Expression expression, Dictionary<string, string> parameterMappings, string op)
        {
            var binaryExpression = (BinaryExpression)expression;
            builder.Append("(");

            if (op == "=" || op == "<>")
            {

                int length00 = builder.Length;

                TranslateExpression(builder, binaryExpression.Left, parameterMappings);

                int length01 = builder.Length;

                if (IsNull(builder, length00, length01))
                {
                    builder.Length = length00;

                    TranslateExpression(builder, binaryExpression.Right, parameterMappings);

                    length01 = builder.Length;

                    if (IsNull(builder, length00, length01))
                    {
                        builder.Length = length00;
                        builder.Append(op == "=" ? "1=1" : "1<>1");
                    }
                    else
                    {
                        builder.Append(op == "=" ? " IS NULL" : " IS NOT NULL");
                    }
                }
                else
                {

                    builder.Append(" ").Append(op).Append(" ");

                    int length02 = builder.Length;

                    TranslateExpression(builder, binaryExpression.Right, parameterMappings);

                    int length03 = builder.Length;

                    if (IsNull(builder, length02, length03))
                    {
                        builder.Length = length01;
                        builder.Append(op == "=" ? " IS NULL" : " IS NOT NULL");
                    }
                }
            }
            else if (op == "AND" || op == "OR")
            {
                TranslateExpression(builder, binaryExpression.Right, parameterMappings);
                builder.Append(" ").Append(op).Append(" ");
                TranslateExpression(builder, binaryExpression.Left, parameterMappings);
            }
            else
            {
                TranslateExpression(builder, binaryExpression.Left, parameterMappings);
                builder.Append(" ").Append(op).Append(" ");
                TranslateExpression(builder, binaryExpression.Right, parameterMappings);
            }
            builder.Append(")");
        }

        private static bool IsNull(StringBuilder builder, int length1, int length2)
        {
            return (length2 - length1) == 4 && builder[length1] == 'N' && builder[length1 + 1] == 'U' && builder[length1 + 2] == 'L' && builder[length1 + 3] == 'L';
        }

        private void BuildConstant(StringBuilder builder, Expression expression)
        {
            var constantExpression = (ConstantExpression)expression;
            var value = constantExpression.Value;
            BuildConstant(builder, value);
        }

        protected void BuildConstant(StringBuilder builder, object value)
        {
            if (value == null)
            {
                builder.Append("NULL");
                return;
            }

            if (value is Enum)
            {
                value = Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()));
            }

            SqlParameter p = new SqlParameter(GetNextParameterName(), value);
            _CommandParameters.Add(p);
            builder.Append(p.ParameterName);
        }

        private string GetNextParameterName()
        {
            _ParameterIndex++;
            return "@" + _ParameterNamePrefix + _ParameterIndex.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        protected static string QuoteName(string tableName)
        {
            if (!tableName.StartsWith("["))
                tableName = "[" + tableName;
            if (!tableName.EndsWith("]"))
                tableName += "]";

            return tableName;
        }
    }

    public class TableMappings
    {
        class Entry
        {
            public Entry(System.Type type, string tableName)
            {
                Type = type;
                TableName = tableName;
            }
            public System.Type Type { get; set; }
            public string TableName { get; set; }
        }

        private List<Entry> Entries = new List<Entry>();

        public void Add<T>(string tableName)
        {
            var en = new Entry(typeof(T), tableName);
            Entries.Add(en);
        }

        internal string GetTableName(System.Type type)
        {
            var tm = Entries.Where(x => x.Type.IsAssignableFrom(type)).FirstOrDefault();
            return tm == null ? null : tm.TableName;
        }
    }

    // 永远不要手动创建该类的实例。
    // 通过引入 BookManager.Data 命名空间， 并在 Lambada 表达式中对字段使
    // 用 System.Object.Ascending 或 System.Object.Descending 两
    // 个扩展方法生成 Order By 表达式。
    public class OrderBy
    {
        // 永远不要手动创建该类的实例。
        // 通过引入 BookManager.Data 命名空间， 并在 Lambada 表达式中对字段使
        // 用 System.Object.Ascending 或 System.Object.Descending 两
        // 个扩展方法生成 Order By 表达式。
        internal OrderBy()
        {
            throw new System.InvalidOperationException();
        }
    }


    // 永远不要手动创建该类的实例。
    // 通过引入 BookManager.Data 命名空间， 并在 Lambada 表达式中对字段使
    // 用 System.Object.Update 扩展方法生成 Update 表达式。
    public class Update
    {
        // 永远不要手动创建该类的实例。
        // 通过引入 BookManager.Data 命名空间， 并在 Lambada 表达式中对字段使
        // 用 System.Object.Update 扩展方法生成 Update 表达式。
        internal Update()
        {
            throw new System.InvalidOperationException();
        }
    }


    // 永远不要手动创建该类的实例。
    // 通过引入 BookManager.Data 命名空间， 并在 Lambada 表达式中对字段使
    // 用 System.Object.KeySelector 扩展方法生成 KeySelector 表达式。
    public class KeySelector
    {
        // 永远不要手动创建该类的实例。
        // 通过引入 BookManager.Data 命名空间， 并在 Lambada 表达式中对字段使
        // 用 System.Object.KeySelector 扩展方法生成 KeySelector 表达式。
        internal KeySelector()
        {
            throw new System.InvalidOperationException();
        }
    }

    public static class UpdateExtensions
    {
        // 只能在 Lambda 表达式中使用， 不可实际执行。
        public static Update Update<T>(this object target, T value)
        {
            throw new System.InvalidOperationException();
        }

        public static KeySelector KeySelector<T>(this T target)
        {
            throw new System.InvalidOperationException();
        }
    }

    public static class OrderByExtensions
    {
        // 只能在 Lambda 表达式中使用， 不可实际执行。
        public static OrderBy Ascending<T>(this T target)
        {
            throw new System.InvalidOperationException();
        }

        // 只能在 Lambda 表达式中使用， 不可实际执行。
        public static OrderBy Descending<T>(this T target)
        {
            throw new System.InvalidOperationException();
        }
    }
}
