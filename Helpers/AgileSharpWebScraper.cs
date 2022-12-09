using System.Threading.Tasks;
using WebGallery.Helpers.Interfaces;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers
{
    public class AgileSharpWebScraper : IHTMLWebScraper
    {
        public AgileSharpWebScraper()
        {

        }

        public Task<Media> DownloadMediaAsync(string url)
        {
            /*
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            string address = url;
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = await context.OpenAsync(address);
            string cellSelector = "tr.vevent td:nth-child(3)";
            IHtmlCollection<IElement> cells = document.QuerySelectorAll(cellSelector);
            IEnumerable<string> titles = cells.Select(m => m.TextContent);
            */

            return Task.FromResult(new Media());
        }
    }
}
