using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Providers
{
    public interface IControllerStorage
    {
        Type GetControllerType(string controllerName);
        void ClearCache(string controllerName);
    }
}
