using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.ViewModel.Base
{
    public interface IFormLogin
    {
        string UserName { get; }
        SecureString SecurePassword { get; }
    }
}
