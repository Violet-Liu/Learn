/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Foundation.Utility;

namespace QZ.Instrument.Model
{
    public class DatabaseSearchModel
    {
        private StringBuilder _sb = new StringBuilder();
        /// <summary>
        /// columns clause
        /// </summary>
        public string Column { get; set; } = "*";
        /// <summary>
        /// Order clause
        /// </summary>
        public string Order { get; set; }
        public string Table { get; set; }


        /// <summary>
        /// Where clause
        /// </summary>
        public string Where
        {
            get { return _sb.ToString(); }
            set { _sb.Append(value); }
        }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DatabaseSearchModel SetWhere(string where)
        {
            if (_sb.Length > 0)
                _sb.Append(" and ").Append(where);
            else
                _sb.Append(" where ").Append(where);
            return this;
        }

        public DatabaseSearchModel SetOrWhere(string where)
        {
            if (_sb.Length > 0)
                _sb.Append(" or ").Append(where);
            else
                _sb.Append(" where ").Append(where);
            return this;
        }
        public DatabaseSearchModel Where_Clear()
        {
            _sb.Clear();
            return this;
        }
        public DatabaseSearchModel SetTable(string table)
        {
            Table = table;
            return this;
        }
        public DatabaseSearchModel SetOrder(string order)
        {
            Order = order;
            return this;
        }
        public DatabaseSearchModel SetColumn(string column)
        {
            Column = column;
            return this;
        }

        public DatabaseSearchModel SetPageIndex(int index)
        {
            PageIndex = Calibrater.Pg_Index_Check(index);
            return this;
        }
        public DatabaseSearchModel SetPageSize(int pagesize)
        {
            PageSize = Calibrater.Pg_Size_Check(pagesize);
            return this;
        }
    }
}
