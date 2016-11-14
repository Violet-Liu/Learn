/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 *
 *
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Monad
{
    public class Either<L, R>
    {
        public L Left { get; private set; }
        public bool HasLeft { get; set; }
        public R Right { get; private set; }
        public bool HasRight { get; set; }

        public Either<L, R> SetLeft(L l)
        {
            this.Left = l;
            this.HasLeft = true;
            return this;
        }
        public Either<L, R> SetRight(R r)
        {
            this.Right = r;
            this.HasRight = true;
            return this;
        }
        public static Either<L, R> FromLeft(L l)
        {
            return new Either<L, R>().SetLeft(l);
        }
        public static Either<L, R> FromRight(R r)
        {
            return new Either<L, R>().SetRight(r);
        }
        public Either<L, NR> Select<NR>(Func<R, Either<L, NR>> f)
        {
            return this.HasLeft ? new Either<L, NR>().SetLeft(this.Left) : f(this.Right);
        }
        public Either<L, V> Select<U, V>(Func<R, Either<L, U>> f, Func<R, U, V> s)
        {
            return Select(r => f(r).Select(u => new Either<L, V>().SetRight(s(r, u))));
        }
    }
}
