using System;
using System.Collections.Generic;
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

namespace Scan.App
{
    /// <summary>
    /// Interaction logic for UserInfoItem.xaml
    /// </summary>
    public partial class UserInfoItem : UserControl
    {
        public ICommand onChooseUser
        {
            get { return (ICommand)GetValue(onChooseUserProperty); }
            set { SetValue(onChooseUserProperty, value); }
        }

        public static readonly DependencyProperty onChooseUserProperty = DependencyProperty.Register(
               "onChooseUser",
               typeof(ICommand),
               typeof(UserInfoItem),
               new UIPropertyMetadata(null));

        public UserInfoItem()
        {
            InitializeComponent();
        }
    }
}
