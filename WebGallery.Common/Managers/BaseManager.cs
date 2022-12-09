using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Channels;
using WebGallery.Common.Databases.Interfaces;
using WebGallery.Common.Extensions;
using WebGallery.Common.Managers.Interfaces;
using WebGallery.Common.Models.Interfaces;

namespace WebGallery.Common.Managers
{
    public class BaseManager<TEntity> : IBaseManager<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly IDatabase Database;
        protected readonly IServiceProvider ServiceProvider;

        public BaseManager(IDatabase database, IServiceProvider serviceProvider)
        {
            this.Database = database;
            this.ServiceProvider = serviceProvider;
        }

        public virtual TEntity Get(int id)
        {
            return this.Database.GetConnection().Get<TEntity>(id);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Database.GetConnection().Get<TEntity>(predicate);
        }

        public virtual List<TEntity> GetAll()
        {
            return this.Database.GetConnection().Table<TEntity>().ToList();
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Database.GetConnection().Table<TEntity>().Where(predicate).ToList();
        }

        public virtual TEntity Find(int id)
        {
            return this.Database.GetConnection().Find<TEntity>(id);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Database.GetConnection().Find<TEntity>(predicate);
        }

        public virtual int Delete(int id)
        {
            var record = this.Find(id);
            if (record != null)
            {
                return this.Delete(record);
            }

            return 0;
        }

        public virtual int Delete(TEntity entity)
        {
            var changed = 0;

            var delete = this.PreDelete(entity);

            this.Database.RunInTransaction(conn =>
            {
                changed = conn.Delete(delete);
            });

            if (changed > 0)
            {
                this.PostDelete(entity);
            }

            return changed;
        }


        public virtual int Delete(IEnumerable<int> ids)
        {
            var changed = 0;

            foreach (var id in ids)
            {
                changed += this.Delete(id);
            }

            return changed;
        }

        public virtual int Save(TEntity entity)
        {
            int changed = 0;

            var preSavedEntity = this.PreSave(entity);

            this.Database.RunInTransaction(conn =>
            {
                changed = conn.InsertOrReplaceExisting(preSavedEntity);
            });

            if (changed > 0)
            {
                this.PostSave(entity);
            }

            return changed;
        }

        public virtual TEntity NewSave(Func<TEntity, bool> func) 
        {
            var entity = new TEntity
            {
                Guid = Guid.NewGuid()
            };

            func(entity); 
            
            this.Save(entity);

            return entity;
        }

        protected virtual TEntity PreSave(TEntity entity)
        {
            return entity;
        }

        protected virtual void PostSave(TEntity entity)
        {

        }

        protected virtual TEntity PreDelete(TEntity entity)
        {
            return entity;
        }
        protected virtual void PostDelete(TEntity entity)
        {

        }
    }
}
