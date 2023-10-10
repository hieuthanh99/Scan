using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Scan.Core;

namespace Scan.App
{
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member

        private readonly Window mWindow;

        private int mOuterMarginSize = 10;

        private int mWindowRadius = 10;

        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        #endregion

        #region Public Properties

        public double WindowMinimumWidth { get; set; } = 400;

        public double WindowMinimumHeight { get; set; } = 400;

        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); } }

        public int ResizeBorder { get { return Borderless ? 0 : 6; } }

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness InnerContentPadding { get { return new Thickness(0); } }

        public int OuterMarginSize
        {
            get
            {
                return Borderless ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return Borderless ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 45;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        #endregion

        #region Commands
        public ICommand onHideSideMenu { get; set; }

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand RestoreCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }
        #endregion

        #region Constructor
        public WindowViewModel(Window window)
        {
            mWindow = window;

            mWindow.StateChanged += (sender, e) =>
            {
                WindowResized();
            };
            onHideSideMenu = new RelayParameterizedCommand((parameter) => HandleHideSideMenu());

            MinimizeCommand = new RelayCommand(() => {
                mWindow.WindowState = WindowState.Minimized;
            });
            MaximizeCommand = new RelayCommand(() => {
                mWindow.WindowState = WindowState.Maximized;
            });
            RestoreCommand = new RelayCommand(() =>
            {
                mWindow.WindowState = WindowState.Normal;
            });
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            var resizer = new WindowResizer(mWindow);

            resizer.WindowDockChanged += (dock) =>
            {
                mDockPosition = dock;

                WindowResized();
            };
        }

        public WindowViewModel() { }

        #endregion

        #region Private Helpers
        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);

            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        private void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }


        #endregion

        /*
        Handle Click hide/show side bar
        */
        private void HandleHideSideMenu()
        {
            var currentStatus = IoC.Get<ApplicationViewModel>().SideMenuVisible;
            IoC.Get<ApplicationViewModel>().SideMenuVisible = !currentStatus;
        }
    }
}
