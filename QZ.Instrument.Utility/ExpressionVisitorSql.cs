using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using QZ.Foundation.Monad;

namespace QZ.Instrument.Utility
{
    /// <summary>
    /// Visit an expression.
    /// </summary>
    public class ExpressionVisitorSql : ExpressionVisitor
    {
        private Stack<string> _stack = new Stack<string>();
        public MemberInfo Member { get; private set; }

        public MemberInfo ResolveAsMemberInfo(Expression expression)
        {
            if (Member == null)
                Visit(expression);
            return Member;
        }
        /// <summary>
        /// Resolve a expression, from whose body, get the member name
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string ResolveAsString(Expression expression)
        {
            if(Member == null)
                Visit(expression);
            return Member.Name;
            //return _stack.Aggregate(new StringBuilder(),
            //    (sb, name) => (sb.Length > 0 ? sb.Append(".") : sb).Append(name)).ToString();
        }
        public PropertyInfo ResolveAsPropertyInfo(Expression expression)
        {
            if(Member == null)
                Visit(expression);
            return Member as PropertyInfo;
        }
        private Maybe<string> Resolve(Maybe<MemberInfo> info) => info.HasValue ? info.Value.Name : null;
        

        protected override Expression VisitMember(MemberExpression expression)
        {
            Member = expression.Member;
            //var name = this.Resolve(Member);
            //if(name.HasValue)
            //    _stack.Push(name.Value);
            return base.VisitMember(expression);
        }
        protected override Expression VisitConstant(ConstantExpression expression)
        {
            _stack.Push(expression.Value.ToString());
            return base.VisitConstant(expression);
        }
        protected override Expression VisitBinary(BinaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Equal)
            {
                Visit(expression.Right);
                _stack.Push("=");
                Visit(expression.Left);
            }
            else if(expression.NodeType == ExpressionType.NotEqual)
            {
                Visit(expression.Right);
                _stack.Push("!=");
                Visit(expression.Left);
            }
            else if(expression.NodeType == ExpressionType.LessThan)
            {
                Visit(expression.Right);
                _stack.Push("<");
                Visit(expression.Left);
            }
            else if(expression.NodeType == ExpressionType.LessThanOrEqual)
            {
                Visit(expression.Right);
                _stack.Push("<=");
                Visit(expression.Left);
            }
            else if(expression.NodeType == ExpressionType.GreaterThan)
            {
                Visit(expression.Right);
                _stack.Push(">");
                Visit(expression.Left);
            }
            else if(expression.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                Visit(expression.Right);
                _stack.Push(">");
                Visit(expression.Left);
            }
            return base.VisitBinary(expression);
        }
        protected override Expression VisitParameter(ParameterExpression expression)
        {
            return base.VisitParameter(expression);
        }
        [Obsolete("This function is a simple expression-visiting process.")]
        public static string GetPropertyName<T>(Expression<Func<T, object>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }
    }
}
