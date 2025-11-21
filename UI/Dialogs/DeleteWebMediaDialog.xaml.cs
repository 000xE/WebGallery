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
using WebGallery.Common.UI;
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
    public sealed partial class DeleteWebMediaDialog : BaseContentDialog
    {
        public DeleteWebMediaDialog(object sender, IEntityViewModel<WebMedia> viewModel, IEnumerable<WebMedia> webMedia) : base(sender)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.WebMedia = webMedia;
        }

        public IEntityViewModel<WebMedia> ViewModel { get; private set; }

        public IEnumerable<WebMedia> WebMedia { get; private set; }

        protected override void BaseContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            foreach (var item in this.WebMedia.ToList())
            {
                this.ViewModel.DeleteObject(item);
            }
        }
    }
}
