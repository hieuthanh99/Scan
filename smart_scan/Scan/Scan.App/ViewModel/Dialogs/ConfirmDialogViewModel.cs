using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scan.App
{
    public class ConfirmDialogViewModel : BaseDialogViewModel
    {
        public string OkText { get; set; }
        public string CancelText { get; set; }
        public bool CancelButtonVisibility { get; set; } = true;

        public string ContentText { get; set; }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ConfirmDialogViewModel()
        {
            OkCommand = new RelayParameterizedCommand((param) => handleOkCommand(param));
            CancelCommand = new RelayParameterizedCommand((param) => handleCancelCommand(param));
        }

        private void handleOkCommand(object param)
        {
            var pass = (param as ConfirmDialog);
            pass.OkCommand.Execute("OK");

        }

        private void handleCancelCommand(object param)
        {
            var pass = (param as ConfirmDialog);
            pass.OkCommand.Execute(null);
        }
    }
}
