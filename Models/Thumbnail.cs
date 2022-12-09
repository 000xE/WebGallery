using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Models.Enums;
using WebGallery.Models.Structures;

namespace WebGallery.Models
{
    public class Thumbnail
    {
        public MediaType MediaType { get; set; }

        public ThumbnailData Data { get; set; }
    }
}
