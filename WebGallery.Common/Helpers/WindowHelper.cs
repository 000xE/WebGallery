using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Helpers.Interfaces;

namespace WebGallery.Common.Helpers
{
    public class WindowHelper : IWindowHelper
    {
        private Window window;

        public WindowHelper() { }

        public Window Window => this.window;

        public IntPtr WindowHandle => WinRT.Interop.WindowNative.GetWindowHandle(this.window);

        public void SetWindow(Window window)
        {
            this.window = window;
        }

        public void InitialiseWithWindow(object target)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(target, this.WindowHandle);
        }
    }
}
