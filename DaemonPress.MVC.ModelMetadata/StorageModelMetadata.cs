using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.ModelMetadata
{
    using System.Web.Mvc;

    public class StorageModelMetadata
    {
        //public bool IsComplexType { get; set; }
        //public bool IsNullableValueType { get; set; }
        
        public bool IsReadOnly { get; set; }
        
        public bool IsRequired { get; set; }

        public bool ConvertEmptyStringToNull { get; set; }

        public bool RequestValidationEnabled { get; set; }

        public bool ShowForDisplay { get; set; }
        public bool ShowForEdit { get; set; }

        public string DisplayName { get; set; }
        public string ShortDisplayName { get; set; }
        public string SimpleDisplayText { get; set; }
        public string NullDisplayText { get; set; }
        public string TemplateHint { get; set; }
        public string Watermark { get; set; }

        private Dictionary<string, object> _AdditionalValues;
        public Dictionary<string, object> AdditionalValues { 
            get { return _AdditionalValues ?? (_AdditionalValues = new Dictionary<string, object>()); } 
        }

        public long nMetadataVersion { get; set; }


        public ModelMetadata ApplyToModelMetadata(ModelMetadata metadata)
        {
            metadata.IsRequired = this.IsRequired;
            metadata.IsReadOnly = this.IsReadOnly;
            
            metadata.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
            metadata.RequestValidationEnabled = this.RequestValidationEnabled;

            metadata.ShowForDisplay = this.ShowForDisplay;
            metadata.ShowForEdit = this.ShowForEdit;

            metadata.DisplayName = this.DisplayName;
            metadata.ShortDisplayName = this.ShortDisplayName;
            metadata.SimpleDisplayText = this.SimpleDisplayText;
            metadata.NullDisplayText = this.NullDisplayText;

            metadata.TemplateHint = this.TemplateHint;
            metadata.Watermark = this.Watermark;

            if (metadata.AdditionalValues != null)
            {
                foreach (string key in this.AdditionalValues.Keys)
                    metadata.AdditionalValues[key] = this.AdditionalValues[key];
            }

            return metadata;

        }
    }
}
