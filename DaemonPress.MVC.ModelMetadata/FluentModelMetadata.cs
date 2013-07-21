using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataPress.MVC.ModelMetadata
{
    public class FluentModelMetadata : IMetadataStorage
    {
        public static string PropertyName<TModel, TProp>(Expression<Func<TModel, TProp>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                return null;

            return memberExpression.Member.Name;
        }

        public class ModelTypeMetadata<TModel> : ModelTypeMetadata
        {
            public PropertyTypeMetadata ForProperty<TProp>(Expression<Func<TModel, TProp>> expression)
            {
                return ForProperty(PropertyName(expression));
            }

        }
        public class ModelTypeMetadata : PropertyTypeMetadata
        {
            public PropertyTypeMetadata ForProperty(string propertyName)
            {
                PropertyTypeMetadata propertyMetadata;
                if (this.PropertyMetadata.TryGetValue(propertyName, out propertyMetadata))
                    return propertyMetadata;

                return null;
            }

            private Dictionary<string, PropertyTypeMetadata> _PropertyMetadata;
            public Dictionary<string, PropertyTypeMetadata> PropertyMetadata
            {
                get
                {
                    return _PropertyMetadata ??
                        (_PropertyMetadata = new Dictionary<string, PropertyTypeMetadata>());
                }
            }
        }
        public class PropertyTypeMetadata
        {
            private StorageModelMetadata _Metadata;
            public StorageModelMetadata Metadata { get { return _Metadata ?? (_Metadata = new StorageModelMetadata()); } }

            private ICollection<IStorageValidator> _Validators;
            public ICollection<IStorageValidator> Validators {
                get { return _Validators ?? (_Validators = new HashSet<IStorageValidator>()); } 
            }

            #region Fluent Metadata

            #region Display

            public PropertyTypeMetadata Display(string Name)
            {
                this.Metadata.DisplayName = Name;

                return this;
            }

            public PropertyTypeMetadata Display(Expression<Func<string>> expression)
            {
                this.Metadata.DisplayName = expression.Compile()();

                return this;
            }

            #endregion // Display

            #endregion // Fluent Metadata

            #region Fluent Validation

            #region Required

            public PropertyTypeMetadata Required()
            {
                Validators.Add(new StorageValidator(VALIDATOR_TYPE.Required));

                return this;
            }

            public PropertyTypeMetadata Required(string ErrorMessage)
            {
                StorageValidator validator = new StorageValidator(VALIDATOR_TYPE.Required)
                {
                    ErrorMessage = ErrorMessage
                };

                Validators.Add(validator);

                return this;
            }

            public PropertyTypeMetadata Required(Expression<Func<string>> expression)
            {
                return Required(expression.Compile()());
            }

            public PropertyTypeMetadata Required(string errorMessageResourceName, Type errorMessageResourceType)
            {
                StorageValidator validator = new StorageValidator(VALIDATOR_TYPE.Required)
                {
                    ErrorMessageResourceName = errorMessageResourceName,
                    ErrorMessageResourceType = errorMessageResourceType
                };

                Validators.Add(validator);

                return this;
            }
            #endregion // Required

            public PropertyTypeMetadata Range(int min, int max, string errorMessage = null)
            {
                var validator = new StorageValidator<RangeValidatorData>(VALIDATOR_TYPE.Range) { ErrorMessage = errorMessage };
                validator.data = new RangeValidatorData() { TypeName = typeof(int).Name, min = min.ToString(), max = max.ToString() };
                Validators.Add(validator);

                return this;
            }

            public PropertyTypeMetadata Range(int min, int max, string errorMessageResourceName, Type errorMessageResourceType)
            {
                var validator = new StorageValidator<RangeValidatorData>(VALIDATOR_TYPE.Range) {
                    ErrorMessageResourceName = errorMessageResourceName,
                    ErrorMessageResourceType = errorMessageResourceType
                };
                validator.data = new RangeValidatorData() { TypeName = typeof(int).Name, min = min.ToString(), max = max.ToString() };

                return this;
            }

            #endregion // Fluent Validation

        }

        protected Dictionary<Type, ModelTypeMetadata> metadataCache = new Dictionary<Type, ModelTypeMetadata>();


        public ModelTypeMetadata ForType<T>()
        {
            return ForType(typeof(T));
        }

        public ModelTypeMetadata ForType(Type modelType)
        {
            ModelTypeMetadata metadata;

            if (metadataCache.TryGetValue(modelType, out metadata))
                return metadata;
            else
            {
                metadataCache[modelType] = metadata;

                return metadata;
            }
        }

        //public TypeMetadata ForProperty<T>(string propertyName)
        //{

        //}

        //public TypeMetadata ForProperty<TModel, TProp>(Expression<Func<TModel, TProp>> expression)
        //{

        //}

        #region IMetadataStorage Members

        public void ClearCache(Type modelType)
        {
            metadataCache.Remove(modelType);
        }

        public bool HasMetadataFor(Type modelType)
        {
            return metadataCache.ContainsKey(modelType);
        }

        public StorageModelMetadata GetModelMetadata(Type modelType)
        {
            ModelTypeMetadata typeMetadata;
            if (metadataCache.TryGetValue(modelType, out typeMetadata))
                return typeMetadata.Metadata;

            return null;
        }

        public StorageModelMetadata GetModelMetadata(Type modelType, string propertyName)
        {
            ModelTypeMetadata typeMetadata;
            if (metadataCache.TryGetValue(modelType, out typeMetadata))
            {
                PropertyTypeMetadata propertyMetadata = typeMetadata.ForProperty(propertyName);
                return propertyMetadata != null ? propertyMetadata.Metadata : null;

                //PropertyTypeMetadata propertyMetadata;
                //if (typeMetadata.PropertyMetadata.TryGetValue(propertyName, out propertyMetadata))
                //    return propertyMetadata.Metadata;
            }

            return null;
        }

        public IEnumerable<IStorageValidator> GetValidators(Type modelType)
        {
            ModelTypeMetadata typeMetadata;
            if (metadataCache.TryGetValue(modelType, out typeMetadata))
                return typeMetadata.Validators;

            return null;
        }

        public IEnumerable<IStorageValidator> GetValidators(Type modelType, string propertyName)
        {
            ModelTypeMetadata typeMetadata;
            if (metadataCache.TryGetValue(modelType, out typeMetadata))
            {
                PropertyTypeMetadata propertyMetadata = typeMetadata.ForProperty(propertyName);
                return propertyMetadata != null ? propertyMetadata.Validators : null;

                //PropertyTypeMetadata propertyMetadata;
                //if (typeMetadata.PropertyMetadata.TryGetValue(propertyName, out propertyMetadata))
                //    return propertyMetadata.Validators;
            }

            return null;
        }

        #endregion //IMetadataStorage Members
    }
}
