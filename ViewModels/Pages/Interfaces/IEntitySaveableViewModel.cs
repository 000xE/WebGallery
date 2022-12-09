using System;

namespace WebGallery.ViewModels.Pages.Interfaces
{
    public interface IEntitySaveableViewModel<TEntity>
        where TEntity : class, IEntity, new()
    {
#nullable enable
        TEntity? ParentEntity { get; }
#nullable disable

        int SaveObject(TEntity entity);

        TEntity NewObject(Func<TEntity, bool> func);
    }
}