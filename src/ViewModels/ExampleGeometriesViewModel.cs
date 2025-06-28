using System;
using System.Windows.Input;
using WpfMapPlayground.Models;
using WpfMapPlayground.Views;

namespace WpfMapPlayground.ViewModels
{
    public class ExampleGeometriesViewModel : ObservableObject
    {
        private IItemForMap m_selectedTimedLine;
        
        public event Action<object> OnAddIem;

        public ExampleGeometriesViewModel()
        {
            AddToMapCommand = new RelayCommand<object>(o => { OnAddIem?.Invoke(o); });
        }

        public IItemForMap SelectedItem
        {
            get => m_selectedTimedLine;
            set => SetProperty(ref m_selectedTimedLine, value);
        }

        public ICommand AddToMapCommand { get; }
    }

}