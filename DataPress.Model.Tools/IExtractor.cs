using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    public interface IExtractor<T>
    {
        //Expression<Func<IQueryable<T>, IQueryable<T>>> Predicate { get; }

        IQueryable<T> Extract(IQueryable<T> source);
    }
}
