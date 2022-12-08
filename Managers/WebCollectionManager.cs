using System;
using System.Collections.Generic;
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
    }
}
