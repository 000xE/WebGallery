using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Helpers.Interfaces;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;
using Windows.Storage;

namespace WebGallery.ViewModels.Pages
{
    [ObservableRecipient]
    public partial class WebCollectionPageViewModel : ItemsPageViewModel<WebCollection, IWebCollectionManager>, IFileProcessable
    {
        private readonly IWebHelper webHelper;
        private readonly IFileHelper fileHelper;
        private readonly IBitmapHelper bitmapHelper;

        private readonly IWebMediaManager webMediaManager;

        public WebCollectionPageViewModel(IServiceProvider serviceProvider, IWebHelper webHelper) : base(serviceProvider)
        {
            this.webHelper = webHelper;
            this.webMediaManager = serviceProvider.GetService<IWebMediaManager>();
            this.fileHelper = serviceProvider.GetService<IFileHelper>();
            this.bitmapHelper = serviceProvider.GetService<IBitmapHelper>();
        }

        protected override bool ShouldInitialiseItems => true;

        [ObservableProperty]
        private ObservableCollection<Category> _categories = new();

        [ObservableProperty]
        private Category _selectedCategory;

        public override void RefreshCollection(Expression<Func<WebCollection, bool>> expression)
        {
            base.RefreshCollection(expression);

            this.Categories = new();

            foreach (var item in this.Items)
            {
                this.CreateCategory(item);
            }
        }

        public override WebCollection NewObject(Func<WebCollection, bool> func)
        {
            var collection = base.NewObject(func);

            this.CreateCategory(collection);

            return collection;
        }

        public async Task ProcessFiles(List<StorageFile> files)
        {
            foreach (var file in files)
            {
                var collection = this.NewObject(i =>
                {
                    i.Name = file.DisplayName;
                    i.OriginalFilePath = file.Path;
                    return true;
                });

                var lines = File.ReadLines(file.Path);
                var uris = this.webHelper.ParseURLs(lines);

                //Parallel.ForEach(uris, /*parallelOptions: new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount}*/, async uri =>
                //{
                foreach (var uri in uris)
                {
                    var media = await this.webHelper.DownloadMedia(uri.ToString(), true);
                    if (media.Thumbnail.Data.Data != null)
                    {
                        var storageFile = await this.fileHelper.GetFileAsync(collection.ResourceFolderPath, media.Guid.ToString());
                        await this.bitmapHelper.SaveMediaAsync(storageFile, media.Thumbnail);

                        var saved = this.webMediaManager.NewSave((Func<WebMedia, bool>)(i =>
                        {
                            i.Guid = media.Guid;
                            i.Title = media.Title;
                            i.ThumbnailFilePath = storageFile.Path;
                            i.MediaType = media.MediaType;
                            i.URL = media.URL;
                            i.CollectionId = collection.Id;
                            return true;
                        }));
                    }
                }
                //});
            }
        }

        private void CreateCategory(WebCollection collection)
        {
            this.Categories.Add(new Category(collection.Name, Microsoft.UI.Xaml.Controls.Symbol.Library, collection.Id.ToString(), string.Empty));
        }

        public override void Dispose()
        {
            base.Dispose();

            this.Categories.Clear();
        }
    }
}
