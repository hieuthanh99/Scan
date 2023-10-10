using log4net;
using Microsoft.Win32;
using Newtonsoft.Json;
using Scan.Core;
using Scan.Core.Services;
using Scan.Core.Settings;
using Scan.Core.ViewModel.Base;
using Scan.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using Scan.Core.Entities;
using Clock.ImageProcessing;

namespace Scan.App
{
    public class ScanNoLoginDynamsoftViewModel : BaseViewModel
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ScanNoLoginDynamsoftViewModel));

        private readonly WrapperServices services;
        public ConcurrentObservableCollection<CustomerImage> ListImagesScaned { get; set; } = new ConcurrentObservableCollection<CustomerImage>();

        public UserState userState { get; set; }

        private bool _isSavePdfSearchable { get; set; } = false;
        public bool IsSavePdfSearchable
        {
            get { return _isSavePdfSearchable; }
            set
            {
                _isSavePdfSearchable = value;
                OnPropertyChanged("IsSavePdfSearchable");
            }
        }

        //danh sách duong dan luu tru
        public ObservableCollection<string> ListInitSavePath { get; set; }
        public ObservableCollection<string> ListSavePath { get; set; }
        private string _selectedSavePath { get; set; }
        public string SelectedSavePath
        {
            get { return _selectedSavePath; }
            set
            {
                _selectedSavePath = value;
            }
        }

        // danh sach loai in (1 mat hay 2 mat)
        public ObservableCollection<ScanCapability> ListScanCapabilities { get; set; }
        private ScanCapability _selectedScanCapability { get; set; }
        public ScanCapability SelectedScanCapability
        {
            get { return _selectedScanCapability; }
            set
            {
                _selectedScanCapability = value;
            }
        }

        ////public bool IsPdfSeachable { get; set; } = false;
        public bool IsScan { get; set; } = false;
        public bool IsEnableScanButton { get; set; } = true;
        public bool IsEnableSave { get; set; } = false;
        public Visibility VisibleHeader { get; set; } = Visibility.Visible;
        public bool HiddenRowHeader { get; set; } = false;

        public ICommand OnChooseFile { get; set; }
        public ICommand OnScanFile { get; set; }
        public ICommand OnViewImage { get; set; }
        public ICommand OnRemoveImage { get; set; }
        public ICommand OnRefresh { get; set; }
        public ICommand OnBackPageClick { get; set; }
        public ICommand OnChooseFolder { get; set; }
        public ICommand OnSaveData { get; set; }

        private string _pathFolder { get; set; } = Path.Combine(TempData.Instance.CurrentDirectory);
        public string PathFolder
        {
            get { return _pathFolder; }
            set
            {
                _pathFolder = value;
                OnPropertyChanged("PathFolder");
            }
        }

        private void SetStateUser(string key, object value)
        {
            var dataState = new Dictionary<string, object>();
            if (userState != null)
            {
                dataState = JsonConvert.DeserializeObject<Dictionary<string, object>>(userState.StateData);
            }
            dataState[key] = value;
            var jData = JsonConvert.SerializeObject(dataState);
          //  services.UserStateServices.SetStateUser("local", jData);
        }

        ////private string _fileName { get; set; } = Path.GetRandomFileName();
        private string _fileName { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        // folder tam luu anh scan + image pdf
        public string outputFolder { get; set; }

        public ScanNoLoginDynamsoftViewModel()
        {

            if (IoC.Get<ApplicationViewModel>().CurrentUser != null)
            {
                VisibleHeader = Visibility.Hidden;
                HiddenRowHeader = true;
                OnPropertyChanged("VisibleHeader");
            }

            services = WrapperServices.Instance();

           // userState = services.UserStateServices.GetSettingByUser("local");

            OnChooseFile = new RelayParameterizedCommand(async (parameter) => await HandleChooseFile());
            OnScanFile = new RelayParameterizedCommand(async (parameter) => await HandleScanFile());
            OnViewImage = new RelayParameterizedCommand(parameter => HandleViewImage(parameter));
          //  OnRemoveImage = new RelayParameterizedCommand(parameter => HandleRemoveImage(parameter));
            OnRefresh = new RelayParameterizedCommand(parameter => HandleReFresh());
            OnBackPageClick = new RelayParameterizedCommand((parameter) => HandleBackPageClick());
            OnChooseFolder = new RelayParameterizedCommand(parameter => HandleChooseFolder());
           // OnSaveData = new RelayParameterizedCommand(async (parameter) => await HandleSaveData());

            ListScanCapabilities = LoadScanCapabilities();
            SelectedScanCapability = ListScanCapabilities != null && ListScanCapabilities.Count > 0 ? ListScanCapabilities[0] : null;

            ListSavePath = LoadSavePath();
            if (ListSavePath == null || ListSavePath.Count == 0)
            {
                ListSavePath.Add(PathFolder);
            }
            SelectedSavePath = ListSavePath != null && ListSavePath.Count > 0 ? ListSavePath[0] : null;

            IoC.Get<ApplicationViewModel>().IsProcessing = false;
        }

        private Dictionary<string, string> GetStateUser(string key)
        {
            try
            {
                if (userState != null && key != null)
                {
                    var dataState = JsonConvert.DeserializeObject<Dictionary<string, object>>(userState.StateData);
                    if (dataState.ContainsKey(key) && dataState[key] != null)
                    {
                        var value = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataState[key].ToString());
                        return value;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return new Dictionary<string, string>();
        }

        // load Save Path
        private ObservableCollection<string> LoadSavePath()
        {
            ObservableCollection<string> lstSavePath = new ObservableCollection<string>();
            ListInitSavePath = new ObservableCollection<string>();
           
            return lstSavePath;
        }

        // load Capabilities may in
        private ObservableCollection<ScanCapability> LoadScanCapabilities()
        {
            ObservableCollection<ScanCapability> lstScanCapabilities = new ObservableCollection<ScanCapability>() {
                new ScanCapability
                {
                    Code = ScanCapa.FEED.ToString(),
                    Name = "Scan một mặt"
                },
                new ScanCapability
                {
                    Code = ScanCapa.DUP.ToString(),
                    Name = "Scan hai mặt"
                }
            };


            return lstScanCapabilities;
        }

        // scan
        private async Task HandleScanFile()
        {
            // để ngoài try/catch để không chạy code trong block finally
            if (IoC.Get<ApplicationViewModel>().IsProcessing)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Hệ thống đang trong quá trình xử lý, vui lòng chờ đợi!");
                return;
            }

            try
            {
                BaseDialogViewModel baseDialogViewModel = new BaseDialogViewModel();
                var checkFreeSpace = await baseDialogViewModel.checkAvailableFreeSpace();
                if (!checkFreeSpace)
                {
                    return;
                }

                IsScan = true;
               

                IoC.Get<ApplicationViewModel>().IsProcessing = false;

                if (ListImagesScaned != null)
                {
                    IsEnableSave = true;
                }
                else
                {
                    IsEnableSave = false;
                }
                ////IsEnableSave = ListImagesScaned != null && ListImagesScaned.Count > 0 ? true : false;
                ////AppLog.Log.Info("ListImagesScaned]" + ListImagesScaned.Count);
                ////AppLog.Log.Info("IsEnableSave 1]" + IsEnableSave);
                OnPropertyChanged("IsEnableSave");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
            }

        }


#pragma warning disable S3776
        private async Task HandleChooseFile()
        {
            IsScan = false;
            if (IoC.Get<ApplicationViewModel>().IsProcessing)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Hệ thống đang trong quá trình xử lý, vui lòng chờ đợi!");
                return;
            }

            BaseDialogViewModel baseDialogViewModel = new BaseDialogViewModel();
            var checkFreeSpace = await baseDialogViewModel.checkAvailableFreeSpace();
            if (!checkFreeSpace)
            {
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Choose files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.pdf) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.pdf";
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == true)
            {
                IoC.Get<ApplicationViewModel>().isLoading = true;
                IoC.Get<ApplicationViewModel>().IsProcessing = true;

                ////await HandleChooseFileThread1(dialog);
                ////var fileNames = dialog.FileNames.ToArray();
                int pageIndex = ListImagesScaned.Count + 1;
                //// string outputFolder = TempData.Instance.CreateBatchFolder(AppConfig.ImportFolderName);

                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    if (Path.GetExtension(dialog.FileNames[i].ToUpper()) == ".PDF" && !PdfiumViewers.IsPdf(dialog.FileNames[i]))
                    {
                        IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Error, $"File {dialog.FileNames[i]} lỗi hoặc không phải là định dạng pdf!");
                        IoC.Get<ApplicationViewModel>().IsProcessing = false;
                        return;
                    }
                }

                await Task.Run(() =>
                {
                    // create folder
                    string outputImportFolder = TempData.Instance.CreateScanNoLoginTempFolder(AppConfig.ImportFolderName);

                    for (int i = 0; i < dialog.FileNames.Length; i++)
                    {
                        if (Path.GetExtension(dialog.FileNames[i].ToUpper()) != ".PDF")
                        {
                            long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                            string destFile = outputImportFolder + "\\" + Path.GetFileNameWithoutExtension(dialog.FileNames[i]) + "_" + time.ToString() + Path.GetExtension(dialog.FileNames[i]);
                            File.Copy(dialog.FileNames[i], destFile, true);
                            string thumbPath = ImageProcessor.CreateThumbnailImage(destFile);
                            ListImagesScaned.Add(new CustomerImage
                            {
                                FileName = destFile,
                                ThumbnailPath = thumbPath,
                                PageIndex = pageIndex,
                                HasProcessed = false
                            });
                            pageIndex++;
                        }
                        else
                        {
                            int resolution = 200;
                            PdfiumViewers.ConvertPDFToBitmap(dialog.FileNames[i], outputImportFolder, (string outputPath, int iPage, DateTime timeProcess) =>
                            {
                                string thumbPath = ImageProcessor.CreateThumbnailImage(outputPath);
                                ListImagesScaned.Add(new CustomerImage
                                {
                                    FileName = outputPath,
                                    ThumbnailPath = thumbPath,
                                    HasProcessed = false,
                                });
                                return null;
                            }, resolution);
                        }
                    }

                  //  IoC.Get<ApplicationViewModel>().countImgScan = ListImagesScaned.Count;
                });

                if (ListImagesScaned != null && ListImagesScaned.Count > 0)
                {
                    IsEnableSave = true;
                }
                else
                {
                    IsEnableSave = false;
                }
                OnPropertyChanged("IsEnableSave");
                IoC.Get<ApplicationViewModel>().IsProcessing = false;
            }

        }
