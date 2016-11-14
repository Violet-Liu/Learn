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
    public abstract class ObjectBase<T, UInterface> : IObject where T : ObjectBase<T, UInterface>, UInterface where UInterface : class
    {
        protected UInterface Self => (T)this;
        protected T Assign(Action<UInterface> assigner) => Fluent.Assign_1((T)this, assigner);
    }

    public interface IObject
    {

    }
}
