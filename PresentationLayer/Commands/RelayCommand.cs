using System.Windows.Input;

namespace PresentationLayer.Commands
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Delegate that holds the method to be executed
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Delegate that holds the method that determine if the command can execute
        /// </summary>
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Automatically re-query the command's execution status.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Check if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>bool</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        /// <summary>
        /// Perform the action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
