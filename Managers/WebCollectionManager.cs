﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Databases.Interfaces;
using WebGallery.Databases;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.Managers
{
    public class WebCollectionManager : BaseManager<WebCollection>, IWebCollectionManager
    {
        public WebCollectionManager(WebDatabase database, IServiceProvider serviceProvider) : base(database, serviceProvider)
        {
        }

        protected override WebCollection PreSave(WebCollection entity)
        {
            if (string.IsNullOrEmpty(entity.ResourceFolderPath))
            {
                if (entity.ParentId != 0)
                {
                    var parent = this.Get(entity.ParentId);
                    entity.ResourceFolderPath = Path.Combine(parent.ResourceFolderPath, entity.Guid.ToString());
                }
            }


            return base.PreSave(entity);
        }
    }
}
