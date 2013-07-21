using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    public class StorageValidator : IStorageValidator
    {
        public StorageValidator() { }
        public StorageValidator(VALIDATOR_TYPE type) { Name = type.ToString(); }

        #region IStorageValidator Members

        public string Name { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorMessageResourceName { get; set; }

        public Type ErrorMessageResourceType { get; set; }  

        #endregion // IStorageValidator Members
    }

    public class StorageValidator<T> : StorageValidator
    {
        public StorageValidator() { }
        public StorageValidator(VALIDATOR_TYPE type) : base(type) { }

        public T data { get; set; }
    }
}
