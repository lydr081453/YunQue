using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AdminForm.Configuration
{
    public class SecuritySettings : System.Configuration.ConfigurationElement
    {
        class EncryptionKeyPropertyConverter : System.ComponentModel.TypeConverter
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
                    return Convert.FromBase64String((string)value);

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return Convert.ToBase64String((byte[])value);
                
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        private static ConfigurationPropertyCollection _Properties;
        private static readonly ConfigurationProperty _MaxInvalidPasswordAttemptsProperty;
        private static readonly ConfigurationProperty _PasswordAttemptWindowProperty;
        private static readonly ConfigurationProperty _AutoLockoutDurationProperty;
        private static readonly ConfigurationProperty _EncryptionKeyProperty;

        static SecuritySettings()
        {
            _Properties = new ConfigurationPropertyCollection();
            _MaxInvalidPasswordAttemptsProperty = new ConfigurationProperty("maxInvalidPasswordAttempts", typeof(int), 5);
            _PasswordAttemptWindowProperty = new ConfigurationProperty("passwordAttemptWindow", typeof(TimeSpan), new TimeSpan(0, 10, 0));
            _AutoLockoutDurationProperty = new ConfigurationProperty("autoLockoutDuration", typeof(TimeSpan), new TimeSpan(24, 0, 0));
            _EncryptionKeyProperty = new ConfigurationProperty("encryptionKey", typeof(byte[]), null, new EncryptionKeyPropertyConverter(), null, ConfigurationPropertyOptions.None);

            _Properties.Add(_MaxInvalidPasswordAttemptsProperty);
            _Properties.Add(_PasswordAttemptWindowProperty);
            _Properties.Add(_AutoLockoutDurationProperty);
            _Properties.Add(_EncryptionKeyProperty);
        }
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        public TimeSpan PasswordAttemptWindow
        {
            get
            {
                return (TimeSpan)this["passwordAttemptWindow"];

            }
            set
            {
                this["passwordAttemptWindow"] = value;
            }
        }
        public int MaxInvalidPasswordAttempts
        {
            get
            {
                return (int)this["maxInvalidPasswordAttempts"];

            }
            set
            {
                this["maxInvalidPasswordAttempts"] = value;
            }
        }

        public TimeSpan AutoLockoutDuration
        {
            get
            {
                return (TimeSpan)this["autoLockoutDuration"];

            }
            set
            {
                this["autoLockoutDuration"] = value;
            }
        }

        public byte[] EncryptionKey
        {
            get
            {
                return (byte[])this["encryptionKey"];

            }
            set
            {
                this["encryptionKey"] = value;
            }
        }
    }
}
