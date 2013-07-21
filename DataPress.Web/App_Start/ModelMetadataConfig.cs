using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataPress.Web
{
    using DataPress.MVC.ModelMetadata;
    using System.Web.Mvc;

    public static class ModelMetadataConfig
    {
        public static void Register()
        {
            FluentModelMetadata metadataStorage = new FluentModelMetadata();

            MvcMetadataProviderRegistry.Register(metadataStorage);
        }
    }
}