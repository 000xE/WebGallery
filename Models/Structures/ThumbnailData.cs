using System.Drawing;
using WebGallery.Models.Enums;

namespace WebGallery.Models.Structures
{
    public struct ThumbnailData
    {
        public Size Size { get; set; }

        public byte[] Data { get; set; }
    }
}
