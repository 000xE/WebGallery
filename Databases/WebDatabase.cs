using System;
using WebGallery.Common.Databases;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Models;

namespace WebGallery.Databases
{
    public class WebDatabase : BaseDatabase
    {
        public WebDatabase(IFileHelper fileHelper) : base(fileHelper)
        {
        }

        protected override string DatabaseName => nameof(WebDatabase);

        protected override Type[] EntityTypes => new Type[]
        {
            typeof(WebMedia),
            typeof(WebCollection)
        };
    }
}
