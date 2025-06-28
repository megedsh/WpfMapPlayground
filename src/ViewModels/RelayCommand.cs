using System;
using System.Windows.Input;

namespace WpfMapPlayground.ViewModels;

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