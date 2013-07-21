using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.Model.Tools
{
    public interface ISelector<Tin, Tout>
    {
        Expression<Func<Tin, Tout>> Predicate { get; }
    }
}
