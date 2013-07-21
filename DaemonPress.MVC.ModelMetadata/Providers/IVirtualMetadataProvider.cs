using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    public interface IVirtualMetadataProvider
    {
        //DateTime GetMetadataTimestamp(Type modelType);
        bool HasMetadataFor(Type modelType);

        //VirtualModelMetadata GetMetadata(Type modelType);
        //VirtualModelMetadata GetMetadata(Type modelType, string propertyName);

        void ClearCache(Type modelType);

        IEnumerable<System.Web.Mvc.ModelMetadata> GetMetadataForProperties(
            object container, Type containerType);

        System.Web.Mvc.ModelMetadata GetMetadataForProperty(
            Func<object> modelAccessor, Type containerType, string propertyName);

        System.Web.Mvc.ModelMetadata GetMetadataForType(
            Func<object> modelAccessor, Type modelType);
    }
}
