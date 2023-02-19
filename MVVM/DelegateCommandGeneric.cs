using System;
using System.Windows.Input;

namespace WpfApp1.MVVM
{
    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<T> executeMethod, Func<bool>? canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object? parameter) 
            => _canExecuteMethod is null || _canExecuteMethod();

        public void Execute(object? parameter)
        {
            if (parameter is not T arg)
            {
                throw new ArgumentNullException(nameof(parameter));
            }
            _executeMethod(arg);
        }

        private readonly Action<T> _executeMethod;
        private readonly Func<bool>? _canExecuteMethod;
    }
}
