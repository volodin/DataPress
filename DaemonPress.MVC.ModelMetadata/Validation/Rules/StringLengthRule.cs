using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    internal class StringLengthRule : ModelValidatorRule
    {
        public const string Name = "StringLength";
        public override ModelValidator Create(IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context)
        {
            StorageValidator<int> vldtr = validator as StorageValidator<int>;
            if (vldtr == null)
                throw new System.IO.InvalidDataException(
                    "Validator value must be of type StorageValidator<int>.");

            //int maxLength = -1;
            //try
            //{
            //    maxLength = validator.data.maxLenght;
            //}
            //catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            //{
            //    throw new System.IO.InvalidDataException(
            //        string.Format("The maximum string length was not set. Element: {0}", validator.Name));
            //}

            var attribute = new StringLengthAttribute(vldtr.data);
            this.BindErrorMessageToAttribte(attribute, validator, defaultResourceType);

            return new StringLengthAttributeAdapter(metadata, context, attribute);
        }
    }
}
