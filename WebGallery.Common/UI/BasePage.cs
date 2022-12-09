using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Common.ViewModels.Interfaces;

namespace WebGallery.Common.UI
{
    public class BasePage : Page
    {
        public BasePage()
        {
            this.windowHelper = Ioc.Default.GetService<IWindowHelper>();
        }

        protected IWindowHelper windowHelper;

        protected void InitialiseWithWindow(object target) => this.windowHelper.InitialiseWithWindow(target);
    }
}
