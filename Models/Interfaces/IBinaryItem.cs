namespace WebGallery.Models.Interfaces
{
    public interface IBinaryItem : IWebEntity
    {
        byte[] Data { get; set; }
    }
}