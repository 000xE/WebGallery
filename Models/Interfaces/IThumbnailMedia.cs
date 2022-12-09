namespace WebGallery.Models.Interfaces
{
    public interface IThumbnailMedia : IWebMedia
    {
        Thumbnail Thumbnail { get; set; }
    }
}