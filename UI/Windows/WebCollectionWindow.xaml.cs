using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using WebGallery.Common.ViewModels;
using WebGallery.Models;
using WebGallery.UI.Pages;
using WebGallery.ViewModels.Pages.Interfaces;
using WebGallery.ViewModels.Windows;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebCollectionWindow : Window
    {
        public WebCollectionWindow(WebCollection webCollection)
        {
            this.InitializeComponent();

            this.ViewModel = Ioc.Default.GetService<WindowViewModel>();
            this.ViewModel.Initialise(this);
            this.ViewModel.Title = webCollection.Name;

            this.ContentFrame.Navigate(typeof(WebCollectionPage), webCollection);
        }

        public WindowViewModel ViewModel { get; private set; }
    }
}
