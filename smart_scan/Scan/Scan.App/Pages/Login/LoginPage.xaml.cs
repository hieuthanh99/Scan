﻿using Scan.Core;
using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Scan.App;
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
public partial class LoginPage : BasePage<LoginViewModel>, IFormLogin
{
    public SecureString SecurePassword => txtPassword.SecurePassword;

    public string UserName => txtUserName.Text;

    public LoginPage()
    {
        InitializeComponent();
    }

}
