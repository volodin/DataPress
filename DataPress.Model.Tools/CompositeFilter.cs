using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.Model.Tools
{
    public abstract class CompositeFilter<T> : Filter<T>
    {
         /// <summary>
        /// Список спецификаций
        /// </summary>
        protected readonly List<Filter<T>> _Filters;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="specifications">Список спецификаций</param>
        protected CompositeFilter(params Filter<T>[] filters)
        {
            _Filters = new List<Filter<T>>(filters);
        }


        /// <summary>
        /// Добавить спецификацию
        /// </summary>
        /// <param name="specification">Спецификация</param>
        public void Add(Filter<T> filter)
        {
            _Filters.Add(filter);
        }


        /// <summary>
        /// Удалить спецификацию
        /// </summary>
        /// <param name="specification">Спецификация</param>
        public void Remove(Filter<T> filter)
        {
            _Filters.Remove(filter);
        }
    }
}
