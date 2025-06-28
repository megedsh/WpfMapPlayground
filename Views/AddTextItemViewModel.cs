using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using PropertyTools.DataAnnotations;

namespace WpfMapPlayground.Views
{
    
    internal class AddTextItemViewModel : ObservableObject, INotifyDataErrorInfo, IDataErrorInfo
    {
        private readonly Dictionary<string, ValidationResult> m_errors = new Dictionary<string, ValidationResult>();
        private          string                               m_name;
        private          string                               m_text;

        [System.ComponentModel.Category("Wpf Playground|Create Text Item")]
        public string Name
        {
            get => m_name;
            set => SetProperty(ref m_name, value);            
        }

        [Height(100)]
        [Width(500)]        
        public string Text
        {
            get => m_text;
            set => SetProperty(ref m_text, value);            
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            if (m_errors.TryGetValue(propertyName, out ValidationResult error))
            {
                yield return error;
            }
        }

        bool INotifyDataErrorInfo.HasErrors => m_errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        [System.ComponentModel.Browsable(false)]
        public string Error => m_errors.Values.Select(e => e.ErrorMessage).FirstOrDefault() ?? string.Empty;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):
                    {
                        return doValidate(columnName,()=> string.IsNullOrEmpty(Name), "Mandatory");
                    }
                    case nameof(Text):
                    {
                        return doValidate(columnName, () => string.IsNullOrEmpty(Text), "Mandatory");
                    }
                }

                return null;
            }
        }

        private string doValidate(string columnName, Func<bool> func, string msg)
        {
            m_errors.TryGetValue(columnName, out ValidationResult current);
            if (func())
            {
                m_errors[columnName] = new ValidationResult(msg);
                if (current == null || current.ErrorMessage != msg)
                {
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(columnName));
                }                
                return msg;
            }
            else
            {
                if (m_errors.ContainsKey(columnName))
                {                    
                    m_errors.Remove(columnName);
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(columnName));
                }
            }
            return null;            
        }
    }
}