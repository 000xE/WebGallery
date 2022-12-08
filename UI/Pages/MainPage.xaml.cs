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
using WebGallery.UI.Dialogs;
using WebGallery.UI.Windows;
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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = Ioc.Default.GetService<MainPageViewModel>();
        }

        public MainPageViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.ViewModel.Initialise();
        }

        private void openCollection_Click(object sender, SplitButtonClickEventArgs e)
        {
            if (this.ViewModel.HasSelectedItem)
            {
                var collectionWindow = new WebCollectionWindow(this.ViewModel.SelectedItem);
                collectionWindow.Activate();
            }
        }

        private async void createCollection_Click(object sender, RoutedEventArgs e)
        {
            CreateWebCollectionDialog dialog = new CreateWebCollectionDialog(this.ViewModel)
            {
                XamlRoot = ((Button)sender).XamlRoot
            };

            var result = await dialog.ShowAsync();
        }
    }
}
