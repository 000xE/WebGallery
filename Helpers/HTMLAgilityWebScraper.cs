using HtmlAgilityPack;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebGallery.Common.Services.Interfaces;
using WebGallery.Helpers.Interfaces;
using WebGallery.Models;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers
{
    public class HTMLAgilityWebScraper : IHTMLWebScraper
    {
        private readonly ILoggingService loggingService;

        private readonly SemaphoreSlim semaphoreSlim = new(1, 1);

        public HTMLAgilityWebScraper(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public async Task<Media> DownloadMediaAsync(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                //await this.semaphoreSlim.WaitAsync();
                var web = new HtmlWeb();
                try
                {
                    var document = await web.LoadFromWebAsync(url).ConfigureAwait(false);
                    var nodes = document.DocumentNode.SelectNodes("//meta");
                    //this.semaphoreSlim.Release();
                    return this.ScrapeMedia(nodes);
                } 
                catch (Exception e)
                {
                    this.loggingService.Error($"Failed to load the document for {url}", e);
                }
            }

            return default;
        }

        private Media ScrapeMedia(HtmlNodeCollection htmlNodes)
        {
            Media media = new()
            {
                Guid = Guid.NewGuid(),
                MediaType = Models.Enums.MediaType.Image
            };

            Thumbnail thumbnail = new()
            {
                MediaType = Models.Enums.MediaType.Image
            };

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
                                            thumbnail.MediaType = Models.Enums.MediaType.Image;
                                            media.MediaType = Models.Enums.MediaType.Image;

                                            if (property.Value.Contains(".gif"))
                                            {
                                                thumbnail.MediaType = Models.Enums.MediaType.Animated;
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

            media.Thumbnail = thumbnail;

            return media;
        }
    }
}
