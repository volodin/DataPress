using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class RangeRule : ModelValidatorRule
    {
        //public const string Name = "Range";
        public VALIDATOR_TYPE ValidatorType { get { return VALIDATOR_TYPE.Range; } }

        public override ModelValidator Create(
            IStorageValidator validator, Type defaultResourceType, ModelMetadata metadata, ControllerContext context)
        {

            StorageValidator<RangeValidatorData> vldtr = validator as StorageValidator<RangeValidatorData>;
            if (vldtr == null)
                throw new System.IO.InvalidDataException(
                    "Validator value must be of type StorageValidator<RangeValidatorData>.");

            //string minValue = null;
            //try
            //{
            //    minValue = validator.data.min;
            //}
            //catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            //{
            //    throw new System.IO.InvalidDataException(
            //        string.Format("Min value can't be null. Element: {0}.", validator.Name));
            //}

            //string maxValue = null;  //xmlElement.GetValueOrNull("max");
            //try
            //{
            //    maxValue = validator.data.max;
            //}
            //catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            //{
            //    throw new System.IO.InvalidDataException(
            //        string.Format("Max value can't be null. Element: {0}.", validator.Name));
            //}

            //string typeName = null; //xmlElement.GetValueOrNull("type");
            //try
            //{
            //    typeName = validator.data.typeName ?? "System.Int32";
            //}
            //catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            //{
            //    throw new System.IO.InvalidDataException(
            //        string.Format("Max value can't be null. Element: {0}.", validator.Name));
            //}

            Type rangeType = Type.GetType(vldtr.data.TypeName);
            if (rangeType == null)
            {
                throw new System.IO.InvalidDataException(
                    string.Format("Unknown type: {0}. Element: {1}.", vldtr.data.TypeName, validator.Name));
            }

            var attribute = new RangeAttribute(rangeType, vldtr.data.min,vldtr.data.max);
            this.BindErrorMessageToAttribte(attribute, validator, defaultResourceType);

            return new RangeAttributeAdapter(metadata, context, attribute);
        }
    }

    //internal class RangeStorageValidator : StorageValidator
    //{
    //    string Type
    //}

    public class RangeValidatorData
    {
        public string TypeName { get; set; }
        public string min { get; set; }
        public string max { get; set; }
    }

}
