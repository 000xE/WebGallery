// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CommunityToolkit.Mvvm.DependencyInjection;
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
using WebGallery.ViewModels.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebMediaGalleryPage : Page
    {
        public WebMediaGalleryPage()
        {
            this.InitializeComponent();
            this.DataContext = this.ViewModel = Ioc.Default.GetService<WebMediaGalleryPageViewModel>();

            this.Unloaded += WebMediaGalleryPage_Unloaded;
        }

        public WebMediaGalleryPageViewModel ViewModel { get; private set; }

        public WebCollection WebCollection { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.WebCollection = e.Parameter as WebCollection;

            if (this.WebCollection == null)
            {
                throw new NullReferenceException("WebCollection cannot be null");
            }

            this.ViewModel.RefreshCollection(i => i.CollectionId == this.WebCollection.Id);
        }

        private void WebMediaGalleryPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= WebMediaGalleryPage_Unloaded;
            this.ViewModel.Dispose();
        }
    }
}
