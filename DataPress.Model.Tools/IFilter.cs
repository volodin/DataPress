using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Удовлетворяет ли объект спецификации
        /// </summary>
        /// <param name="item">Проверяемый объект</param>
        bool IsSatisfiedBy(T item);
    }
}
