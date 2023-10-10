using Scan.Core.DTO;
using Scan.Core.Entities;
using Scan.Core.Settings;
using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToastNotifications.Core;

namespace Scan.Core
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Page
        private ApplicationPage _CurrentPage = ApplicationPage.Login;
        public ApplicationPage CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                _CurrentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        #endregion

        #region SidebarMenu
        private bool _SideMenuVisible = false;
        public bool SideMenuVisible
        {
            get
            {
                return _SideMenuVisible;
            }
            set
            {
                _SideMenuVisible = value;
                OnPropertyChanged("SideMenuVisible");
            }
        }
        #endregion
        #region ServerReachable
        public bool _serverReachable { get; set; } = false;
        public bool serverReachable
        {
            get
            {
                return _serverReachable;
            }
            set
            {
                _serverReachable = value;
                OnPropertyChanged("serverReachable");
            }
        }
     
        #endregion
        #region Toast
        private readonly ToastViewModel toastVM = new ToastViewModel();

        public void ShowMessage(NotifyType notifyType, string message)
        {
            MessageOptions opts = new MessageOptions
            {
                FreezeOnMouseEnter = false,
                UnfreezeOnMouseLeave = true
            };

            Action<string, MessageOptions> action;

            switch (notifyType)
            {
                case NotifyType.Success:
                    {
                        action = toastVM.ShowSuccess;
                        break;
                    }
                case NotifyType.Information:
                    {
                        action = toastVM.ShowInformation;
                        break;
                    }
                case NotifyType.Warning:
                    {
                        action = toastVM.ShowWarning;
                        break;
                    }
                case NotifyType.Error:
                    {
                        action = toastVM.ShowError;
                        break;
                    }
                default:
                    {
                        action = toastVM.ShowInformation;
                        break;
                    }
            }


            action(message, opts);
        }

        #endregion

        #region Constructor
        public ApplicationViewModel()
        {
            toastVM.OnUnloaded();
        }
        #endregion

        #region LoadingStatus
        private bool _isLoading { get; set; } = false;
        public bool isLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged("isLoading");
            }
        }
        #endregion

        #region HideShowRightBar
        private bool _isRightBar { get; set; } = false;
        public bool isRightBar
        {
            get
            {
                return _isRightBar;
            }
            set
            {
                _isRightBar = value;
                OnPropertyChanged("isRightBar");
            }
        }
        private bool _sideBarVisible { get; set; } = false;
        public bool sideBarVisible
        {
            get { return _sideBarVisible; }
            set
            {
                if (_sideBarVisible != value)
                {
                    _sideBarVisible = value;
                    OnPropertyChanged("sideBarVisible");
                }
            }
        }
        #endregion

        #region UserProfile
        private UserInfo _currentUser = null;
        public UserInfo CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged("CurrentUser");
                OnPropertyChanged("IsLogin");
            }
        }

        private Group _userGroupSelected = null;
        public Group UserGroupSelected
        {
            get
            {
                return _userGroupSelected;
            }
            set
            {
                _userGroupSelected = value;
                OnPropertyChanged("UserGroupSelected");
            }
        }

        private string _UserHistoryChoosed = null;
        public string UserHistoryChoosed
        {
            get
            {
                return _UserHistoryChoosed;
            }
            set
            {
                _UserHistoryChoosed = value;
                OnPropertyChanged("UserHistoryChoosed");
            }
        }

        public bool IsLogin
        {
            get
            {
                if (CurrentUser == null || String.IsNullOrEmpty(CurrentUser.UserName))
                {
                    return false;
                }

                return true;
            }
        }
        #endregion
        public void GotoPage(ApplicationPage page)
        {
            if (page == ApplicationPage.Login || page == ApplicationPage.ScanNoLogin)
            {
                CurrentUser = null;
            }
            else if (CurrentUser == null && CurrentPage == ApplicationPage.Login)
            {
                return;
            }

            CurrentPage = page;
        }
        #region ContractScan
        private BusinessTypeApp? _currentBusinessType { get; set; }
        public BusinessTypeApp? CurrentBusinessType
        {
            get
            {
                return _currentBusinessType;
            }
            set
            {
                _currentBusinessType = value;
                OnPropertyChanged("CurrentBusinessType");
            }
        }

        private List<dynamic> _currentContractList { get; set; }
        public List<dynamic> CurrentContractList
        {
            get
            {
                return _currentContractList;
            }
            set
            {
                _currentContractList = value;
                OnPropertyChanged("CurrentContractList");
            }
        }
        private ScanBusiness _currentScanBusiness { get; set; }
        public ScanBusiness CurrentScanBusiness
        {
            get
            {
                return _currentScanBusiness;
            }
            set
            {
                _currentScanBusiness = value;
                OnPropertyChanged("CurrentScanBusiness");
            }
        }
        private List<string> _deleteImagePathList { get; set; }
        public List<string> DeleteImagePathList
        {
            get
            {
                return _deleteImagePathList;
            }
            set
            {
                _deleteImagePathList = value;
                OnPropertyChanged("DeleteImagePathList");
            }
        }
        #endregion
        #region Number GlobalThread
        private int _NumberThread = 0;
        public int NumberThread
        {
            get
            {
                return _NumberThread;
            }
            set
            {
                _NumberThread = value;
                if (value <= 0)
                {
                    HasThreadRun = false;
                }
                else
                {
                    HasThreadRun = true;
                }
                OnPropertyChanged("NumberThread");
            }
        }

        private bool _HasThreadRun = false;
        public bool HasThreadRun
        {
            get
            {
                return _HasThreadRun;
            }
            set
            {
                _HasThreadRun = value;
                OnPropertyChanged("HasThreadRun");
            }
        }
        #endregion


        #region IsProcessing
        private bool _IsProcessing = false;
        public bool IsProcessing
        {
            get
            {
                return _IsProcessing;
            }
            set
            {
                _IsProcessing = value;
                OnPropertyChanged("IsProcessing");
            }
        }
        #endregion

        #region IsWorkerProcessing
        private bool _IsWorkerProcessing = false;
        public bool IsWorkerProcessing
        {
            get
            {
                return _IsWorkerProcessing;
            }
            set
            {
                _IsWorkerProcessing = value;
                OnPropertyChanged("IsWorkerProcessing");
            }
        }
        #endregion

        #region Version
        private string _ServerVersion { get; set; }
        public string ServerVersion
        {
            get
            {
                return _ServerVersion;
            }
            set
            {
                _ServerVersion = value;
                OnPropertyChanged("ServerVersion");
            }
        }

        private string _LocalVersion { get; set; }
        public string LocalVersion
        {
            get
            {
                return _LocalVersion;
            }
            set
            {
                _LocalVersion = value;
                OnPropertyChanged("LocalVersion");
            }
        }

        private bool _IsShowNotifyUpdate { get; set; }
        public bool IsShowNotifyUpdate
        {
            get
            {
                return _IsShowNotifyUpdate;
            }
            set
            {
                _IsShowNotifyUpdate = value;
                OnPropertyChanged("IsShowNotifyUpdate");
            }
        }
        #endregion

        public static readonly System.Threading.SemaphoreSlim _lock = new System.Threading.SemaphoreSlim(1, 1);

        #region dat lich autoupload
        private string _autoUploadScheduleStatus { get; set; }
        public string AutoUploadScheduleStatus
        {
            get
            {
                return _autoUploadScheduleStatus;
            }
            set
            {
                _autoUploadScheduleStatus = value;
                OnPropertyChanged("AutoUploadScheduleStatus");
            }
        }
        #endregion
    }
}
