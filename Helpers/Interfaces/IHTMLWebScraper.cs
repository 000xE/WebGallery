using HtmlAgilityPack;
using System.Threading.Tasks;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers.Interfaces
{
    public interface IHTMLWebScraper
    {
        Task<Media> DownloadMediaAsync(string url);
    }
}