using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using WebGallery.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThumbnailGallery : Page
    {
        public ObservableCollection<WebThumbnail> WebImages = new ObservableCollection<WebThumbnail>();

        public ThumbnailGallery()
        {
            this.InitializeComponent();
            WebImages.Add(new WebThumbnail() { Name = "2" });
            WebImages.Add(new WebThumbnail());
            WebImages.Add(new WebThumbnail());
        }

        private void Gallery_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {

        }
    }
}
