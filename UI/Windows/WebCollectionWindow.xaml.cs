using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Common.UI;
using WebGallery.Models;
using WebGallery.UI.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebCollectionWindow : BaseWindow
    {
        public WebCollectionWindow(WebCollection webCollection)
        {
            this.InitializeComponent();

            this.Title = webCollection.Name;

            this.ContentFrame.Navigate(typeof(WebCollectionPage), webCollection); 
        }
    }
}
