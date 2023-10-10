using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scan.Core.ViewModel.Base
{
    public class RelayCommand : ICommand
    {
        #region Private Members
        private readonly Action mAction;
        #endregion

        #region Public Events
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
