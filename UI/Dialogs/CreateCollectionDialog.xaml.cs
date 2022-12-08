using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WebGallery.Models;
using WebGallery.ViewModels.Pages.Interfaces;
using WebGallery.ViewModels.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateWebCollectionDialog : ContentDialog
    {
        public CreateWebCollectionDialog(IEntitySaveableViewModel<WebCollection> viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IEntitySaveableViewModel<WebCollection> ViewModel { get; set; }

        private void CreateCollectionDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.ViewModel.NewObject(i => 
            { 
                i.Name = this.CollectionName.Text;
                return true; 
            });
        }
    }
}
