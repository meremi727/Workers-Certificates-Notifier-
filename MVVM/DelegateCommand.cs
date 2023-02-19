using System;
using System.Windows.Input;

namespace WpfApp1.MVVM
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action executeMethod, Func<bool>? canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute() => _canExecuteMethod is null || _canExecuteMethod();

        public void Execute() => _executeMethod();

        bool ICommand.CanExecute(object? parameter) => CanExecute();

        void ICommand.Execute(object? parameter) => Execute();

        private readonly Action _executeMethod;
        private readonly Func<bool>? _canExecuteMethod;
    }
}
