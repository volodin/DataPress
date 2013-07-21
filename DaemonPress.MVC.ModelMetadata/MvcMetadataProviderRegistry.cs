using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DataPress.MVC.ModelMetadata
{
    public static class MvcMetadataProviderRegistry
    {
        public static void Register(IMetadataStorage metadataStorage)
        {
            VirtualModelMetadataProvider provider = new VirtualModelMetadataProvider(metadataStorage);

            var matadataProvider = new ModelMetadataProviderProxy(provider, ModelMetadataProviders.Current);

            ModelMetadataProviders.Current = matadataProvider;

            ModelValidatorProviders.Providers.Insert(0, new VirtualModelValidatorProvider(metadataStorage, null));
        }
    }
}
