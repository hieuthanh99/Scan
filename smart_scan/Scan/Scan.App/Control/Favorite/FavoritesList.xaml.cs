using Scan.Core;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scan.App;

namespace Scan.App
{
    /// <summary>
    /// Interaction logic for FavoritesList.xaml
    /// </summary>
    public partial class FavoritesList : UserControl, INotifyPropertyChanged
    {
        public List<BusinessTypeFavoriteDto> BusinessTypeTemp { get; set; }
        public ObservableCollection<BusinessTypeFavoriteDto> ListBusinessType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FavoritesList()
        {
            InitializeComponent();
            //Data test
            ListBusinessType = new ObservableCollection<BusinessTypeFavoriteDto>();
            Random random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                Color randomColor = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                ListBusinessType.Add(new BusinessTypeFavoriteDto
                {
                    Id = i,
                    BusinessCode = BusinessTypeApp.CCT.ToString(),
                    Name = i == 1 ? "Hồ sơ KHDN (OCR support)" : "Hồ sơ KHDN (OCR support)",
                    UserId = 1,
                    Icon = @"\Images\cct.png",
                    IsSelected = true,
                    SortOrder = i,
                    BackgroundColor = new MySolidColorBrush(randomColor)

                });
            }
            // Gán DataContext của UserControl
            DataContext = this;

            IoC.Get<ApplicationViewModel>().CurrentContractList = null;
            IoC.Get<ApplicationViewModel>().CurrentScanBusiness = null;

            BackgroundWorker loadScanBusinessWorker = new BackgroundWorker();
            loadScanBusinessWorker.DoWork += LoadScanBusinessWorker_DoWork; ;
            loadScanBusinessWorker.RunWorkerCompleted += LoadScanBusinessWorker_RunWorkerCompleted; ;

            loadScanBusinessWorker.RunWorkerAsync();
        }

        private void LoadScanBusinessWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IoC.Get<ApplicationViewModel>().isLoading = false;
        }

        private void LoadScanBusinessWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IoC.Get<ApplicationViewModel>().isLoading = true;
            BusinessTypeTemp = LoadBusinessType();
        }

        private List<BusinessTypeFavoriteDto> LoadBusinessType()
        {
            UserInfo user = IoC.Get<ApplicationViewModel>().CurrentUser;
            if (user == null)
            {
                IoC.Get<ApplicationViewModel>().GotoPage(ApplicationPage.Login);
            }

            var listBusinessType = new List<BusinessTypeFavoriteDto>();

            listBusinessType.Add(new BusinessTypeFavoriteDto
            {
                Id = 1,
                BusinessCode = BusinessTypeApp.CCT.ToString(),
                Name = "Chấm chứng từ \ncuối ngày",
                UserId = 1,
                Icon = @"\Images\cct.png",
                IsSelected = true,
                SortOrder = 1,
            });
            listBusinessType.Add(new BusinessTypeFavoriteDto
            {
                Id = 2,
                BusinessCode = BusinessTypeApp.KHDN.ToString(),
                Name = "Hồ sơ KHDN \n(OCR support)",
                UserId = 1,
                Icon = @"\Images\image.png",
                IsSelected = true,
                SortOrder = 2,
            });
            listBusinessType.Add(new BusinessTypeFavoriteDto
            {
                Id = 3,
                BusinessCode = BusinessTypeApp.KHDN.ToString(),
                Name = "Hồ sơ KHDN \n(OCR support)",
                UserId = 1,
                Icon = @"\Images\image.png",
                IsSelected = true,
                SortOrder = 3,
            });
            listBusinessType.Add(new BusinessTypeFavoriteDto
            {
                Id = 4,
                BusinessCode = BusinessTypeApp.KHDN.ToString(),
                Name = "Hồ sơ KHDN \n(OCR support)",
                UserId = 1,
                Icon = @"\Images\image.png",
                IsSelected = true,
                SortOrder = 4,
            });

            return listBusinessType;
        }
    }
}
