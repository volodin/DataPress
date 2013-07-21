using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    /// <summary>
    /// Инверсия спецификации
    /// </summary>
    /// <typeparam name="T">Тип объекта, для которого применяется спецификация</typeparam>
    class NotFilter<T> : Filter<T>
    {
        private readonly Filter<T> _filter;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <exception cref="ArgumentNullException"/>
        public NotFilter(Filter<T> filter)
        {
            if (filter == null)
                throw new ArgumentNullException();

            _filter = filter;
        }


        /// <summary>
        /// Спецификация
        /// </summary>
        public Filter<T> Filter
        {
            get { return _filter; }
        }

        /// <summary>
        /// Предикат для проверки спецификации
        /// </summary>
        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                return ((Expression<Func<T, bool>>)_filter).Not();
            }
        }
    }
}
