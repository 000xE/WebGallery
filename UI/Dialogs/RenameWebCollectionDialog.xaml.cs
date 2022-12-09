// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RenameWebCollectionDialog : BaseContentDialog
    {
        private bool overrideCancel = true;

        public RenameWebCollectionDialog(object sender, IEntityViewModel<WebCollection> viewModel, WebCollection webCollection) : base(sender)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.WebCollection = webCollection;
        }

        public IEntityViewModel<WebCollection> ViewModel { get; private set; }

        public WebCollection WebCollection { get; private set; }

        protected override void BaseContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!string.IsNullOrEmpty(this.CollectionName.Text))
            {
                this.InfoBar.IsOpen = false;

                this.WebCollection.Name = this.CollectionName.Text;

                this.ViewModel.SaveObject(this.WebCollection);
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
