using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Helpers.Interfaces;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.ViewModels.Pages
{
    public class WebMediaGalleryPageViewModel : ItemsPageViewModel<WebMedia, IWebMediaManager>, ILinesProcessableViewModel
    {
        private readonly IWebHelper webHelper;
        private readonly IFileHelper fileHelper;
        private readonly IBitmapHelper bitmapHelper;

        public WebMediaGalleryPageViewModel(IServiceProvider serviceProvider, IWebHelper webHelper) : base(serviceProvider)
        {
            this.webHelper = webHelper;
            this.fileHelper = serviceProvider.GetService<IFileHelper>();
            this.bitmapHelper = serviceProvider.GetService<IBitmapHelper>();
        }

        public WebCollection WebCollection { get; set; }

        public Task ProcessString(string str)
        {
            var lines = str.Split(new char[] { '\n', '\r', ','}, StringSplitOptions.RemoveEmptyEntries);
            return this.ProcessLines(lines);
        }

        public async Task ProcessLines(IEnumerable<string> lines)
        {
            var uris = this.webHelper.ParseURLs(lines);

            foreach (var uri in uris)
            {
                var media = await this.webHelper.DownloadMedia(uri.ToString(), true);
                if (media.Thumbnail?.Data.Data != null)
                {
                    var storageFile = await this.fileHelper.GetFileAsync(this.WebCollection.ResourceFolderPath, media.Guid.ToString());
                    await this.bitmapHelper.SaveMediaAsync(storageFile, media.Thumbnail);

                    var saved = this.NewObject((Func<WebMedia, bool>)(i =>
                    {
                        i.Guid = media.Guid;
                        i.Title = media.Title;
                        i.ThumbnailFilePath = storageFile.Path;
                        i.MediaType = media.MediaType;
                        i.URL = media.URL;
                        i.CollectionId = this.WebCollection.Id;
                        return true;
                    }));
                }
            }
        }
    }
}
