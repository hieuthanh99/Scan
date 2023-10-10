using Scan.Core;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public static class RedirectHelper
    {
        public static void RedirectLoading(ApplicationPage page)
        {
            IoC.Get<ApplicationViewModel>().isLoading = true;
            IoC.Get<ApplicationViewModel>().GotoPage(page);
            IoC.Get<ApplicationViewModel>().isLoading = false;
        }
    }
}