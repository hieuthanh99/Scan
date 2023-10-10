using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class WrapperServices
    {
        private static WrapperServices _wrapperServices { get; set; }
        public static WrapperServices Instance()
        {
            if (_wrapperServices == null)
            {
                _wrapperServices = new WrapperServices();
            }
            return _wrapperServices;
        }

        //Declare variables
        private UserServices _userService;
        private ScanBusinessService _scanBusinessService;

        public UserServices User
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserServices();
                }

                return _userService;
            }
        }

        public ScanBusinessService ScanBusinessService
        {
            get
            {
                if (_scanBusinessService == null)
                {
                    _scanBusinessService = new ScanBusinessService();
                }

                return _scanBusinessService;
            }
        }

        private UserStateServices _userStateServices;
        public UserStateServices UserStateServices
        {
            get
            {
                if (_userStateServices == null)
                {
                    _userStateServices = new UserStateServices();
                }

                return _userStateServices;
            }
        }
        //public UserAuthenticateServices UserAuthenticateServices
        //{
        //    get
        //    {
        //        if (_userAuthenticateServices == null)
        //        {
        //            _userAuthenticateServices = new UserAuthenticateServices();
        //        }

        //        return _userAuthenticateServices;
        //    }
        //}
    }
}
