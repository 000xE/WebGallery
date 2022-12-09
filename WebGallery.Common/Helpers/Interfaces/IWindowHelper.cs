using Microsoft.UI.Xaml;
using System;

namespace WebGallery.Common.Helpers.Interfaces
{
    public interface IWindowHelper
    {
        Window Window { get; }
        IntPtr WindowHandle { get; }

        void InitialiseWithWindow(object target);
        void SetWindow(Window window);
    }
}