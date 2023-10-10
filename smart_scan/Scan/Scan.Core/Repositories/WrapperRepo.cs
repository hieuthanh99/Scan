using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Repositories
{
    public class WrapperRepo
    {
        private static WrapperRepo _wrapperRepo { get; set; }
        public static WrapperRepo Instance()
        {
            if (_wrapperRepo == null)
            {
                _wrapperRepo = new WrapperRepo();
            }
            return _wrapperRepo;
        }

        private UserAuthenticateRepo _userAuthenticateRepo;
        public UserAuthenticateRepo UserAuthenticateRepo
        {
            get
            {
                if (_userAuthenticateRepo == null)
                {
                    _userAuthenticateRepo = new UserAuthenticateRepo();
                }

                return _userAuthenticateRepo;
            }
        }

        private SystemRepo _system;
        public SystemRepo System
        {
            get
            {
                if (_system == null)
                {
                    _system = new SystemRepo();
                }

                return _system;
            }
        }

    }
}
