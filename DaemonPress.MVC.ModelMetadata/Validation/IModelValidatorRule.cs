using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;

    public interface IModelValidatorRule
    {
        ModelValidator Create(IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context);
    }
}
