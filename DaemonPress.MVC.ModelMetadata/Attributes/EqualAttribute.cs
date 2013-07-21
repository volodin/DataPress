/// Copyright (c) 2011 Andrey Veselov. All rights reserved.
/// WebSite: http://andrey.moveax.ru 
/// Email: andrey@moveax.ru
/// This source is subject to the Microsoft Public License (Ms-PL).

namespace DataPress.MVC.ModelMetadata
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EqualAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly object _valueToCompare;

        public EqualAttribute(object valueToCompare)
            : base("The {0} must be the same as the {1}.")
        {
            this._valueToCompare = valueToCompare;
        }

        #region ValidationAttribute overrides

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                this.ErrorMessageString, name, this._valueToCompare);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (this._valueToCompare == null) {
                throw new NullReferenceException();
            }

            if (value == null) {
                return ValidationResult.Success;
            }

            if (value.Equals(this._valueToCompare)) {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                this.FormatErrorMessage(validationContext.DisplayName));
        }

        #endregion

        #region IClientValidatable members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule() {
                ValidationType = "equal",
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName)
            };

            rule.ValidationParameters.Add("valuetocompare", this._valueToCompare);

            yield return rule;
        }

        #endregion
    }
}