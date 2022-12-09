using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebGallery.ViewModels.Pages.Interfaces
{
    public interface IItemsViewModel<TEntity> : IEntityViewModel<TEntity>
        where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> GetObjects(Expression<Func<TEntity, bool>> expression = null);

        void RefreshCollection(Expression<Func<TEntity, bool>> expression);

        public void Initialise(TEntity parentEntity = null);
    }
}