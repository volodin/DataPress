using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;

    internal class RemoteRule  : ModelValidatorRule
    {
        public override ModelValidator Create(IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context)
        {
            StorageValidator<RemoteValidatorData> vldtr = validator as StorageValidator<RemoteValidatorData>;
            if (vldtr == null)
                throw new System.IO.InvalidDataException(
                    "Validator value must be of type StorageValidator<RemoteValidatorData>.");

            var attribute = new RemoteAttribute(vldtr.data.action, vldtr.data.controller);
            this.BindErrorMessageToAttribte(attribute, validator, defaultResourceType);
            if (!String.IsNullOrEmpty(vldtr.data.httpMethod))
                attribute.HttpMethod = vldtr.data.httpMethod;

            return new DataAnnotationsModelValidator<RemoteAttribute>(metadata, context, attribute);
        }
    }

    internal class RemoteValidatorData
    {
        public string action { get; set; }
        public string controller { get; set; }
        public string httpMethod { get; set; }
    }
}
