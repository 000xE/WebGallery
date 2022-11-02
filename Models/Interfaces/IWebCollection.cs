using System.Collections.Generic;

namespace WebGallery.Models.Interfaces
{
    public interface IWebCollection<TMediaEntity> : IEntity
        where TMediaEntity : class, IWebEntity
    {
        List<TMediaEntity> Items { get; set; }
    }
}