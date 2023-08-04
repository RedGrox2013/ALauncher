using System;
using System.Windows.Input;

namespace ALauncher
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object?> executeAction,
            Func<object?, bool>? canExecute = null)
        {
            _execute = executeAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute(parameter);
        }

        public void Execute(object? parameter) => _execute(parameter);
    }
}
