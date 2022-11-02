using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Models.Interfaces;

namespace WebGallery.Models
{
    public class WebCollection<TWebEntity> : IWebCollection<TWebEntity>
        where TWebEntity : class, IWebEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<TWebEntity> Items { get; set; }
    }
}
