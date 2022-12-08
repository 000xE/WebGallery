using HtmlAgilityPack;
using System.Threading.Tasks;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers.Interfaces
{
    public interface IHTMLWebScraper
    {
        Task<ThumbnailMedia> DownloadMetadataAsync(string url);
    }
}