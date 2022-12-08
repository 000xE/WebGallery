using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using WebGallery.Common.Managers.Interfaces;
using WebGallery.Common.ViewModels;
using WebGallery.ViewModels.Pages.Interfaces;

namespace WebGallery.ViewModels.Pages
{
    [ObservableObject]
    public partial class ItemsPageViewModel<TEntity, TManager> : PageViewModel, IItemsViewModel<TEntity>, IDisposable
        where TEntity : class, IEntity, new()
        where TManager : class, IBaseManager<TEntity>
    {
        private readonly TManager manager;

        public ItemsPageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.manager = serviceProvider.GetService<TManager>();
        }

        public bool HasSelectedItem => this.SelectedItem != null;

        protected virtual bool ShouldInitialiseItems => false;

        protected virtual bool ShouldRefreshOnNewItems => true;

        protected virtual bool SelectFirstByDefault => true;

        protected virtual Expression<Func<TEntity, bool>> Expression => null;

#nullable enable
        [ObservableProperty]
        protected TEntity? _parentEntity;
#nullable disable

        [ObservableProperty]
        protected ObservableCollection<TEntity> _items = new ObservableCollection<TEntity>();

        [ObservableProperty]
        protected ObservableCollection<TEntity> _childItems = new ObservableCollection<TEntity>();

        [ObservableProperty]
        protected TEntity _selectedItem;

        public void Initialise(TEntity parentEntity = null)
        {
            this.ParentEntity = parentEntity;

            if (this.ShouldInitialiseItems)
            {
                this.RefreshCollection(this.Expression);
            }
        }

        public virtual void RefreshCollection(Expression<Func<TEntity, bool>> expression)
        {
            this._items = new ObservableCollection<TEntity>(this.GetObjects(expression));
            this.SelectedItem = this.Items.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetObjects(Expression<Func<TEntity, bool>> expression = null)
        {
            List<TEntity> objects = new();

            if (this.ParentEntity != null)
            {
                objects = this.manager.GetAll(i => i.ParentId == this.ParentEntity.Id);
            }
            else
            {
                objects = this.manager.GetAll();
            }

            if (expression != null)
            {
                objects = objects.Where(expression.Compile()).ToList();
            }

            return objects;
        }

        public int SaveObject(TEntity entity)
        {
            return this.manager.Save(entity);
        }

        public virtual TEntity NewObject(Func<TEntity, bool> func)
        {
            var entity = new TEntity();
            entity.Guid = Guid.NewGuid();

            if (this.ParentEntity != null)
            {
                entity.ParentId = this.ParentEntity.Id;
            }

            func(entity);

            var changed = this.manager.Save(entity);

            if (this.ShouldRefreshOnNewItems && changed > 0)
            {
                this.AddItem(entity);
            }

            return entity;
        }

        protected void AddItem(TEntity entity)
        {
            this._items.Add(entity);

            if (entity.ParentId != 0)
            {
                this._childItems.Add(entity);
            }
        }

        public virtual void Dispose()
        {
            this.Items.Clear();
            this.ChildItems.Clear();
        }
    }
}