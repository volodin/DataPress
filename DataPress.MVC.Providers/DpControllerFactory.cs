using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Providers
{
    using System.Web.Mvc;
    using System.Reflection;
    using StructureMap;

    class DpControllerFactory : DefaultControllerFactory
    {
        //protected string _DefaultNamespacel;
        protected IContainer _Container;
        protected IControllerStorage _Storage;
        //Dictionary<string, Assembly> _LoadedAssemblies = new Dictionary<string, Assembly>();

        //Dictionary<string, Type> _ControllerTypes = new Dictionary<string, Type>();

        public DpControllerFactory(IContainer container, IControllerStorage controllerStorage = null)
        {
            _Storage = controllerStorage;
            _Container = container;
            
        }

        protected override Type GetControllerType(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            if (_Storage != null)
            {
                Type controllerType = _Storage.GetControllerType(controllerName);
                if (controllerType != null) return controllerType;
            }

            return base.GetControllerType(requestContext, controllerName);
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController controller = _Container.GetInstance(controllerType) as IController;
            if (controller != null) return controller;

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
