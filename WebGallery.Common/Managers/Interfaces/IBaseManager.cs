using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebGallery.Common.Models.Interfaces;

namespace WebGallery.Common.Managers.Interfaces
{
    public interface IBaseManager<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        List<TEntity> GetAll();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity NewSave(Func<TEntity, bool> func);
        int Save(TEntity entity);
    }
}