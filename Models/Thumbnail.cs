using WebGallery.Models.Enums;
using WebGallery.Models.Structures;

namespace WebGallery.Models
{
    public class Thumbnail
    {
        public MediaType MediaType { get; set; }

        public ThumbnailData Data { get; set; }
    }
}
