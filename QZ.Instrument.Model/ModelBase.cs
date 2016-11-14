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

namespace QZ.Instrument.Model
{
    public class ModelBase<T, U, V> : Object<T, U>, IModelBase
        where T : ModelBase<T, U, V>, U
        where U : class, IModelBase
        where V : class
    {
    }

    public interface IModelBase : IObject
    {

    }
}
