using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using WebGallery.Databases;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.Managers
{
    public class WebCollectionManager : BaseManager<WebCollection>, IWebCollectionManager
    {
        private readonly IWebMediaManager webMediaManager;

        public WebCollectionManager(WebDatabase database, IServiceProvider serviceProvider) : base(database, serviceProvider)
        {
            this.webMediaManager = serviceProvider.GetService<IWebMediaManager>();
        }

        protected override WebCollection PreSave(WebCollection entity)
        {
            var preSavedEntity = base.PreSave(entity);

            if (string.IsNullOrEmpty(preSavedEntity.ResourceFolderPath))
            {
                if (preSavedEntity.ParentId != 0)
                {
                    var parent = this.Get(preSavedEntity.ParentId);
                    preSavedEntity.ResourceFolderPath = Path.Combine(parent.ResourceFolderPath, preSavedEntity.Guid.ToString());
                }
            }

            return preSavedEntity;
        }

        protected override WebCollection PreDelete(WebCollection entity)
        {
            var mediaIds = this.webMediaManager.GetAll(i => i.CollectionId == entity.Id).Select(i => i.Id);

            this.webMediaManager.Delete(mediaIds);

            if (entity.ParentId == 0)
            {
                var subCollectionIds = this.GetAll(i => i.ParentId == entity.Id).Select(i => i.Id);

                this.Delete(subCollectionIds);
            }

            return base.PreDelete(entity);
        }

        protected override void PostDelete(WebCollection entity)
        {
            var directoryInfo = new DirectoryInfo(entity.ResourceFolderPath);

            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
                /*
                foreach (FileInfo file in directoryInfo.EnumerateFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo directory in directoryInfo.EnumerateDirectories())
                {
                    directory.Delete();
                }*/
            }

            base.PostDelete(entity);
        }
    }
}
