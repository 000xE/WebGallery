using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebGallery.Common.Services.Interfaces;
using WebGallery.Helpers.Interfaces;
using WebGallery.Models.Structures;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace WebGallery.Helpers
{
    public class WebHelper : IWebHelper
    {
        private readonly IHTMLWebScraper webScraper;
        private readonly ILoggingService loggingService;

        private readonly HttpClient httpClient = new();

        public WebHelper(IHTMLWebScraper webScraper, ILoggingService loggingService)
        {
            this.webScraper = webScraper;
            this.loggingService = loggingService;
        }

        public async Task<IEnumerable<Media>> DownloadMedias(List<string> urls, bool includeThumbnail = false)
        {
            List<Media> mediaList = new();

            List<Task> tasks= new();

            Parallel.ForEach(urls, i =>
            {
                this.DownloadMedia(i, includeThumbnail).ContinueWith(task => mediaList.Add(task.Result));
            });

            await Task.WhenAll(tasks);

            return mediaList;
        }

        public async Task<Media> DownloadMedia(string url, bool includeThumbnail = false)
        {
            var media = await this.webScraper.DownloadMediaAsync(url);

            if (includeThumbnail && media.Thumbnail != null)
            {
                media.Thumbnail.Data = await this.DownloadMediaThumbnail(media).ConfigureAwait(false);
            }

            return media;
        }

        public async Task<ThumbnailData> DownloadMediaThumbnail(Media media)
        {
            ThumbnailData mediaData = new();

            if (!string.IsNullOrEmpty(media.ThumbnailURL))
            {
                try
                {
                    var response = await this.httpClient.GetAsync(new Uri(media.ThumbnailURL)).ConfigureAwait(false);

                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using IRandomAccessStream randomAccessStream = (await response.Content.ReadAsStreamAsync()).AsRandomAccessStream();

                        try
                        {
                            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(randomAccessStream);

                            BitmapTransform transform = new()
                            {
                                ScaledHeight = decoder.PixelHeight,
                                ScaledWidth = decoder.PixelWidth
                            };

                            //PixelDataProvider pixelDataProvider = await decoder.GetPixelDataAsync();

                            PixelDataProvider pixelDataProvider = await decoder.GetPixelDataAsync(
                                    BitmapPixelFormat.Bgra8,
                                    BitmapAlphaMode.Straight,
                                    transform,
                                    ExifOrientationMode.IgnoreExifOrientation,
                                    ColorManagementMode.DoNotColorManage);

                            byte[] sourcePixels = pixelDataProvider.DetachPixelData();

                            mediaData.Size = new System.Drawing.Size()
                            {
                                Height = Convert.ToInt32(transform.ScaledHeight),
                                Width = Convert.ToInt32(transform.ScaledWidth)
                            };

                            mediaData.Data = sourcePixels;
                            /*
                            var contentStream = await response.Content.ReadAsStreamAsync();
                            BitmapImage bitmapImage = new();
                            await bitmapImage.SetSourceAsync(randomAccessStream);*/
                        }
                        catch (Exception e)
                        {
                            this.loggingService.Error("Failed to decode thumbnail", e);
                        }
                    }
                }
                catch (Exception e)
                {
                    this.loggingService.Error("Failed to download thumbnail", e);
                }
            }

            return mediaData;
        }

        public IEnumerable<Uri> ParseURLs(IEnumerable<string> lines, UriKind uriKind = UriKind.Absolute)
        {
            return lines.Select(l =>
            {
                int index = l.IndexOf("http");

                if (index == -1)
                {
                    index = l.IndexOf("www");
                }

                if (index != -1)
                {
                    if (Uri.TryCreate(l[index..], uriKind, out var uri))
                    {
                        return uri;
                    }
                }

                //todo: improve if no HTTP or WWW?

                return null;
            }).Where(i => i != null);
        }
    }
}
