using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Entities
{
    public class UserAuthenticate
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpAccessToken { get; set; }
        public int ExpRefreshToken { get; set; }
        public string CreatedTime { get; set; }
    }
}
