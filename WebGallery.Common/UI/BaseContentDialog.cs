using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WebGallery.Common.Helpers.Interfaces;

namespace WebGallery.Common.UI
{
    public class BaseContentDialog : ContentDialog
    {
        public BaseContentDialog(object sender)
        {
            this.XamlRoot = ((UIElement)sender).XamlRoot;
            this.windowHelper = Ioc.Default.GetService<IWindowHelper>();
        }

        protected IWindowHelper windowHelper;

        protected void InitialiseWithWindow(object target) => this.windowHelper.InitialiseWithWindow(target);
    }
}
