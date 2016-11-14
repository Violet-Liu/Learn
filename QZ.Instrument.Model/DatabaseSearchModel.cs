using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QZ.Instrument.Utility;
using QZ.Foundation.Monad;

namespace QZ.Instrument.Model
{
    public class DatabaseSearchModel<T>
    {
        private StringBuilder _sb = new StringBuilder();
        /// <summary>
        /// denote this model's status
        /// </summary>
        public bool Status { get; private set; } = true;
        /// <summary>
        /// columns clause
        /// </summary>
        public string Column { get; set; } = "*";
        /// <summary>
        /// Field on which order are orgnized
        /// </summary>
        public string OrderField { get; set; }
        /// <summary>
        /// Order direction
        /// </summary>
        public bool OrderAscend { get; set; }
        /// <summary>
        /// Order clause
        /// </summary>
        public string Order
        {
            get { return $" {OrderField} {(OrderAscend ? "asc" : "desc")}"; }
        }
        /// <summary>
        /// Where clause
        /// </summary>
        public string Where { get { return _sb.ToString(); } }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// Get the count of total rows which meet the select filter
        /// </summary>
        public Func<int> GetTotalRows { get; set; }
        public ExpressionVisitorSql ExpressionVisitor { get { return new ExpressionVisitorSql(); } }


        public DatabaseSearchModel<T> SetColumn(Expression<Func<T, object>> columnSelector)
        {
            Column = ExpressionVisitor.ResolveAsString(columnSelector);
            //if (colMb.HasValue)
            //    Column = colMb.Value;
            return this;
        }

        public DatabaseSearchModel<T> SetOrderField(Expression<Func<T, object>> orderFieldSelector)
        {
            var field = ExpressionVisitor.ResolveAsString(orderFieldSelector);
            OrderField = field;
            return this;
        }

        public DatabaseSearchModel<T> Ascend(bool flag)
        {
            OrderAscend = flag;
            return this;
        }
        public DatabaseSearchModel<T> SetPageIndex(int index)
        {
            PageIndex = index;
            return this;
        }
        public DatabaseSearchModel<T> SetPageSize(int pagesize)
        {
            PageSize = pagesize;
            return this;
        }

        [Obsolete("Where's equal expression is substituded by SetWhereExpr, in which you can pass in a paprmenter expression such as p.FieldName == \"name\"")]
        public DatabaseSearchModel<T> SetWhereEq(Expression<Func<T, object>> fieldSelect, object val)
        {
            if(_sb.Length > 0)
                _sb.Append(" and ").Append(ExpressionVisitor.ResolveAsString(fieldSelect)).Append("=").Append(val);
            else
                _sb.Append(" where ").Append(ExpressionVisitor.ResolveAsString(fieldSelect)).Append("=").Append(val);
            return this;
        }

        public DatabaseSearchModel<T> SetWhereExpr(Expression<Func<T, object>> expression)
        {
            if (_sb.Length > 0)
                _sb.Append(" and ").Append(ExpressionVisitor.ResolveAsString(expression));
            else
                _sb.Append(" where ").Append(ExpressionVisitor.ResolveAsString(expression));

            return this;
        }

        public DatabaseSearchModel<T> SetWhereClause(string where)
        {
            if (_sb.Length > 0)
                _sb.Append(" and ").Append(where);
            else
                _sb.Append(" where ").Append(where);
            return this;
        }
    }
}
