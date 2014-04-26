using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HLP.Comum.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _method;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> method)
            : this(method, null)
        { 
        }

        public RelayCommand(Action<object> method, Predicate<object> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _method.Invoke(parameter);
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;

            if (canExecuteChanged != null)
                canExecuteChanged(this, e);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }
    }
}
