using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core
{
    public class HistoryLogin
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string LoginTime { get; set; }
        public bool IsSelect { get; set; } = false;
    }
}
