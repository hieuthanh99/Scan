using Microsoft.TeamFoundation.Pipelines.WebApi;
using Scan.Core.Services;
using Scan.Core.Settings;
using Scan.Core.ViewModel.Base;
using Scan.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Scan.Core.DTO;
using System.IO;

namespace Scan.App
{
    public class HomeScanViewModel : BaseViewModel
    {
        private readonly WrapperServices services;

        public List<BusinessTypeDto> BusinessTypeTemp { get; set; }
        public ObservableCollection<BusinessTypeDto> ListBusinessType { get; set; }
        public ICommand ChooseBusinessType { get; set; }
        public ICommand onHideSideMenu { get; set; }
        private string _TextFilter { get; set; }
        public string TextFilter
        {
            get
            {
                return _TextFilter;
            }
            set
            {
                _TextFilter = value;
                FilterBusinessType(value);
            }
        }

        public HomeScanViewModel()
        {
            services = new WrapperServices();

            ChooseBusinessType = new RelayParameterizedCommand((parameter) => onChooseBusinessType(parameter));
            onHideSideMenu = new RelayParameterizedCommand((parameter) => HandleHideSideMenu());

            IoC.Get<ApplicationViewModel>().CurrentContractList = null;
            IoC.Get<ApplicationViewModel>().CurrentScanBusiness = null;

            BackgroundWorker loadScanBusinessWorker = new BackgroundWorker();
            loadScanBusinessWorker.DoWork += LoadScanBusinessWorker_DoWork; ;
            loadScanBusinessWorker.RunWorkerCompleted += LoadScanBusinessWorker_RunWorkerCompleted; ;

            loadScanBusinessWorker.RunWorkerAsync();

            if (IoC.Get<ApplicationViewModel>().DeleteImagePathList != null && IoC.Get<ApplicationViewModel>().DeleteImagePathList.Count > 0)
            {
                Task.Run(() =>
                {
                    foreach (string imgPath in IoC.Get<ApplicationViewModel>().DeleteImagePathList)
                    {
                        TempData.Instance.Cleanup(imgPath);
                    }
                });
            }
        }

        private void LoadScanBusinessWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IoC.Get<ApplicationViewModel>().isLoading = false;
        }

