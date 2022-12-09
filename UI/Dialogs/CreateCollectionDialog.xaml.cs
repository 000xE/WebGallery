using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Models;
using WebGallery.ViewModels.Pages.Interfaces;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateWebCollectionDialog : BaseContentDialog
    {
        public CreateWebCollectionDialog(IEntitySaveableViewModel<WebCollection> viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IEntitySaveableViewModel<WebCollection> ViewModel { get; set; }

        private async void CreateCollectionDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (this.ViewModel.ParentEntity == null)
            {
                FolderPicker folderPicker = new FolderPicker();

                this.InitialiseWithWindow(folderPicker);

                folderPicker.SuggestedStartLocation = PickerLocationId.Unspecified;
                folderPicker.FileTypeFilter.Add("*");

                var folder = await folderPicker.PickSingleFolderAsync();

                if (folder != null)
                {
                    this.ViewModel.NewObject(i =>
                    {
                        i.Name = this.CollectionName.Text;
                        i.ResourceFolderPath = folder.Path;
                        return true;
                    });
                }
            }
            else
            {
                this.ViewModel.NewObject(i =>
                {
                    i.Name = this.CollectionName.Text;
                    return true;
                });
            }
        }
    }
}
