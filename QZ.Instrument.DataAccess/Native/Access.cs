using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

using QZ.Foundation.Monad;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;

namespace QZ.Instrument.DataAccess.Native
{
    public abstract class Access
    {
        public Access(string conn_key)
        {
            _conn_key = conn_key;
        }

        private string _conn_key;
        public SqlConnection Connection { get { return new SqlConnection(_conn_key); } }


        public DataSet Query2DataSet(string sql)
        {
            using (var con = new SqlConnection(_conn_key))
            {
                using (var sda = new SqlDataAdapter(sql, con))
                {
                    var ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
            }
        }

        public SqlDataReader Query2DataReader(string sql)
        {
            var con = new SqlConnection(_conn_key);
            con.Open();
            var cmd = new SqlCommand(sql, con);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public Maybe<T> Select<T>(string sql) where T : class =>
            sql.ToMaybe()
               .Select<DataSet>(s => Query2DataSet(s))
               .Where(ds => ds.Tables[0].Rows.Count > 0)
               .Select<T>(ds => DynConstructor.Generate<T>(ds.Tables[0].Rows[0]));

        public Maybe<List<T>> SelectMany<T>(string sql) where T : class =>
            sql.ToMaybe()
               .Select<DataSet>(s => Query2DataSet(s))
               .Where(ds => ds.Tables[0].Rows.Count > 0)
               .Select<List<T>>(ds => DynConstructor.List_Generate<T>(ds.Tables[0].Rows));
    }
}
