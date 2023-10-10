using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Scan.App
{
    public class DialogWindowViewModel : WindowViewModel
    {
        #region Public Properties
        public string Title { get; set; }

        public bool CloseDialogVisibility { get; set; } = true;

        //public Control Content { get; set; }

        #endregion

        #region Constructor
        public DialogWindowViewModel(Window window) : base(window)
        {
            WindowMinimumWidth = 250;
            WindowMinimumHeight = 100;

            TitleHeight = 30;
        }

        #endregion
    }
}
