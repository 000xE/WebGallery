using WebGallery.Models.Structures;

namespace WebGallery.Models.Interfaces
{
    public interface IThumbnailMedia : IWebMedia
    {
        ThumbnailData ThumbnailData { get; set; }
    }
}