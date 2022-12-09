using WebGallery.Models.Enums;
using WebGallery.Models.Structures;

namespace WebGallery.Models.Interfaces
{
    public interface IMedia
    {
        MediaType MediaType { get; set; }

        string Title { get; set; }
    }
}