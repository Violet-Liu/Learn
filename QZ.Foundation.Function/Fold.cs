using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Function
{
    public static class Fold
    {
        public static V foldl<T, V>(this List<T> list, Func<V, T, V> func, V v)
        {
            var iter = list.GetEnumerator();
            if (iter.MoveNext())
            {
                var a = iter.Current;
                var b = func(v, a);
                while(iter.MoveNext())
                {
                    b = func(b, iter.Current);
                }
                return b;
            }
            else
                return default(V);
        }
    }
}
