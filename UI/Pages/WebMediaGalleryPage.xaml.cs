// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using WebGallery.Models;
using WebGallery.UI.Dialogs;
using WebGallery.ViewModels.Pages;
using WebGallery.ViewModels.Pages.Interfaces;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebMediaGalleryPage : BasePage
    {
        public WebMediaGalleryPage()
        {
            this.InitializeComponent();
            this.DataContext = this.ViewModel = Ioc.Default.GetService<WebMediaGalleryPageViewModel>();

            this.Unloaded += this.WebMediaGalleryPage_Unloaded;
        }

        public WebMediaGalleryPageViewModel ViewModel { get; private set; }
        
        public IEntityViewModel<WebCollection> CollectionViewModel { get; private set; } 

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.CollectionViewModel = e.Parameter as IEntityViewModel<WebCollection>;

            this.ViewModel.WebCollection = this.CollectionViewModel.SelectedItem;

            this.ViewModel.RefreshCollection(i => i.CollectionId == this.CollectionViewModel.SelectedItem.Id);
        }

        private async void AddLinks_Click(object sender, RoutedEventArgs e)
        {
            AddLinksDialog addLinksDialog = new(sender, this.ViewModel); 
            await addLinksDialog.ShowAsync();
        }

        private async void RenameCollection_Click(object sender, RoutedEventArgs e)
        {
            RenameWebCollectionDialog dialog = new(sender, this.CollectionViewModel, this.CollectionViewModel.SelectedItem);

            await dialog.ShowAsync();
        }

        private async void DeleteCollection_Click(object sender, RoutedEventArgs e)
        {
            DeleteWebCollectionDialog dialog = new(sender, this.CollectionViewModel, this.CollectionViewModel.SelectedItem);

            await dialog.ShowAsync();
        }

        private void WebMediaGalleryPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= this.WebMediaGalleryPage_Unloaded;
            this.ViewModel.Dispose();
        }

        private void CopyLinks_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dataPackage = new DataPackage(); 
            dataPackage.RequestedOperation = DataPackageOperation.Copy;

            var urls = this.Gallery.SelectedItems.Cast<WebMedia>().Select(i => i.URL);
            dataPackage.SetText(string.Join('\n', urls)); 

            Clipboard.SetContent(dataPackage);
        }

        private void Gallery_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            this.CopyLinks.IsEnabled = this.Gallery.SelectedItems.Count > 0;
        }
    }
}
