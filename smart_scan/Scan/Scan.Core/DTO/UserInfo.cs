using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scan.Core
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Pin { get; set; }
        public string LoginTime { get; set; }
        public int ExpAccessToken { get; set; }
        public int ExpRefreshToken { get; set; }
        public List<Role> Roles { get; set; }
        public List<Group> Groups { get; set; }
    }

    public class Role
    {
        public string RoleName { get; set; }
    }

    public class Group
    {
        public string GroupName { get; set; }
    }
}
