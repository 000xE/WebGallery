using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.ViewModels.Interfaces;

namespace WebGallery.Common.UI
{
    public class BasePage<TViewModel, TWindowViewModel> : Page
        where TViewModel : class, IPageViewModel
        where TWindowViewModel : class, IWindowViewModel
    {
        public BasePage()
        {
            this.DataContext = this.ViewModel = Ioc.Default.GetService<TViewModel>();
            this.WindowViewModel = Ioc.Default.GetService<TWindowViewModel>();
        }

        public TViewModel ViewModel { get; private set; }

        public TWindowViewModel WindowViewModel { get; private set; }
    }
}
