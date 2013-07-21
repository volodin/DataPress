using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class RegularExpressionRule : ModelValidatorRule
    {
        public const string Name = "RegularExpression";

        public override ModelValidator Create(
            IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context)
        {

            StorageValidator<string> vldtr = validator as StorageValidator<string>;
            if (vldtr == null)
                throw new System.IO.InvalidDataException(
                    "Validator value must be of type StorageValidator<string>.");

            //string regExp = null;// xmlElement.GetValueOrNull("regexp");
            //try
            //{
            //    regExp = validator.data.regExp;
            //}
            //catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            //{
            //    throw new System.IO.InvalidDataException(
            //        string.Format("The regular expression was not set. Element: {0}", validator.Name));
            //}

            var attribute = new RegularExpressionAttribute(vldtr.data);
            this.BindErrorMessageToAttribte(attribute, validator, defaultResourceType);

            return new RegularExpressionAttributeAdapter(metadata, context, attribute);
        }
    }

    //internal class RegularExpressionValidatorData
    //{
    //    public string RegExp { get; set; }
    //}
}
