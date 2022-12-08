using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGallery.Common.Models.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        int ParentId { get; set; }

        Guid Guid { get; set; }
    }
}
