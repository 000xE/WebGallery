using System;
using WebGallery.Databases;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.Managers
{
    public class WebMediaManager : BaseManager<WebMedia>, IWebMediaManager
    {
        public WebMediaManager(WebDatabase database, IServiceProvider serviceProvider) : base(database, serviceProvider)
        {

        }
    }
}
