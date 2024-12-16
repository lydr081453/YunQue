using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AdminForm.Configuration
{
    public class ProviderSettingsCollection : ConfigurationElementCollection, IEnumerable<ProviderSettings>
    {
        class EnumeratorWrapper : IEnumerator<ProviderSettings>
        {
            System.Collections.IEnumerator _Inner;
            public EnumeratorWrapper(System.Collections.IEnumerator inner)
            {
                _Inner = inner;
            }

            #region IEnumerator<ProviderSettings> 成员

            ProviderSettings IEnumerator<ProviderSettings>.Current
            {
                get { return (ProviderSettings)_Inner.Current; }
            }

            #endregion

            #region IDisposable 成员

            void IDisposable.Dispose()
            {
                _Inner = null;
            }

            #endregion

            #region IEnumerator 成员

            object System.Collections.IEnumerator.Current
            {
                get { return _Inner.Current; }
            }

            bool System.Collections.IEnumerator.MoveNext()
            {
                return _Inner.MoveNext();
            }

            void System.Collections.IEnumerator.Reset()
            {
                _Inner.Reset();
            }

            #endregion
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderSettings)element).Key;
        }

        public ProviderSettings this[int index]
        {
            get
            {
                return (ProviderSettings)this.BaseGet(index);
            }
        }

        public ProviderSettings this[Type key]
        {
            get
            {
                return (ProviderSettings)this.BaseGet(key);
            }
        }

        #region IEnumerable<ProviderSettings> 成员

        public new IEnumerator<ProviderSettings> GetEnumerator()
        {
            return new EnumeratorWrapper(base.GetEnumerator());
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return base.GetEnumerator();
        }

        #endregion
    }
}
