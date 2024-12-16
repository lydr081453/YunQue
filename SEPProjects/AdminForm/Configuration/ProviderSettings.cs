using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AdminForm.Configuration
{
    public class ProviderSettings : ConfigurationElement
    {
        class PropertyConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;

                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return Type.GetType((string)value);

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return ((Type)value).AssemblyQualifiedName;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        private static ConfigurationPropertyCollection _Properties;
        private static readonly ConfigurationProperty _ValueProperty;
        private static readonly ConfigurationProperty _KeyProperty;

        static ProviderSettings()
        {
            _Properties = new ConfigurationPropertyCollection();
            _KeyProperty = new ConfigurationProperty("key", typeof(Type), null, new PropertyConverter(), null, ConfigurationPropertyOptions.IsRequired);
            _ValueProperty = new ConfigurationProperty("value", typeof(Type), null, new PropertyConverter(), null, ConfigurationPropertyOptions.IsRequired);
            _Properties.Add(_KeyProperty);
            _Properties.Add(_ValueProperty);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        public Type Value
        {
            get
            {
                return (Type)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }

        public Type Key
        {
            get
            {
                return (Type)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }
    }
}
