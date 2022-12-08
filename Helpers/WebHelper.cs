﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebGallery.Helpers.Interfaces;
using WebGallery.Models.Structures;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace WebGallery.Helpers
{
    public class WebHelper : IWebHelper
    {
        private readonly IHTMLWebScraper webScraper;
        private readonly HttpClient httpClient = new();

        public WebHelper(IHTMLWebScraper webScraper)
        {
            this.webScraper = webScraper;
        }

        public async Task<IEnumerable<ThumbnailMedia>> DownloadMedias(List<string> urls, bool includeData = false)
        {
            List<ThumbnailMedia> mediaList = new List<ThumbnailMedia>();

            List<Task> tasks= new List<Task>();

            Parallel.ForEach(urls, i =>
            {
                this.DownloadMedia(i, includeData).ContinueWith(task => mediaList.Add(task.Result));
            });

            await Task.WhenAll(tasks);

            return mediaList;
        }

        public async Task<ThumbnailMedia> DownloadMedia(string url, bool includeData = false)
        {
            var media = await this.webScraper.DownloadMetadataAsync(url);

            if (includeData)
            {
                media.ThumbnailData = await this.DownloadMediaData(media).ConfigureAwait(false);
            }

            return media;
        }

        public async Task<ThumbnailData> DownloadMediaData(ThumbnailMedia media)
        {
            ThumbnailData mediaData = new();

            if (media.MediaType != Models.Enums.MediaType.Audio)
            {
                if (!string.IsNullOrEmpty(media.ThumbnailURL))
                {
                    var response = await this.httpClient.GetAsync(new Uri(media.ThumbnailURL)).ConfigureAwait(false);

                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using IRandomAccessStream randomAccessStream = (await response.Content.ReadAsStreamAsync()).AsRandomAccessStream();

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
                }
            }
            else
            {
                //Todo
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
                    if (Uri.TryCreate(l.Substring(index), uriKind, out var uri))
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