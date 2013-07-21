using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public abstract class ModelValidatorRule : IModelValidatorRule
    {
        public abstract ModelValidator Create(
            IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context);

        protected void BindErrorMessageToAttribte(
            ValidationAttribute attribute, IStorageValidator validator, Type defaultResourceType)
        {
            if (!String.IsNullOrEmpty(validator.ErrorMessage))
            {
                attribute.ErrorMessage = validator.ErrorMessage;
                return;
            }

            if (!String.IsNullOrEmpty(validator.ErrorMessageResourceName))
            {
                attribute.ErrorMessageResourceName = validator.ErrorMessageResourceName;
                attribute.ErrorMessageResourceType = 
                    validator.ErrorMessageResourceType ?? defaultResourceType;
            }
        }
    }
}
