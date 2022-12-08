namespace WebGallery.Models.Interfaces
{
    public interface IWebMedia : IMedia
    {
        string URL { get; set; }

        string ThumbnailURL { get; set; }
    }
}