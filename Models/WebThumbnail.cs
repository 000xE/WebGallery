using Microsoft.UI.Xaml.Media.Imaging;
using WebGallery.Models.Interfaces;

namespace WebGallery.Models
{
    public class WebThumbnail : IWebEntity, IBinaryItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public string URL { get; set; }

        public WebThumbnail()
        {
            new BitmapImage();
        }
    }
}
