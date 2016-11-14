/* Qianzhan Com. © Copyight Reserved
 * Author: Sha Jianjian
 * Date: 2016-05-27  
 * Description: Interface which gives out common operations of database
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using QZ.Instrument.Model;
using QZ.Foundation.Monad;

namespace QZ.Instrument.Interface
{
    /// <summary>
    /// Interface of accessing database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDatabaseAccess<T>
    {
        int Insert(T t);
        int Update(T t);
        Maybe<T> Select(Expression<Func<T, object>> expr, object val);
        Maybe<T> Select(Expression<Func<T, object>> expr1, Expression<Func<T, object>> expr2, object val1, object val2);
        int Delete(Expression<Func<T, object>> expr, object val);
        int Delete(Expression<Func<T, object>> expr1, Expression<Func<T, object>> expr2, object val1, object val2);
        IList<T> Page_Select(DatabaseSearchModel<T> search);
    }
}
