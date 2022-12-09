using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebGallery.ViewModels.Pages.Interfaces
{
    public interface IItemsViewModel<TEntity> : IEntitySaveableViewModel<TEntity>
        where TEntity : class, IEntity, new()
    {
        TEntity SelectedItem { get; }

        IEnumerable<TEntity> GetObjects(Expression<Func<TEntity, bool>> expression = null);

        void RefreshCollection(Expression<Func<TEntity, bool>> expression);

        public void Initialise(TEntity parentEntity = null);
    }
}