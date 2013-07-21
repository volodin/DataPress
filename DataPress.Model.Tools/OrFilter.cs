using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    public class OrFilter<T> : CompositeFilter<T>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public OrFilter()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="specifications">Список спецификаций</param>
        public OrFilter(params Filter<T>[] filters)
            : base(filters)
        {
        }


        /// <summary>
        /// Предикат для проверки спецификации
        /// </summary>
        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                Expression<Func<T, bool>> result = _Filters.First();
                return _Filters.Skip(1).Aggregate(result, (current, filter) => current.Or((Expression<Func<T, bool>>)filter));
            }
        }
    }
}
