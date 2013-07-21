using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    public class PageSelector<T> : IExtractor<T> 
    {
        Expression<Func<IQueryable<T>, IQueryable<T>>> _Predicate;

        public PageSelector(int limit, int offset = 0)
        {
            if (offset == 0)
                _Predicate = x => x.Take(limit);
            else
                _Predicate = x => x.Skip(offset).Take(limit);
        }

        #region IExtractor<T> Members

        public Expression<Func<IQueryable<T>, IQueryable<T>>> Predicate
        {
            get { return _Predicate; }
        }

        #endregion // IExtractor<T> Members
    }
}
