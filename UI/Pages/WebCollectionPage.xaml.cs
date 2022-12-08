﻿using CommunityToolkit.Mvvm.DependencyInjection;
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
using WebGallery.UI.Dialogs;
using WebGallery.ViewModels.Pages;
using WebGallery.ViewModels.Pages.Interfaces;
using WebGallery.ViewModels.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebCollectionPage : Page
    {
        public WebCollectionPage()
        {
            this.InitializeComponent();

            this.DataContext = this.ViewModel = Ioc.Default.GetService<WebCollectionPageViewModel>();
            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            this.WindowViewModel = Ioc.Default.GetService<WindowViewModel>();

            this.Unloaded += this.WebCollectionPage_Unloaded;
        }

        public WebCollectionPageViewModel ViewModel { get; private set; }

        public WindowViewModel WindowViewModel { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel.Initialise(e.Parameter as WebCollection);
        }

        private void WebCollectionPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= this.WebCollectionPage_Unloaded;
            this.ViewModel.Dispose();
        }

        private void AddLinks_Click(object sender, RoutedEventArgs e)
        {
            //todo: pass URLs, then DL <--- all through viewmodel
        }

        private async void ImportFiles_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new();
            this.WindowViewModel.InitialiseWithWindow(fileOpenPicker);

            fileOpenPicker.FileTypeFilter.Add(".txt");
            var files = await fileOpenPicker.PickMultipleFilesAsync();

            this.ViewModel.ProcessFiles(files.ToList());

            //todo: read from storage files then pass URLs, then DL <--- all through viewmodel (1 collection per file!!!)
        }

        private async void NewCollection_Click(object sender, RoutedEventArgs e)
        {
            var casted = this.ViewModel as IEntitySaveableViewModel<WebCollection>;

            CreateWebCollectionDialog dialog = new(casted)
            {
                XamlRoot = ((Button)sender).XamlRoot
            };

            var result = await dialog.ShowAsync();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.ViewModel.SelectedCategory))
            {
                if (this.ViewModel.SelectedCategory != null && this.ViewModel.Items.Any())
                {
                    var intTag = Convert.ToInt32(this.ViewModel.SelectedCategory.Tag);

                    this.ViewModel.SelectedItem = this.ViewModel.Items.First(i => i.Id == intTag);
                    this.ContentFrame.Navigate(typeof(WebMediaGalleryPage), this.ViewModel.SelectedItem);
                }
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            
        }
    }
}