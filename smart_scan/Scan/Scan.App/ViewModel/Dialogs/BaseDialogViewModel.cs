using log4net;
using Scan.Core.Services;
using Scan.Core.Settings;
using Scan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public class BaseDialogViewModel : BaseViewModel
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseDialogViewModel));

        private const float WARNING_NUMBER_GB = 2f;
        private const long WARNING_FREE_SPACE = (long)(WARNING_NUMBER_GB /*GB*/ * 1024 /*MB*/ * 1024 /*KB*/ * 1024);

        private const float STOP_NUMBER_GB = 1f;
        private const long STOP_FREE_SPACE = (long)(STOP_NUMBER_GB /*GB*/ * 1024 /*MB*/ * 1024 /*KB*/ * 1024);

        public string Title { get; set; }
        public bool CloseDialogVisibility { get; set; } = true;

        public async Task<bool> checkToken()
        {
            int curTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            //check exp Access Token
            await ApplicationViewModel._lock.WaitAsync();
            try
            {
                //no exp access token
                if (IoC.Get<ApplicationViewModel>().CurrentUser.ExpAccessToken >= curTimestamp)
                {
                    int remainTime = (IoC.Get<ApplicationViewModel>().CurrentUser.ExpAccessToken - curTimestamp) / 60;
                    if (remainTime < 1)
                    {
                        WrapperServices services = new WrapperServices();
                       // await services.User.refreshToken();
                    }
                }
                else
                {
                    //exp access token -> check exp refresh token
                    if (IoC.Get<ApplicationViewModel>().CurrentUser.ExpRefreshToken >= curTimestamp)
                    {
                       // WrapperServices services = new WrapperServices();
                       // await services.User.refreshToken();
                    }
                    //else
                    //{
                    //    // check version trước khi hiển thị màn hình login
                    //    await CheckVersion();

                    //    if (IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate)
                    //    {
                    //        var confirmUpdateVersionDiaLog = IoC.Get<IDialogConfirmUI>().ShowDialog(new ConfirmDialogViewModel
                    //        {
                    //            Title = "Thông báo cập nhật ứng dụng",
                    //            OkText = "Cập nhật",
                    //            CloseDialogVisibility = false,
                    //            CancelButtonVisibility = false,
                    //            ContentText = $"Đã có bản cập nhật mới {IoC.Get<ApplicationViewModel>().ServerVersion}, bạn cần cập nhật để tiếp tục sử dụng ứng dụng."
                    //                        + Environment.NewLine + "Quá trình cập nhật vui lòng chờ đợi, không tắt ứng dụng."
                    //                        + Environment.NewLine + "Nếu có lỗi trong quá trình cập nhật, vui lòng tạo yêu cầu hỗ trợ trên trang công nghệ thông tin."
                    //        }).Result;

                    //        if (confirmUpdateVersionDiaLog != null && confirmUpdateVersionDiaLog.GetType() == typeof(string) && string.Equals((string)confirmUpdateVersionDiaLog, "OK"))
                    //        {
                    //            try
                    //            {
                    //                DialogUpdate dialogUpdate = new DialogUpdate
                    //                {
                    //                    Owner = System.Windows.Application.Current.MainWindow,
                    //                };

                    //                IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = false;
                    //                IoC.Get<ApplicationViewModel>().IsProcessing = true;

                    //                var dialogUpdateResult = dialogUpdate.ShowDialog();

                    //                var isSuccessProcess = dialogUpdateResult != null && (bool)dialogUpdateResult;
                    //                if (isSuccessProcess)
                    //                {
                    //                    IoC.Get<ApplicationViewModel>().IsProcessing = false;
                    //                    //// 4. Run Update App
                    //                    System.Diagnostics.Process.Start("UpdateVersion.exe");
                    //                }
                    //                else
                    //                {
                    //                    IoC.Get<ApplicationViewModel>().IsProcessing = false;
                    //                    IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;

                    //                    IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Có lỗi trong quá trình nâng cấp phiên bản, vui lòng thử lại sau!");
                    //                }
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                IoC.Get<ApplicationViewModel>().IsProcessing = false;
                    //                IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;

                    //                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Có lỗi trong quá trình nâng cấp phiên bản, vui lòng thử lại sau!");
                    //                logger.Error(ex);
                    //            }
                    //        }
                    //    }

                    //    var confirmDiaLog = IoC.Get<IDialogConfirmUI>().ShowDialog(new ConfirmDialogViewModel
                    //    {
                    //        Title = "Đăng nhập",
                    //        OkText = "Đồng ý",
                    //        CancelText = "Hủy",
                    //        ContentText = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại để làm mới phiên của bạn!"
                    //    }).Result;

                    //    if (confirmDiaLog != null && confirmDiaLog.GetType() == typeof(string))
                    //    {
                    //        var loginDialog = IoC.Get<IDialogLoginUI>().ShowDialog(new LoginDialogViewModel
                    //        {
                    //            Title = "Đăng nhập"
                    //        }).Result;

                    //        if (loginDialog != null && loginDialog.GetType() == typeof(string))
                    //        {
                    //            return (string)loginDialog == "OK";
                    //        }
                    //        else
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return false;
                    //    }
                    //}
                }
            }
            finally
            {
                ApplicationViewModel._lock.Release();
            }

            return true;
        }

        public async Task<bool> checkAvailableFreeSpace()
        {
            await ApplicationViewModel._lock.WaitAsync();
            try
            {
                var availableFreeSpace = Core.Helpers.SystemHelper.AvailableFreeSpace();
                //// Có 2 ngưỡng: WARNING và STOP
                if (availableFreeSpace.Item2 < STOP_FREE_SPACE)
                {
                    _ = IoC.Get<IDialogConfirmUI>().ShowDialog(new ConfirmDialogViewModel
                    {
                        Title = "Thông báo",
                        OkText = "Đóng",
                        CancelButtonVisibility = false,
                        ContentText = $"Dung lượng còn lại của ổ {availableFreeSpace.Item1.Replace(":\\", "")} nhỏ hơn {STOP_NUMBER_GB} GB." + Environment.NewLine + " Bạn cần giải phóng bớt dung lượng trước khi tiếp tục sử dụng phần mềm!"
                    }).Result;

                    return false;
                }
                else
                {
                    if (availableFreeSpace.Item2 < WARNING_FREE_SPACE)
                    {
                        var confirmDiaLog = IoC.Get<IDialogConfirmUI>().ShowDialog(new ConfirmDialogViewModel
                        {
                            Title = "Cảnh báo",
                            OkText = "Đồng ý",
                            CancelText = "Tiếp tục",
                            ContentText = $"Dung lượng còn lại của ổ {availableFreeSpace.Item1.Replace(":\\", "")} nhỏ hơn {WARNING_NUMBER_GB} GB." + Environment.NewLine + " Đề nghị giải phóng bớt dung lượng trước khi tiếp tục sử dụng phần mềm!"
                        }).Result;

                        return !(confirmDiaLog != null && confirmDiaLog.GetType() == typeof(string) && string.Equals((string)confirmDiaLog, "OK")); // cancel trả về true
                    }
                }
            }
            finally
            {
                ApplicationViewModel._lock.Release();
            }

            return true;
        }

        ////if (IoC.Get<ApplicationViewModel>().CurrentUser.ExpAccessToken < curTimestamp)
        ////{
        ////    var confirmDiaLog = IoC.Get<IDialogConfirmUI>().ShowDialog(new ConfirmDialogViewModel
        ////    {
        ////        Title = "Đăng nhập",
        ////        OkText = "Đồng ý",
        ////        CancelText = "Hủy",
        ////        ContentText = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại để làm mới phiên của bạn!"
        ////    }).Result;
        ////    if (confirmDiaLog != null && confirmDiaLog.GetType() == "".GetType())
        ////    {
        ////        var loginDiaLog = IoC.Get<IDialogLoginUI>().ShowDialog(new LoginDialogViewModel
        ////        {
        ////            Title = "Đăng nhập"
        ////        }).Result;
        ////    }
        ////}

        //private async Task CheckVersion()
        //{
        //    try
        //    {
        //        var systemService = new SystemServices();
        //        var currentVersion = systemService.GetCurrentVersion();

        //        var localVersion = string.Empty;
        //        if (currentVersion != null)
        //        {
        //            localVersion = currentVersion.VersionName;
        //        }

        //        var serverVersion = await systemService.GetServerVersion();

        //        IoC.Get<ApplicationViewModel>().LocalVersion = localVersion;
        //        IoC.Get<ApplicationViewModel>().ServerVersion = serverVersion;

        //        if (localVersion != null && localVersion.Trim().Length > 0 && localVersion.Contains(".")
        //            && serverVersion != null && serverVersion.Trim().Length > 0 && serverVersion.Contains("."))
        //        {
        //            string[] arrLocalVersion = localVersion.Split('.');
        //            string[] arrServerVersion = serverVersion.Split('.');
        //            if (arrLocalVersion.Length == 3 && arrServerVersion.Length == 3)
        //            {
        //                int majorLocal = int.Parse(arrLocalVersion[0]);
        //                int minorLocal = int.Parse(arrLocalVersion[1]);
        //                int patchLocal = int.Parse(arrLocalVersion[2]);

        //                int majorServer = int.Parse(arrServerVersion[0]);
        //                int minorServer = int.Parse(arrServerVersion[1]);
        //                int patchServer = int.Parse(arrServerVersion[2]);

        //                if (majorServer > majorLocal)
        //                {
        //                    IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;
        //                }
        //                else if (majorServer == majorLocal)
        //                {
        //                    if (minorServer > minorLocal)
        //                    {
        //                        IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;
        //                    }
        //                    else if (minorServer == minorLocal && patchServer > patchLocal)
        //                    {
        //                        IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = false;
        //        }

        //    }
        //    catch (Exception) { }
        //}
    }
}
