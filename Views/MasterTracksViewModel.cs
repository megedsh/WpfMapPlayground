using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WpfMapPlayground.Views
{
    public class MasterTracksViewModel : ObservableObject
    {
        private TimedLineItem m_selectedTimedLine;

        public object SelectedItems { get; set; }

        public event Action<object> OnAddIem;

        public MasterTracksViewModel()
        {
            AddToMapCommand = new RelayCommand<object>(o => { OnAddIem?.Invoke(o); });
        }

        public TimedLineItem SelectedTimedLine
        {
            get => m_selectedTimedLine;
            set => SetProperty(ref m_selectedTimedLine, value);
        }

        public ICommand AddToMapCommand { get; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action m_action;

        public RelayCommand(Action action) => m_action = action;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            m_action?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> m_action;

        public RelayCommand(Action<T> action) => m_action = action;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            m_action?.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}