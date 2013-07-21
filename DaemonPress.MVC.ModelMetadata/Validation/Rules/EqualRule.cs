using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;

    internal class EqualRule : ModelValidatorRule
    {
        public override ModelValidator Create(IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context)
        {
            StorageValidator<EqualValidatorData> vldtr = validator as StorageValidator<EqualValidatorData>;
            if (vldtr == null)
                throw new System.IO.InvalidDataException(
                    "Validator value must be of type StorageValidator<EqualValidatorData>.");

            if (vldtr.data.value == null)
                vldtr.data.type = "System.Boolean";

            var objectValue = Convert.ChangeType(vldtr.data.value, Type.GetType(vldtr.data.type, throwOnError: true));

            var attribute = new EqualAttribute(objectValue);
            this.BindErrorMessageToAttribte(attribute, validator, defaultResourceType);

            return new DataAnnotationsModelValidator<EqualAttribute>(metadata, context, attribute);
        }
    }

    internal class EqualValidatorData
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}
