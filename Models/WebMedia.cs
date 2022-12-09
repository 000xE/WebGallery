using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Models.Enums;

namespace WebGallery.Models
{
    public class WebMedia : WebEntity
    {
        public MediaType MediaType { get; set; }

        public string ThumbnailFilePath { get; set; }
    }
}
