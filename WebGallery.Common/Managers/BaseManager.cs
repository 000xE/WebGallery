using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Databases.Interfaces;
using WebGallery.Common.Extensions;
using WebGallery.Common.Managers.Interfaces;
using WebGallery.Common.Models.Interfaces;
using Windows.Data.Xml.Dom;

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

        public TEntity Get(int id)
        {
            return this.Database.GetConnection().Get<TEntity>(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Database.GetConnection().Get<TEntity>(predicate);
        }

        public List<TEntity> GetAll()
        {
            return this.Database.GetConnection().Table<TEntity>().ToList();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Database.GetConnection().Table<TEntity>().Where(predicate).ToList();
        }

        public TEntity Find(int id)
        {
            return this.Database.GetConnection().Find<TEntity>(id);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Database.GetConnection().Find<TEntity>(predicate);
        }

        public int Save(TEntity entity)
        {
            int changed = 0;

            this.Database.RunInTransaction(conn =>
            {
                changed = conn.InsertOrReplaceExisting(entity);
            });

            return changed;
        }

        public TEntity NewSave(Func<TEntity, bool> func) 
        {
            var entity = new TEntity();
            entity.Guid = Guid.NewGuid();

            func(entity); 
            
            this.Save(entity);

            return entity;
        }
    }
}
