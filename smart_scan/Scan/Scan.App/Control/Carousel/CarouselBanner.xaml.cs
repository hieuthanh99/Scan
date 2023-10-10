using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Scan.App
{
    /// <summary>
    /// Interaction logic for CarouselBanner.xaml
    /// </summary>
    public partial class CarouselBanner : UserControl
    {
        //lưu trữ đường dẫn đến ảnh
        private string[] imagePaths;
        //theo dõi chỉ số của hình ảnh hiện tại trong mảng imagePaths.
        private int currentImageIndex = 0;
        private DispatcherTimer timer;


        public CarouselBanner()
        {
            InitializeComponent();

            // Đường dẫn đến các hình ảnh (đặt đúng đường dẫn của bạn)
            imagePaths = Directory.GetFiles("F:\\smart_scan_v3\\Scan\\Scan.App\\Assets\\Images\\banner", "*.jpg");
            //Kiểm tra xem có hình ảnh nào trong mảng imagePaths không.
            if (imagePaths.Length > 0)
            {
                ShowImage(currentImageIndex);
                StartTimer();
            }
        }

        private void ShowImage(int index)
        {
            // hiển thị hình ảnh từ mảng imagePaths dựa trên chỉ số index được truyền vào.
            string imagePath = imagePaths[index];
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
            imageSlider.Source = bitmapImage;
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2); // Thời gian giữa các lần chuyển đổi
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Chuyển đổi sang ảnh tiếp theo khi hết thời gian
            if (currentImageIndex < imagePaths.Length - 1)
            {
                currentImageIndex++;
            }
            else
            {
                currentImageIndex = 0;
            }

            ShowImage(currentImageIndex);
        }

        // Các sự kiện Previous và Next giữ nguyên
        // ...

        private void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex > 0)
            {
                currentImageIndex--;
            }
            else
            {
                currentImageIndex = imagePaths.Length - 1;
            }

            ShowImage(currentImageIndex);
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex < imagePaths.Length - 1)
            {
                currentImageIndex++;
            }
            else
            {
                currentImageIndex = 0;
            }

            ShowImage(currentImageIndex);
        }
    }
}