﻿using System;
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
    /// Interaction logic for HomeScan.xaml
    /// </summary>
    public partial class HomeScan : BasePage<HomeScanViewModel>
    {
        public HomeScan()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var businessDataSelected = (sender as ListBox).SelectedItem;

            var viewModel = (HomeScanViewModel)DataContext;
            viewModel.ChooseBusinessType.Execute(businessDataSelected);
        }
    }
}
