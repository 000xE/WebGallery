using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq.Expressions;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.ViewModels.Pages
{
    [ObservableRecipient]
    public partial class MainPageViewModel : ItemsPageViewModel<WebCollection, IWebCollectionManager>
    {
        public MainPageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<WebCollection, bool>> Expression => i => i.ParentId == 0;

        protected override bool ShouldInitialiseItems => true;
    }
}
