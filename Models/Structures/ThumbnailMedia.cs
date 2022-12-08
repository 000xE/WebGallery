using System;
using WebGallery.Models.Enums;
using WebGallery.Models.Interfaces;

namespace WebGallery.Models.Structures
{
    public struct ThumbnailMedia : IThumbnailMedia
    {
        public string Title { get; set; }

        public MediaType MediaType { get; set; }

        public Guid Guid { get; set; }

        public string URL { get; set; }

        public string ThumbnailURL { get; set; }

        public ThumbnailData ThumbnailData { get; set; }
    }
}