        private void LoadScanBusinessWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IoC.Get<ApplicationViewModel>().isLoading = true;
            BusinessTypeTemp = LoadBusinessType();
            FilterBusinessType(_TextFilter);
        }

        private void HandleHideSideMenu()
        {
            var currentStatus = IoC.Get<ApplicationViewModel>().SideMenuVisible;
            IoC.Get<ApplicationViewModel>().SideMenuVisible = !currentStatus;
        }

        private void onChooseBusinessType(object parameter)
        {
            BusinessTypeDto businessType = parameter as BusinessTypeDto;

            IoC.Get<ApplicationViewModel>().CurrentContractList = null;
            if (businessType != null)
            {
                Enum.TryParse(businessType.BusinessCode, out BusinessTypeApp myBusinessTypeApp);

                IoC.Get<ApplicationViewModel>().CurrentBusinessType = myBusinessTypeApp;
                IoC.Get<ApplicationViewModel>().CurrentScanBusiness = businessType.CurrentScanBusiness;
                ApplicationPage? page = GotoPage(myBusinessTypeApp);
                if (page == null)
                {
                    IoC.Get<ApplicationViewModel>().ShowMessage(NotifyType.Warning, "Luồng scan chưa được triển khai trên phiên bản hiện tại!");
                }
                else
                {
                    IoC.Get<ApplicationViewModel>().GotoPage(page.Value);
                }
            }
        }

        private ApplicationPage? GotoPage(BusinessTypeApp businessTypeApp)
        {
            switch (businessTypeApp)
            {
                case BusinessTypeApp.HSKH:
                    return ApplicationPage.ClassifyScan;
                case BusinessTypeApp.VHS:
                    return ApplicationPage.VHSScan;
                case BusinessTypeApp.TSDB:
                    return ApplicationPage.TSDBScanImport;
                case BusinessTypeApp.KHDN:
                    return ApplicationPage.KHDNClassifyScan;
                case BusinessTypeApp.CCT:
                    return ApplicationPage.CCTClassifyScan;
                case BusinessTypeApp.HSTT:
                    return ApplicationPage.HSTHEClassifyScan;
                case BusinessTypeApp.TDKHCN:
                    return ApplicationPage.TDCNClassifyScan;
                case BusinessTypeApp.TDKHDN:
                    return ApplicationPage.TDDNClassifyScan;
                case BusinessTypeApp.TSDBDN:
                    return ApplicationPage.TSDBDNScanImport;
                default:
                    return null;
            }
        }

        private void FilterBusinessType(string textFilter = "")
        {
            ListBusinessType = BusinessTypeTemp.Aggregate(new ObservableCollection<BusinessTypeDto>(), (current, val) =>
            {
                textFilter = textFilter ?? "";
                if (val.Name.ToLower().Contains(textFilter.ToLower()))
                {
                    current.Add(val);
                }
                return current;
            });

            OnPropertyChanged("ListBusinessType");
        }

        private List<BusinessTypeDto> LoadBusinessType()
        {
            UserInfo user = IoC.Get<ApplicationViewModel>().CurrentUser;
            var roles = user.Roles;

            var listBusinessType = new List<BusinessTypeDto>();
            ////if (checkRole(roles, RoleName.flow_cn_hskh_quay))
            ////{
            ////    listBusinessType.Add(new BusinessTypeDto
            ////    {
            ////        Id = 1,
            ////        BusinessCode = BusinessTypeApp.HSKH.ToString(),
            ////        Name = "Hồ sơ khách hàng \n(luồng quầy)",
            ////        UserId = 1,
            ////        Icon = @"\Images\image.png",
            ////        IsSelected = true
            ////    });
            ////}

            if (checkRole(roles, RoleName.flow_dn_hskh_quay))
            {
                listBusinessType.Add(new BusinessTypeDto
                {
                    Id = 2,
                    BusinessCode = BusinessTypeApp.KHDN.ToString(),
                    Name = "Hồ sơ KHDN \n(OCR support)",
                    UserId = 1,
                    Icon = @"\Images\image.png",
                    IsSelected = true,
                    SortOrder = 2,
                });
            }

            if (checkRole(roles, RoleName.flow_cn_hskh_vhs))
            {
                listBusinessType.Add(new BusinessTypeDto
                {
                    Id = 3,
                    BusinessCode = BusinessTypeApp.VHS.ToString(),
                    Name = "Hợp đồng chuyển \nvận hành số",
                    UserId = 1,
                    Icon = @"\Images\image-demo.png",
                    IsSelected = true,
                    SortOrder = 3,
                });
            }

            if (checkRole(roles, RoleName.flow_cn_hstd_hsdb))
            {
                listBusinessType.Add(new BusinessTypeDto
                {
                    Id = 4,
                    BusinessCode = BusinessTypeApp.TSDB.ToString(),
                    Name = "Hợp đồng Tài sản \nđảm bảo - KHCN",
                    UserId = 1,
                    Icon = @"\Images\image-demo.png",
                    IsSelected = true,
                    SortOrder = 4,
                });
            }

            if (checkRole(roles, RoleName.flow_common_cct))
            {
                listBusinessType.Add(new BusinessTypeDto
                {
                    Id = 5,
                    BusinessCode = BusinessTypeApp.CCT.ToString(),
                    Name = "Chấm chứng từ \ncuối ngày",
                    UserId = 1,
                    Icon = @"\Images\cct.png",
                    IsSelected = true,
                    SortOrder = 5,
                });
            }

         //   var listScanBusiness = services.ScanBusinessService.FindAllActive();
            string iconFolder = TempData.Instance.GetAppThemeIconFolder();
            //if (listScanBusiness != null)
            //{
            //    foreach (var scanBusiness in listScanBusiness)
            //    {
            //        if (checkRole(roles, scanBusiness.AccessRole))
            //        {
            //            listBusinessType.Add(new BusinessTypeDto
            //            {
            //                BusinessCode = scanBusiness.Code,
            //                Name = scanBusiness.Name,
            //                UserId = 1,
            //                Icon = getIcon(scanBusiness.IconUrl, iconFolder),
            //                IsSelected = true,
            //                CurrentScanBusiness = scanBusiness,
            //                SortOrder = scanBusiness.SortOrder
            //            });
            //        }
            //    }
            //}

            return listBusinessType.OrderByDescending(i => i.SortOrder.HasValue)
                .ThenBy(i => i.SortOrder).ToList();
        }

        private bool checkRole(List<Role> roles, RoleName roleName)
        {
            var result = roles.FirstOrDefault(r => r.RoleName == roleName.ToString());
            return result != null;
        }

        private bool checkRole(List<Role> roles, string roleName)
        {
            var result = roles.FirstOrDefault(r => r.RoleName == roleName);
            return result != null;
        }

        private string getIcon(string iconUrl, string iconFolder)
        {
            if (string.IsNullOrEmpty(iconUrl))
            {
                return @"\Images\image-demo.png";
            }

            string fullIconUrl = Path.Combine(iconFolder, iconUrl);
            if (File.Exists(fullIconUrl))
            {
                return fullIconUrl;
            }
            else
            {
                return @"\Images\image-demo.png";
            }
        }
    }
}
