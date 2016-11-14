using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Collection
{
    class Util
    {
        public static void Int_Assert(Predicate<int> predicate, params int[] arr)
        {
            foreach(var a in arr)
            {
                if (!predicate(a))
                    throw new Exception("params can not pass the test condition");
            }
        }
    }
}
