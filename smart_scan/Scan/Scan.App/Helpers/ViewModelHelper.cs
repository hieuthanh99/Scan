using Scan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App.Helpers
{
    public static class ViewModelHelper
    {
        public static void HandleHideSideMenu()
        {
            var currentStatus = IoC.Get<ApplicationViewModel>().SideMenuVisible;
            IoC.Get<ApplicationViewModel>().SideMenuVisible = !currentStatus;
        }
    }
}
