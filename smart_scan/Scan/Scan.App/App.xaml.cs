using log4net;
using Scan.Core.Services;
using Scan.Core.Settings;
using Scan.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace Scan.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(App));

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (AlreadyRunning())
                App.Current.Shutdown(); // Just shutdown the current application,if any instance found.  

            base.OnStartup(e);
            ApplicationSetup();
            //// await MonitorServerStatus();   

            // Update database if need before start
           // UpdateDatabaseIfNeed();

            if (e.Args.Length > 1)
            {
                // get dictionary arguments
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                for (int i = 0; i < e.Args.Length; i += 2)
                {
                    string argKey = e.Args[i].Replace("--", "");
                    if (i + 1 < e.Args.Length)
                    {
                        arguments.Add(argKey, e.Args[i + 1]);
                    }
                }

                if (arguments.ContainsKey("action") && arguments.ContainsKey("version"))
                {
                    var action = arguments["action"];
                    var version = arguments["version"];
                    // first run after update version
                    if ("UpdateVersion".Equals(action) && !string.IsNullOrEmpty(version))
                    {
                        UpdateAppVersion(version);
                    }
                }
            }

            var systemService = new SystemServices();
            var currentVersion = systemService.GetCurrentVersion();
            var localVersion = string.Empty;
            if (currentVersion != null)
            {
                localVersion = currentVersion.VersionName;
            }

            IoC.Get<ApplicationViewModel>().LocalVersion = localVersion;

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();

            //check version
            await CheckVersion();

            //// bắt đầu chạy job lúc 21h
            //TimeSpan startScheduleTime = new TimeSpan(21, 0, 0);
            //System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            //timer.Interval = TimeSpan.FromMinutes(1);
            //timer.Tick += (s, evt) =>
            //{
            //    if (IoC.Get<ApplicationViewModel>().AutoUploadScheduleStatus == "PENDING")
            //    {
            //        // thực hiện refresh token
            //        refreshToken();

            //        // kiểm tra đến thời gian chạy job => thực hiện chạy
            //        if (DateTime.Now.TimeOfDay >= startScheduleTime)
            //        {
            //            IoC.Get<ApplicationViewModel>().AutoUploadScheduleStatus = "PROCESSING";

            //            BackgroundWorker runBatchWorker = new BackgroundWorker();
            //            runBatchWorker.DoWork += RunBatchWorker_DoWork;
            //            runBatchWorker.RunWorkerCompleted += RunBatchWorker_RunWorkerCompleted;

            //            runBatchWorker.RunWorkerAsync();
            //        }
            //    }
            //}; ;
            //timer.Start();
        }
        private void UpdateAppVersion(string version)
        {
            var systemService = new SystemServices();
            try
            {
                var serverVersion = "";
                Task.Run(async () =>
                {
                    serverVersion = await systemService.GetServerVersion();
                }).Wait();

                if (!version.Equals(serverVersion))
                {
                    return;
                }

                var currentVersion = systemService.GetCurrentVersion();
                string dBUpLastestTime = null;
                if (currentVersion != null && !string.IsNullOrEmpty(currentVersion.DBUpLastestTime))
                {
                    dBUpLastestTime = currentVersion.DBUpLastestTime;
                }

                // Insert New Version Record To Sqlite
                systemService.InsertNewVersion(version, DateTime.Now.ToString("yyyyMMddHHmmss"), 1, dBUpLastestTime);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        private IEnumerable<string> ReadLines(Func<Stream> streamProvider, Encoding encoding)
        {
            using (var stream = streamProvider())
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private async Task CheckVersion()
        {
            var localVersion = IoC.Get<ApplicationViewModel>().LocalVersion;

            var systemService = new SystemServices();
            var serverVersion = await systemService.GetServerVersion();

            IoC.Get<ApplicationViewModel>().ServerVersion = serverVersion;

            if (localVersion != null && localVersion.Trim().Length > 0 && localVersion.Contains(".")
                && serverVersion != null && serverVersion.Trim().Length > 0 && serverVersion.Contains("."))
            {
                string[] arrLocalVersion = localVersion.Split('.');
                string[] arrServerVersion = serverVersion.Split('.');
                if (arrLocalVersion.Length == 3 && arrServerVersion.Length == 3)
                {
                    int majorLocal = int.Parse(arrLocalVersion[0]);
                    int minorLocal = int.Parse(arrLocalVersion[1]);
                    int patchLocal = int.Parse(arrLocalVersion[2]);

                    int majorServer = int.Parse(arrServerVersion[0]);
                    int minorServer = int.Parse(arrServerVersion[1]);
                    int patchServer = int.Parse(arrServerVersion[2]);

                    if (majorServer > majorLocal)
                    {
                        IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;
                    }
                    else if (majorServer == majorLocal)
                    {
                        if (minorServer > minorLocal)
                        {
                            IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;
                        }
                        else if (minorServer == minorLocal && patchServer > patchLocal)
                        {
                            IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;
                        }
                    }
                }
            }
            else
            {
                IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = false;
            }

            if (IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate)
            {
                var confirmDiaLog = IoC.Get<IDialogConfirmUI>().ShowDialog(new ConfirmDialogViewModel
                {
                    Title = "Thông báo cập nhật ứng dụng",
                    OkText = "Cập nhật",
                    CloseDialogVisibility = false,
                    CancelButtonVisibility = false,
                    ContentText = $"Đã có bản cập nhật mới {IoC.Get<ApplicationViewModel>().ServerVersion}, bạn cần cập nhật để tiếp tục sử dụng ứng dụng."
                                + Environment.NewLine + "Quá trình cập nhật vui lòng chờ đợi, không tắt ứng dụng."
                                + Environment.NewLine + "Nếu có lỗi trong quá trình cập nhật, vui lòng tạo yêu cầu hỗ trợ trên trang công nghệ thông tin."
                }).Result;

                if (confirmDiaLog != null && confirmDiaLog.GetType() == typeof(string) && string.Equals((string)confirmDiaLog, "OK"))
                {
                    try
                    {
                        //DialogUpdate dialogUpdate = new DialogUpdate
                        //{
                        //    Owner = System.Windows.Application.Current.MainWindow,
                        //};

                        //IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = false;
                        //IoC.Get<ApplicationViewModel>().IsProcessing = true;

                        //var dialogUpdateResult = dialogUpdate.ShowDialog();

                        //var isSuccessProcess = dialogUpdateResult != null && (bool)dialogUpdateResult;
                        //if (isSuccessProcess)
                        //{
                        //    IoC.Get<ApplicationViewModel>().IsProcessing = false;
                        //    //// 4. Run Update App
                        //    Process.Start("UpdateVersion.exe");
                        //}
                        //else
                        //{
                        //    IoC.Get<ApplicationViewModel>().IsProcessing = false;
                        //    IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;

                        //    IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Có lỗi trong quá trình nâng cấp phiên bản, vui lòng thử lại sau!");
                        //}
                    }
                    catch (Exception ex)
                    {
                        IoC.Get<ApplicationViewModel>().IsProcessing = false;
                        IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = true;

                        IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, "Có lỗi trong quá trình nâng cấp phiên bản, vui lòng thử lại sau!");
                        logger.Error(ex);
                    }
                }
            }
        }

        private void ApplicationSetup()
        {
            IoC.Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
           // IoC.Kernel.Bind<IDialogViewImageUI>().ToConstant(new ViewImageDialog());
            ////IoC.Kernel.Bind<IDialogPinUserUi>().ToConstant(new PinUserDialog());
            //IoC.Kernel.Bind<IDialogImageCropperUI>().ToConstant(new ImageCropperDialog());
            //IoC.Kernel.Bind<IDialogKhdnSignatureCropperUI>().ToConstant(new KhdnSignatureCropperDialog());
            //IoC.Kernel.Bind<IDialogCctDetailUI>().ToConstant(new CctDetailDialog());
            //IoC.Kernel.Bind<IDialogUserSettingsUI>().ToConstant(new UserSettingsDialog());
            //IoC.Kernel.Bind<IDialogConfirmUI>().ToConstant(new IocConfirmDialog());
            //IoC.Kernel.Bind<IDialogScrollableConfirmUI>().ToConstant(new IocScrollableConfirmDialog());
            //IoC.Kernel.Bind<IDialogLoginUI>().ToConstant(new IocLoginDialog());
            //IoC.Kernel.Bind<IDialogBatchUploadUI>().ToConstant(new DialogBatchUpload());
            //IoC.Kernel.Bind<IDialogValidateBlankPageUI>().ToConstant(new IocValidateBlankPageDialog());
        }

        private bool AlreadyRunning()
        {
            bool running = false;
            try
            {
                // Getting collection of process  
                Process currentProcess = Process.GetCurrentProcess();
                // Check with other process already running   
                var otherProcess = Process.GetProcessesByName(currentProcess.ProcessName).FirstOrDefault(p => p.Id != currentProcess.Id);

                if (otherProcess != null)
                {
                    IntPtr mwHandle = otherProcess.MainWindowHandle;
                    if (mwHandle != IntPtr.Zero)
                    {
                        running = true;
                        // If application is in ICONIC mode then restore
                        if (IsIconic(mwHandle))
                        {
                            ShowWindow(mwHandle, SW_RESTORE);
                        }
                        // Activate the window, if process is already running 
                        SetForegroundWindow(mwHandle);
                    }
                    else
                    {
                        otherProcess.Kill(); // kill if background process
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return running;
        }

        [DllImport("user32")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32")]
        static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        const int SW_RESTORE = 9;

        [DllImport("user32")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                // delete login authen
                var authenticateServices = new UserAuthenticateServices();
                //authenticateServices.DeleteUserAuthenticate();
                // delete temp folder scan no login
                TempData.Instance.CleanupScanNoLoginFolder();
                // clear all empty folder
                try
                {
                    DateTime currentTime = DateTime.Now;
                    string currentYear = currentTime.ToString("yyyy");
                    string rootFolder = Path.Combine(Environment.CurrentDirectory, currentYear);
                    TempData.Instance.DeleteEmptyDirectory(rootFolder);
                }
                catch (Exception) { }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        ////private async Task MonitorServerStatus()
        ////{
        ////    DispatcherTimer timer = new DispatcherTimer();
        ////    timer.Interval = TimeSpan.FromMilliseconds(20000);

        ////    SystemServices systemService = new SystemServices();
        ////    var status = await systemService.CheckHelpthy();
        ////    IoC.Get<ApplicationViewModel>().serverReachable = status;

        ////    timer.Tick += async (s, evt) =>
        ////    {

        ////        var result = await systemService.CheckHelpthy();
        ////        IoC.Get<ApplicationViewModel>().serverReachable = result;

        ////    };
        ////    timer.Start();
        ////}
        ///}
        ///
        
    }
}
