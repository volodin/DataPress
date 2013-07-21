using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;
    using System.ComponentModel;
    using System.Globalization;

    public class VirtualModelMetadataProvider : ModelMetadataProvider, IVirtualMetadataProvider
    {
        protected IMetadataStorage metadataStorage;

        public VirtualModelMetadataProvider(IMetadataStorage storage)
        {
            metadataStorage = storage;
        }

        private static void ApplyMetadataAwareAttributes(IEnumerable<Attribute> attributes, ModelMetadata result)
        {
            foreach (IMetadataAware awareAttribute in attributes.OfType<IMetadataAware>())
            {
                awareAttribute.OnMetadataCreated(result);
            }
        }

        protected virtual ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = new ModelMetadata(
                    this, containerType, modelAccessor, modelType, propertyName);

            //if (!metadataRepository.HasMetadataFor(containerType))
            //    return metadata;

            if (String.IsNullOrEmpty(propertyName))
            {
                var virualMatadata = metadataStorage.GetModelMetadata(containerType);
                if (virualMatadata != null)
                    return virualMatadata.ApplyToModelMetadata(metadata);
            }
            else
            {
                var virualMatadata = metadataStorage.GetModelMetadata(containerType, propertyName);
                if (virualMatadata != null)
                    return virualMatadata.ApplyToModelMetadata(metadata);
            }

            return metadata;
        }

        //protected virtual IEnumerable<Attribute> FilterAttributes(Type containerType, PropertyDescriptor propertyDescriptor, IEnumerable<Attribute> attributes)
        //{
        //    if (typeof(ViewPage).IsAssignableFrom(containerType) || typeof(ViewUserControl).IsAssignableFrom(containerType))
        //    {
        //        return attributes.Where(a => !(a is ReadOnlyAttribute));
        //    }

        //    return attributes;
        //}


        private IEnumerable<ModelMetadata> GetMetadataForPropertiesImpl(object container, Type containerType)
        {
            foreach (PropertyDescriptor property in GetTypeDescriptor(containerType).GetProperties())
            {
                Func<object> modelAccessor = container == null ? null : GetPropertyValueAccessor(container, property);
                yield return GetMetadataForProperty(modelAccessor, containerType, property);
            }
        }

       

        protected virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
        {
            IEnumerable<Attribute> attributes = propertyDescriptor.Attributes.Cast<Attribute>();//FilterAttributes(containerType, propertyDescriptor, propertyDescriptor.Attributes.Cast<Attribute>());
            ModelMetadata result = CreateMetadata(attributes, containerType, modelAccessor, propertyDescriptor.PropertyType, propertyDescriptor.Name);
            ApplyMetadataAwareAttributes(attributes, result);
            return result;
        }

       

        private static Func<object> GetPropertyValueAccessor(object container, PropertyDescriptor property)
        {
            return () => property.GetValue(container);
        }

        protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return TypeDescriptorHelper.Get(type);
        }

        #region IVirtualMetadataProvider Members

        public bool HasMetadataFor(Type modelType)
        {
            return metadataStorage.HasMetadataFor(modelType);
        }

        public void ClearCache(Type modelType)
        {
            metadataStorage.ClearCache(modelType);
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            return GetMetadataForPropertiesImpl(container, containerType);
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            ICustomTypeDescriptor typeDescriptor = GetTypeDescriptor(containerType);
            PropertyDescriptor property = typeDescriptor.GetProperties().Find(propertyName, true);
            if (property == null)
            {
                throw new ArgumentException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "Property {0}.{1} not found",
                        containerType.FullName, propertyName));
            }

            return GetMetadataForProperty(modelAccessor, containerType, property);
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            IEnumerable<Attribute> attributes = GetTypeDescriptor(modelType).GetAttributes().Cast<Attribute>();
            ModelMetadata result = CreateMetadata(attributes, null /* containerType */, modelAccessor, modelType, null /* propertyName */);
            ApplyMetadataAwareAttributes(attributes, result);
            return result;
        }

        #endregion // IVirtualMetadataProvider Members
    }

    internal static class TypeDescriptorHelper
    {
        private static readonly MockMetadataProvider _mockMetadataProvider = new MockMetadataProvider();

        public static ICustomTypeDescriptor Get(Type type)
        {
            return _mockMetadataProvider.GetTypeDescriptor(type);
        }

        // System.Web.Mvc.TypeDescriptorHelpers is internal, so this mock subclassed type provides
        // access to it via the GetTypeDescriptor() virtual method.
        private sealed class MockMetadataProvider : AssociatedMetadataProvider
        {
            protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
            {
                throw new NotImplementedException();
            }

            public new ICustomTypeDescriptor GetTypeDescriptor(Type type)
            {
                return base.GetTypeDescriptor(type);
            }
        }
    }

}


