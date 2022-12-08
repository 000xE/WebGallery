using System;

namespace WebGallery.ViewModels.Pages.Interfaces
{
    public interface IEntitySaveableViewModel<TEntity>
        where TEntity : class, IEntity, new()
    {
        int SaveObject(TEntity entity);

        TEntity NewObject(Func<TEntity, bool> func);
    }
}