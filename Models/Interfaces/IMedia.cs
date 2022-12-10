using WebGallery.Models.Enums;

namespace WebGallery.Models.Interfaces
{
    public interface IMedia
    {
        MediaType MediaType { get; set; }

        string Title { get; set; }
    }
}