#pragma warning restore S3776

        private void HandleViewImage(Object parameter)
        {
            try
            {
                IoC.Get<ApplicationViewModel>().isLoading = true;
                var customerImage = (CustomerImage)parameter;
                //IoC.Get<IDialogViewImageUI>().ShowDialog(new ViewImageDialogViewModel
                //{
                //    Title = "Chi tiết ảnh",
                //    OkText = "OK",
                //    ImagePath = customerImage.FileName
                //});
                IoC.Get<ApplicationViewModel>().isLoading = false;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        // an nut button refresh
        private void HandleReFresh()
        {
            if (IoC.Get<ApplicationViewModel>().IsProcessing)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Hệ thống đang trong quá trình xử lý, vui lòng chờ đợi!");
                return;
            }
        }

        // click button back page login
        private void HandleBackPageClick()
        {
            if (IoC.Get<ApplicationViewModel>().IsProcessing)
            {
                IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Hệ thống đang trong quá trình xử lý, vui lòng chờ đợi!");
                return;
            }

            IoC.Get<ApplicationViewModel>().GotoPage(ApplicationPage.Login);
        }

        // choose folder luu
        private void HandleChooseFolder()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = false;
            openFileDialog.ValidateNames = false;
            openFileDialog.FileName = "Folder Selection.";

            if (PathFolder == null && PathFolder.Trim().Length == 0)
            {
                openFileDialog.InitialDirectory = "C:\\";
            }
            else
            {
                openFileDialog.InitialDirectory = SelectedSavePath;
            }

            openFileDialog.Filter = "All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                PathFolder = System.IO.Path.GetDirectoryName(openFileDialog.FileName);

                if (!ListSavePath.Contains(PathFolder))
                {
                    ListSavePath.Add(PathFolder);
                }

                SelectedSavePath = PathFolder;
                OnPropertyChanged("SelectedSavePath");
            }
        }


        [ValueConversion(typeof(bool), typeof(GridLength))]
        public class BoolToGridRowHeightConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return ((bool)value) ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {    // Don't need any convert back
                return null;
            }
        }
    }
}
