using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers.Interfaces
{
    public interface IWebHelper
    {
        Task<ThumbnailMedia> DownloadMedia(string url, bool includeData = false);

        Task<ThumbnailData> DownloadMediaData(ThumbnailMedia media);
        Task<IEnumerable<ThumbnailMedia>> DownloadMedias(List<string> urls, bool includeData = false);
        IEnumerable<Uri> ParseURLs(IEnumerable<string> lines, UriKind uriKind = UriKind.Absolute);
    }
}