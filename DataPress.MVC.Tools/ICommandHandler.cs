using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Tools
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        object Execute(TCommand command);
    }
}
