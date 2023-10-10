using Microsoft.TeamFoundation.TestManagement.WebApi;
using Scan.Core;
using Scan.Core.DTO;
using Scan.Core.Settings;
using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Scan.App
{
    public class HomeViewModel : BaseViewModel
    {
        #region ICommand 
        public ICommand onHideSideMenu { get; set; }
        #endregion

        public HomeViewModel()
        {
            #region InitCommand
            onHideSideMenu = new RelayParameterizedCommand((parameter) => HandleHideSideMenu());
            #endregion
        }

    
        /*
        Handle Click hide/show side bar
        */
        private void HandleHideSideMenu()
        {
            var currentStatus = IoC.Get<ApplicationViewModel>().SideMenuVisible;
            IoC.Get<ApplicationViewModel>().SideMenuVisible = !currentStatus;
        }
    }
}
