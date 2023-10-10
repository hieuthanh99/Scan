using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Scan.App
{
    /// <summary>
    /// Interaction logic for ScanNoLogin.xaml
    /// </summary>
    public partial class ScanNoLogin : BasePage<ScanNoLoginDynamsoftViewModel>
    {
        public ScanNoLogin()
        {
            InitializeComponent();
        }

        private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        // validate khi nhap ten file
        private void FileNameValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex($"[/\\\\:*?\"<>|]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
