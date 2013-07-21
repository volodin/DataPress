using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Tools
{
    using System.Web.Mvc;

    public class DpBaseController : Controller
    {
        //protected IDependencyResolver 
        protected const string MODEL_STATE_KEY = "C3B727E5-D517-4213-9A31-D6BC97ADC5FC";

        protected ActionResult ExecuteCommand<TCommand>(TCommand command,
            ActionResult successResult, ActionResult notValidResult) 
            where TCommand : class
        {
            if (!TryUpdateModel(command))
            {
                TempData[MODEL_STATE_KEY] = ModelState;
                return notValidResult;
            }
            
            var cmdHandler = StructureMap.ObjectFactory.GetInstance<ICommandHandler<TCommand>>();
            if (cmdHandler == null)
                throw new ApplicationException("Can't resolve ICommandHandler<" + typeof(TCommand).Name + ">");

            cmdHandler.Execute(command);

            return successResult;
        }

        protected ActionResult ExecuteCommand<TCommand>(TCommand command,
             Func<object, ActionResult> successHandler, ActionResult failureResult)
            where TCommand : class
        {
            if (!TryUpdateModel(command))
            {
                TempData[MODEL_STATE_KEY] = ModelState;
                return failureResult;
            }

            var cmdHandler = StructureMap.ObjectFactory.GetInstance<ICommandHandler<TCommand>>();
            if (cmdHandler == null)
                throw new ApplicationException("Can't resolve ICommandHandler<" + typeof(TCommand).Name + ">");

            object res = cmdHandler.Execute(command);

            return successHandler(res);
        }

        protected ActionResult ExecuteCommand<TCommand, TRes>(TCommand command,
            Func<TRes, ActionResult> successHandler, ActionResult notValidResult)
            where TCommand : class
        {
            if (!TryUpdateModel(command))
            {
                TempData[MODEL_STATE_KEY] = ModelState;
                return notValidResult;
            }

            var cmdHandler = StructureMap.ObjectFactory.GetInstance<ICommandAction<TCommand, TRes>>();
            if (cmdHandler == null)
                throw new ApplicationException("Can't resolve ICommandAction<" + typeof(TCommand).Name + ">");

            TRes res = cmdHandler.Execute(command);

            return successHandler(res);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (TempData[MODEL_STATE_KEY] != null && 
                !ModelState.Equals(TempData[MODEL_STATE_KEY]))
                ModelState.Merge((ModelStateDictionary)TempData[MODEL_STATE_KEY]);
            
            base.OnActionExecuted(filterContext);
        }
    }
}
