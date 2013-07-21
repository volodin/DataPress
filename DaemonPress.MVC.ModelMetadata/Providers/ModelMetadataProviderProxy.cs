using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DataPress.MVC.ModelMetadata
{
    public class ModelMetadataProviderProxy : ModelMetadataProvider
    {
        protected ModelMetadataProvider defaultProvider;

        protected List<IVirtualMetadataProvider> _VirtualProviders;
        public ICollection<IVirtualMetadataProvider> VirtualProviders { 
            get { return _VirtualProviders ?? (_VirtualProviders = new List<IVirtualMetadataProvider>()); } 
        }

        public ModelMetadataProviderProxy(IVirtualMetadataProvider virtualProvider, ModelMetadataProvider defaultProvider)
        {
            this.defaultProvider = (defaultProvider ?? new EmptyModelMetadataProvider());

            this.VirtualProviders.Add(virtualProvider);
        }

        public ModelMetadataProviderProxy(IEnumerable<IVirtualMetadataProvider> virtualProviders, ModelMetadataProvider defaultProvider)
        {
            this.defaultProvider = (defaultProvider ?? new EmptyModelMetadataProvider());

            this._VirtualProviders = new List<IVirtualMetadataProvider>();
            this._VirtualProviders.AddRange(virtualProviders);
        }

        public override IEnumerable<System.Web.Mvc.ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            foreach(var vProvider in VirtualProviders)
                if (vProvider.HasMetadataFor(containerType))
                    return vProvider.GetMetadataForProperties(container, containerType);
            
            return defaultProvider.GetMetadataForProperties(container, containerType);
        }

        public override System.Web.Mvc.ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            foreach (var vProvider in VirtualProviders)
                if (vProvider.HasMetadataFor(containerType))
                    return vProvider.GetMetadataForProperty(modelAccessor, containerType, propertyName);

            return defaultProvider.GetMetadataForProperty(modelAccessor, containerType, propertyName);
        }
     

        public override System.Web.Mvc.ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            foreach (var vProvider in VirtualProviders)
                if (vProvider.HasMetadataFor(modelType))
                    return vProvider.GetMetadataForType(modelAccessor, modelType);

            return defaultProvider.GetMetadataForType(modelAccessor, modelType);
        }
       
    }
}
