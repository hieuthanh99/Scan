using log4net;
using Scan.Core;
using Scan.Core.Settings;
using Scan.Core.ViewModel.Base;
using Scan.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Scan.App.Helpers;
using Scan.Core.Services;
using Scan.Core.DTO;

namespace Scan.App
{
    public class LoginViewModel : BaseViewModel
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoginViewModel));
        public ICommand LoginCommand { get; set; }
        private readonly WrapperServices services;

        public LoginViewModel()
        {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
            services = services ?? new WrapperServices();
        }
  
        public async Task Login(object parameter)
        {
            if (IoC.Get<ApplicationViewModel>().IsProcessing)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Hệ thống đang trong quá trình xử lý, vui lòng chờ đợi!");
                return;
            }

            var loginPage = parameter as IFormLogin;
            var userName = loginPage.UserName;
            var password = loginPage.SecurePassword.Unsecure();

            if (userName.Length == 0 || password.Length == 0)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Vui lòng nhập vào Tên đăng nhập và Mật khẩu!");
                IoC.Get<ApplicationViewModel>().isLoading = false;
                return;
            }
            await LoginUser(userName, password);
        }

        private async Task LoginUser(string userName, string password)
        {
            IoC.Get<ApplicationViewModel>().isLoading = true;
            var loginInfo = await services.User.LoginSSO(userName, password);
            if (loginInfo == null)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Tài khoản hoặc mật khẩu không chính xác, vui lòng nhập lại!");
                IoC.Get<ApplicationViewModel>().isLoading = false;
                return;
            }

            //// Add User To Local
            // services.User.AddNewUser(userParam);

            //var checkExists = services.User.CheckExistUserProfile(userName);
            //if (!checkExists)
            //{
            //    IoC.Get<ApplicationViewModel>().isLoading = true;
            //    UserInfo userParam = new UserInfo();
            //    userParam.UserName = userName;
            //    userParam.FullName = loginInfo.FullName;
            //    ////userParam.Pin = resultDiaLog.ToString();
            //    userParam.Pin = "1234";
            //    userParam.Address = loginInfo.Address;
            //    userParam.Email = loginInfo.Email;
            //    userParam.Phone = loginInfo.Phone;
            //    userParam.Roles = loginInfo.Roles;
            //    userParam.Groups = loginInfo.Groups;

            //    //// Add User To Local
            //    services.User.AddNewUser(userParam);
            //}
            //else
            //{
            //    services.User.UpdateRoleAndGroup(loginInfo);
            //}
            if (CheckGroupAndRole(loginInfo))
            {
                //// Add History
                //services.User.AddLoginHistory(userName);

                IoC.Get<ApplicationViewModel>().UserGroupSelected = loginInfo.Groups[0];
                IoC.Get<ApplicationViewModel>().CurrentUser = loginInfo;
                //services.UserAuthenticateServices.InsertToken(loginInfo);

                //xoa tep ngoai cau hinh
                //  deleteBackUpFile(userName);
                // đồng bộ danh sách nghiệp vụ scan
                //   await services.ScanBusinessService.GetScanBusinessesAsync();

                IoC.Get<ApplicationViewModel>().isLoading = false;
                IoC.Get<ApplicationViewModel>().GotoPage(ApplicationPage.Home);
            }
            else
            {
                IoC.Get<ApplicationViewModel>().isLoading = false;
            }
        }

        private bool CheckGroupAndRole(UserInfo userInfo)
        {
            if (userInfo.Groups == null || userInfo.Groups.Count == 0)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Không tìm thấy chi nhánh của bạn, vui lòng liên hệ quản trị!");
                return false;
            }

            if (userInfo.Roles == null || userInfo.Roles.Count == 0)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Vui lòng kiểm tra lại quyền truy cập của bạn!");
                return false;
            }

            return true;
        }
    }
}