/*
// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
    // This class provides a good implementation of ModelMetadataProvider for people who will be
    // using traditional classes with properties. It uses the buddy class support from
    // DataAnnotations, and consolidates the three operations down to a single override
    // for reading the attribute values and creating the metadata class.
    public abstract class AssociatedMetadataProvider : ModelMetadataProvider
    {
        private static void ApplyMetadataAwareAttributes(IEnumerable<Attribute> attributes, ModelMetadata result)
        {
            foreach (IMetadataAware awareAttribute in attributes.OfType<IMetadataAware>())
            {
                awareAttribute.OnMetadataCreated(result);
            }
        }

        protected abstract ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName);

        protected virtual IEnumerable<Attribute> FilterAttributes(Type containerType, PropertyDescriptor propertyDescriptor, IEnumerable<Attribute> attributes)
        {
            if (typeof(ViewPage).IsAssignableFrom(containerType) || typeof(ViewUserControl).IsAssignableFrom(containerType))
            {
                return attributes.Where(a => !(a is ReadOnlyAttribute));
            }

            return attributes;
        }

        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            if (containerType == null)
            {
                throw new ArgumentNullException("containerType");
            }

            return GetMetadataForPropertiesImpl(container, containerType);
        }

        private IEnumerable<ModelMetadata> GetMetadataForPropertiesImpl(object container, Type containerType)
        {
            foreach (PropertyDescriptor property in GetTypeDescriptor(containerType).GetProperties())
            {
                Func<object> modelAccessor = container == null ? null : GetPropertyValueAccessor(container, property);
                yield return GetMetadataForProperty(modelAccessor, containerType, property);
            }
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            if (containerType == null)
            {
                throw new ArgumentNullException("containerType");
            }
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(MvcResources.Common_NullOrEmpty, "propertyName");
            }

            ICustomTypeDescriptor typeDescriptor = GetTypeDescriptor(containerType);
            PropertyDescriptor property = typeDescriptor.GetProperties().Find(propertyName, true);
            if (property == null)
            {
                throw new ArgumentException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        MvcResources.Common_PropertyNotFound,
                        containerType.FullName, propertyName));
            }

            return GetMetadataForProperty(modelAccessor, containerType, property);
        }

        protected virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
        {
            IEnumerable<Attribute> attributes = FilterAttributes(containerType, propertyDescriptor, propertyDescriptor.Attributes.Cast<Attribute>());
            ModelMetadata result = CreateMetadata(attributes, containerType, modelAccessor, propertyDescriptor.PropertyType, propertyDescriptor.Name);
            ApplyMetadataAwareAttributes(attributes, result);
            return result;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }

            IEnumerable<Attribute> attributes = GetTypeDescriptor(modelType).GetAttributes().Cast<Attribute>();
            ModelMetadata result = CreateMetadata(attributes, null , modelAccessor, modelType, null );
            ApplyMetadataAwareAttributes(attributes, result);
            return result;
        }

        private static Func<object> GetPropertyValueAccessor(object container, PropertyDescriptor property)
        {
            return () => property.GetValue(container);
        }

        protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return TypeDescriptorHelper.Get(type);
        }
 
    }
}
*/