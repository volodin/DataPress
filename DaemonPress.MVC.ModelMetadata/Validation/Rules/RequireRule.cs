using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    internal class RequireRule : ModelValidatorRule
    {
        public const string Name = "Required";

        public override ModelValidator Create(IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context)
        {
            metadata.IsRequired = false;

            var attribute = new RequiredAttribute();
            this.BindErrorMessageToAttribte(attribute, validator, defaultResourceType);

            return new RequiredAttributeAdapter(metadata, context, attribute);
        }
    }
}
