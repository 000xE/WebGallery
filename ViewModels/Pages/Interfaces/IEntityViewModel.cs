using System;

namespace WebGallery.ViewModels.Pages.Interfaces
{
    public interface IEntityViewModel<TEntity>
        where TEntity : class, IEntity, new()
    {
#nullable enable
        TEntity? ParentEntity { get; }
#nullable disable

        TEntity SelectedItem { get; }

        int SaveObject(TEntity entity);

        TEntity NewObject(Func<TEntity, bool> func);

        int DeleteObject(TEntity entity);
    }
}