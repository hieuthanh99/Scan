using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public interface IDialogConfirmUI
    {
        Task<object> ShowDialog(ConfirmDialogViewModel viewModel);
    }
}
