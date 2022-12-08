using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System;
using WebGallery.Common.ViewModels.Interfaces;

namespace WebGallery.Common.ViewModels
{
    [ObservableObject]
    public partial class WindowViewModel : BaseViewModel, IWindowViewModel
    {
        private Window window;

        public WindowViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [ObservableProperty]
        private string _title;

        public IntPtr WindowHandle => WinRT.Interop.WindowNative.GetWindowHandle(this.window);

        public void Initialise(Window window)
        {
            this.window = window;
        }

        public void InitialiseWithWindow(object target)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(target, this.WindowHandle);
        }
    }
}
