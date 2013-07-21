using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    public abstract class Filter<T> : IFilter<T>
    {
        /// <summary>
        /// Удовлетворяет ли объект спецификации
        /// </summary>
        /// <param name="item">Проверяемый объект</param>
        public bool IsSatisfiedBy(T item)
        {
            return Predicate.Compile()(item);
        }

        /// <summary>
        /// Предикат для проверки спецификации
        /// </summary>
        public abstract Expression<Func<T, bool>> Predicate { get; }


        public static Filter<T> operator !(Filter<T> filter)
        {
            return new NotFilter<T>(filter);
        }

        public static Filter<T> operator |(Filter<T> left, Filter<T> right)
        {
            return new OrFilter<T>(left, right);
        }

        public static Filter<T> operator &(Filter<T> left, Filter<T> right)
        {
            return new AndFilter<T>(left, right);
        }


        public static implicit operator Predicate<T>(Filter<T> filter)
        {
            return filter.IsSatisfiedBy;
        }

        public static implicit operator Func<T, bool>(Filter<T> filter)
        {
            return filter.IsSatisfiedBy;
        }

        public static implicit operator Expression<Func<T, bool>>(Filter<T> filter)
        {
            return filter.Predicate;
        }
    }
}
