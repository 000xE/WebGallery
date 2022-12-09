using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WebGallery.Common.Helpers.Interfaces;

namespace WebGallery.Common.UI
{
    public class BaseContentDialog : ContentDialog
    {
        public BaseContentDialog()
        {
            this.windowHelper = Ioc.Default.GetService<IWindowHelper>();
        }

        protected IWindowHelper windowHelper;

        protected void InitialiseWithWindow(object target) => this.windowHelper.InitialiseWithWindow(target);
    }
}
