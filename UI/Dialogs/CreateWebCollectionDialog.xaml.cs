using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
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
        private bool overrideCancel = true;

        public CreateWebCollectionDialog(object sender, IEntityViewModel<WebCollection> viewModel) : base (sender)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IEntityViewModel<WebCollection> ViewModel { get; private set; }

        protected override async void BaseContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!string.IsNullOrEmpty(this.CollectionName.Text))
            {
                this.InfoBar.IsOpen = false;

                if (this.ViewModel.ParentEntity == null)
                {
                    FolderPicker folderPicker = new();

                    this.InitialiseWithWindow(folderPicker);

                    folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                    folderPicker.FileTypeFilter.Add("*");

                    var folder = await folderPicker.PickSingleFolderAsync();

                    if (folder != null)
                    {
                        this.ViewModel.NewObject(i =>
                        {
                            i.Name = this.CollectionName.Text;
                            i.ResourceFolderPath = Path.Combine(folder.Path, i.Guid.ToString());
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
            else
            {
                this.InfoBar.Message = "Please enter a valid collection name";
                this.InfoBar.IsOpen = true;
            }
        }

        protected override void BaseContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.overrideCancel = false;
        }

        protected override void BaseContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (this.overrideCancel)
            {
                args.Cancel = string.IsNullOrEmpty(this.CollectionName.Text);
            }
            else
            {
                args.Cancel = false;
            }
        }
    }
}
