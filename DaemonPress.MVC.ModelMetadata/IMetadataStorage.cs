using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    public interface IMetadataStorage
    {
        //long GetVersion(Type modelType);
        void ClearCache(Type modelType);

        bool HasMetadataFor(Type modelType);

        StorageModelMetadata GetModelMetadata(Type modelType);
        StorageModelMetadata GetModelMetadata(Type modelType, string propertyName);

        IEnumerable<IStorageValidator> GetValidators(Type modelType);
        IEnumerable<IStorageValidator> GetValidators(Type modelType, string propertyName);

    }

    public interface IStorageValidator
    {
        string Name { get; set; }
        string ErrorMessage { get; set; }
        string ErrorMessageResourceName { get; set; }
        Type ErrorMessageResourceType { get; set; }

        //dynamic data { get; set; }
    }

    public enum VALIDATOR_TYPE
    {
        Required,
        StringLength,
        Range,
        RegularExpression,
        Remote,
        Equal
    }
}
