using log4net;
using Scan.Core.Services;
using Scan.Core.Settings;
using Scan.Core;
using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

#pragma warning disable S3776
namespace Scan.App
{
    public class BaseDialogUserControl : UserControl
    {
        private readonly DialogWindow mDialogWindow;

        public ICommand CloseCommand { get; private set; }
        public ICommand OkCommand { get; private set; }

        public int WindowMinimumWidth { get; set; } = 250;

        public int WindowMinimumHeight { get; set; } = 100;

        public int TitleHeight { get; set; } = 30;

        public string Title { get; set; }

        public bool CloseDialogVisibility { get; set; } = true;

        public TaskCompletionSource<object> tcs { get; set; } = new TaskCompletionSource<object>();

        public BaseDialogUserControl()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                // Create a new dialog window
                mDialogWindow = new DialogWindow();
                mDialogWindow.ViewModel = new DialogWindowViewModel(mDialogWindow);
                mDialogWindow.ViewModel.CloseCommand = new RelayCommand(() => handleClose());
                // Create close command
                CloseCommand = new RelayParameterizedCommand((param) => handleClose());
                OkCommand = new RelayParameterizedCommand((param) => handleOk(param));
            }
        }

        private void handleOk(object param)
        {
            tcs.TrySetResult(param);
            mDialogWindow.Close();
        }

        private void handleClose()
        {
            tcs.TrySetResult(true);
            mDialogWindow.Close();
        }

        public Task<object> ShowDialog<T>(T viewModel)
            where T : BaseDialogViewModel
        {
            // Create a task to await the dialog closing

            Application.Current.Dispatcher.Invoke(() =>
            {
                mDialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                mDialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                mDialogWindow.ViewModel.TitleHeight = TitleHeight;
                mDialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;
                mDialogWindow.ViewModel.CloseDialogVisibility = viewModel.CloseDialogVisibility;

                // Set this control to the dialog window content
             //   mDialogWindow.ViewModel.Content = this;

                DataContext = viewModel;
                mDialogWindow.Owner = Application.Current.MainWindow;
                mDialogWindow.ShowInTaskbar = false;
                mDialogWindow.ShowDialog();
            });


            return tcs.Task;
        }
    }
}
#pragma warning restore S3776
