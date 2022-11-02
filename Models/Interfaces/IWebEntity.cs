using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGallery.Models.Interfaces
{
    public interface IWebEntity : IEntity
    {
        string URL { get; set; } 
    }
}
