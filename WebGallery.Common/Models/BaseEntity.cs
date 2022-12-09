using SQLite;
using System;
using WebGallery.Common.Models.Interfaces;

namespace WebGallery.Common.Models
{
    public abstract class BaseEntity : IEntity
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        [Indexed]
        public int ParentId { get; set; }

        public Guid Guid { get; set; }
    }
}
