using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Models;

namespace WebGallery.Models
{
    public abstract class WebEntity : BaseEntity
    {
        public string Title { get; set; }

        public string URL { get; set; }

        [Indexed]
        public int CollectionId { get; set; }
    }
}
