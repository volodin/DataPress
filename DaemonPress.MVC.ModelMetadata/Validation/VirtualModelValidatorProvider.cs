using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using StructureMap;

    public class VirtualModelValidatorProvider : ModelValidatorProvider
    {
        protected IMetadataStorage metadataStorage;
        protected IContainer container;
        protected Type defaultResourceType; 

        public VirtualModelValidatorProvider(IMetadataStorage storage, Type defaultResourceType)
        {
            metadataStorage = storage;
            this.defaultResourceType = defaultResourceType;

            container = new Container(x => x.AddRegistry<RuleRegistry>());
        }

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            IEnumerable<IStorageValidator> validators;

            if (String.IsNullOrEmpty(metadata.PropertyName))
                validators = metadataStorage.GetValidators(metadata.ContainerType);
            else
                validators = metadataStorage.GetValidators(metadata.ContainerType, metadata.PropertyName);

            if (validators == null)
                yield break;

            foreach (var validator in validators)
            {
                var rule = container.GetInstance<IModelValidatorRule>(validator.Name);
                if (rule != null)
                    yield return rule.Create(validator, defaultResourceType, metadata, context);
            }
        }
    }
}
