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
    public class Model<T> : ModelBase<Model<T>, IModel, T>, IModel where T : class
    {
    }
    public interface IModel : IModelBase
    {

    }
}
