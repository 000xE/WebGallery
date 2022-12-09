using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebGallery.Helpers.Interfaces;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers
{
    public class HTMLAgilityWebScraper : IHTMLWebScraper
    {
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public HTMLAgilityWebScraper()
        {

        }

        public async Task<ThumbnailMedia> DownloadMetadataAsync(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                //await this.semaphoreSlim.WaitAsync();
                var web = new HtmlWeb();
                var document = await web.LoadFromWebAsync(url).ConfigureAwait(false);
                var nodes =  document.DocumentNode.SelectNodes("//meta");
                //this.semaphoreSlim.Release();
                return this.ScrapeMedia(nodes);
            }

            return default;
        }

        private ThumbnailMedia ScrapeMedia(HtmlNodeCollection htmlNodes)
        {
            ThumbnailMedia media = new();
            media.Guid = Guid.NewGuid();
            media.MediaType = Models.Enums.MediaType.Image;

            if (htmlNodes != null)
            {
                Parallel.ForEach(htmlNodes, node =>
                {
                    if (node != null)
                    {
                        if (node.Attributes != null)
                        {
                            if (node.Attributes["property"] != null && node.Attributes["content"] != null)
                            {
                                HtmlAttribute property = node.Attributes["property"];
                                HtmlAttribute content = node.Attributes["content"];

                                if (property != null && content != null)
                                {
                                    switch (property.Value)
                                    {
                                        case "og:title":
                                            media.Title = content.Value;
                                            break;
                                        case "og:video:":
                                        case "og:video:url":
                                            media.URL = content.Value;
                                            media.MediaType = Models.Enums.MediaType.Video;
                                            break;
                                        case "og:url":
                                            media.URL = content.Value;
                                            break;
                                        case "og:image":
                                            if (property.Value.Contains(".gif"))
                                            {
                                                media.MediaType = Models.Enums.MediaType.Animated;
                                                media.URL = content.Value;
                                            }
                                            media.ThumbnailURL = content.Value;
                                            break;

                                    }
                                }
                            }
                        }
                    }
                });
            }

            return media;
        }
    }
}
