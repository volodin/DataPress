using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Tools
{
    public interface ICommandAction<TCommand, TRes> where TCommand : class
    {
        TRes Execute(TCommand command);
    }
}
