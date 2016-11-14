/*
 * Author: Sha Jianjian
 * Date: 2016-05-28
 * Description: Base class for accessing database. All database entities are load initially into a dicitionary. When exchanging data between entity class and data reader,
 * search through the dictionary for related data members will be a help.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using QZ.Instrument.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;
using QZ.Foundation.Monad;

namespace QZ.Instrument.DataAccess
{
    public class DataAccess<T> : AccessBase, IDatabaseAccess<T>, IDisposable where T : new()
    {
        private Database _db;
        public string TypeName { get { return typeof(T).Name; } }
        public ExpressionVisitorSql ExpressionVisitor { get { return new ExpressionVisitorSql(); } }

        private string _type_Name;
        #region Store procedure names of CRUD operations
        public string SpName_Delete { get { return $"{_type_Name}_Delete_By_"; } }
        public string SpName_Insert { get { return $"{_type_Name}_Insert"; } }
        public string SpName_Update { get { return $"{_type_Name}_Update"; } }
        public string SpName_Select { get { return $"{_type_Name}_Select_By_"; } }
        public string User_Select_By_Name { get { return "Proc_Users_Selectbyusername"; } }
        public string SpName_Page_Select { get { return $"{_type_Name}_Page_Select"; } }
        #endregion

        #region .ctor
        public DataAccess()
        { }
        public DataAccess(string connStr)
        {
            _type_Name = typeof(T).Name;
            _db = DatabaseFactory.CreateDatabase(connStr);
        }
        #endregion

        #region IDispose
        public void Dispose()
        { }
        #endregion

        #region CRUD operations
        public int Delete(Expression<Func<T, object>> expr, object val)
        {
            return _db.ToMaybe().Select<DbCommand, int>(
                    db => db.GetStoredProcCommand(SpSet.Dict[SpName_Delete + ExpressionVisitor.ResolveAsString(expr)]),
                    (db, cmd) =>
                    {
                        db.AddInParameter(cmd, "@" + ExpressionVisitor.ResolveAsString(expr),
                            Constants.Sql_Csharp_TypeMap[ExpressionVisitor.ResolveAsPropertyInfo(expr).PropertyType.Name], val);
                        return db.ExecuteNonQuery(cmd);
                    }
                   ).Value;
        }
        public int Delete(Expression<Func<T, object>> expr1, Expression<Func<T, object>> expr2, object val1, object val2)
        {
            throw new NotImplementedException();
        }
        public int Insert(T t)
        {
            return _db.ToMaybe().Select<DbCommand, int>(
                       db => db.GetStoredProcCommand(SpSet.Dict[SpName_Insert]),
                       (db, cmd) =>
                       {
                           if (!Map_Insert(t, db, cmd))
                               return -5;
                           else
                           {
                               db.ExecuteNonQuery(cmd);
                               return 0; //(int)cmd.Parameters["@" + t.Id].Value;
                           }
                       }
                   )
                   .Value;
        }

        public IList<T> Page_Select(DatabaseSearchModel<T> search)
        {
            return _db.ToMaybe().Select<DbCommand, List<T>>(
                    db => db.GetStoredProcCommand(SpSet.Dict[SpName_Page_Select]),
                    (db, cmd) =>
                    {
                        Map_Page_Select(search, db, cmd);
                        var list = new List<T>();
                        using (IDataReader dr = db.ExecuteReader(cmd))
                        {
                            while (dr.Read())
                            {
                                list.Add(Map_Entity(dr));
                            }
                            //dr.NextResult();
                            return list;
                        }
                    }
                  ).Value;
        }

        public Maybe<T> Select(Expression<Func<T, object>> expr, object val)
        {
            return _db.ToMaybe().Select<DbCommand, T>(
                        db => db.GetStoredProcCommand(User_Select_By_Name/*SpName_Select + ExpressionVisitor.ResolveAsString(expr)*/),     // get store procedure, sp name := base sp name + key field name
                        (db, cmd) =>                                                                                // set sp params and then execute sp
                        {
                            var paramName = "@" + ExpressionVisitor.ResolveAsMemberInfo(expr).Name;
                            var key = ExpressionVisitor.ResolveAsPropertyInfo(expr).PropertyType.Name;
                            var type = Constants.Sql_Csharp_TypeMap[key];

                            db.AddInParameter(cmd, paramName, type, val);
                            using (IDataReader dr = db.ExecuteReader(cmd))
                            {
                                return dr.Read() ? Map_Entity(dr) : default(T);
                            }
                        }
                   );    
        }
        public Maybe<T> Select(Expression<Func<T, object>> expr1, Expression<Func<T, object>> expr2, object val1, object val2)
        {
            throw new NotImplementedException();
        }
        public int Update(T t)
        {
            return _db.ToMaybe().Select<DbCommand, int>(
                       db => db.GetStoredProcCommand(SpSet.Dict[SpName_Update]),
                       (db, cmd) => Map_Update(t, db, cmd) ? -5 : db.ExecuteNonQuery(cmd)
                   )
                   .Value;
        }
        #endregion

        #region These methods' efficency is low, so each concrete subclass should overload this method
        #region Map entity fields into store procedure parameters
        /// <summary>
        /// Map datas for insert store procedure
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Map success, true, or else false returned.</returns>
        protected virtual bool Map_Insert(T t, Database db, DbCommand dbCmd)
        {
            if (DbEntity.Instance.Entities.ContainsKey(TypeName))
            {
                var properties = DbEntity.Instance.Entities[TypeName];
                db.AddOutParameter(dbCmd, "@" + DbEntity.Instance.Entities[nameof(T)].First().Name, DbType.Int32, 4);
                foreach(var p in properties.Skip(1))
                {
                    db.AddInParameter(dbCmd, "@" + p.Name, Constants.Sql_Csharp_TypeMap[p.PropertyType.Name], p.GetValue(t));
                }
                return true;
            }
            return false;
        }
        protected virtual bool Map_Update(T t, Database db, DbCommand dbCmd)
        {
            if(DbEntity.Instance.Entities.ContainsKey(TypeName))
            {
                var properties = DbEntity.Instance.Entities[TypeName];
                properties.ToList().ForEach(p => db.AddInParameter(dbCmd, "@" + p.Name, Constants.Sql_Csharp_TypeMap[p.PropertyType.Name], p.GetValue(t)));
                return true;
            }
            return false;
        }
        protected bool Map_Page_Select(DatabaseSearchModel<T> search, Database db, DbCommand dbCmd)
        {
            db.AddInParameter(dbCmd, "@column", Constants.Sql_Csharp_TypeMap["string"], search.Column);
            db.AddInParameter(dbCmd, "@where", Constants.Sql_Csharp_TypeMap["string"], search.Where);
            db.AddInParameter(dbCmd, "@order", Constants.Sql_Csharp_TypeMap["string"], search.Order);
            db.AddInParameter(dbCmd, "@page", Constants.Sql_Csharp_TypeMap["int"], search.PageIndex);
            db.AddInParameter(dbCmd, "@pageSize", Constants.Sql_Csharp_TypeMap["int"], search.PageSize);
            db.AddOutParameter(dbCmd, "@rowCount", Constants.Sql_Csharp_TypeMap["int"], 4);
            search.GetTotalRows = () => (int)dbCmd.Parameters["@rowCount"].Value;
            return true;
        }
        #endregion
        #region Map data from data reader into C# entity
        public T Map_Entity(IDataReader dr)
        {
            T t = new T();
            if(DbEntity.Instance.Entities.ContainsKey(TypeName))
            {
                var properties = DbEntity.Instance.Entities[TypeName];
                properties.ToList().ForEach(f => f.SetValue(t, dr[f.Name]));
                return t;
            }
            return t;
        }
        #endregion

        #endregion
    }
}
