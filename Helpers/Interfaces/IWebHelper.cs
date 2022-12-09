using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebGallery.Models.Structures;

namespace WebGallery.Helpers.Interfaces
{
    public interface IWebHelper
    {
        Task<Media> DownloadMedia(string url, bool includeThumbnail = false);

        Task<ThumbnailData> DownloadMediaThumbnail(Media media);
        Task<IEnumerable<Media>> DownloadMedias(List<string> urls, bool includeThumbnail = false);
        IEnumerable<Uri> ParseURLs(IEnumerable<string> lines, UriKind uriKind = UriKind.Absolute);
    }
}