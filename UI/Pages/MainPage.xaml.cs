using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using WebGallery.UI.Dialogs;
using WebGallery.UI.Windows;
using WebGallery.ViewModels.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : BasePage
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

        private void OpenCollection_Click(object sender, SplitButtonClickEventArgs e)
        {
            if (this.ViewModel.HasSelectedItem)
            {
                var collectionWindow = new WebCollectionWindow(this.ViewModel.SelectedItem);
                collectionWindow.Activate();
            }
        }

        private async void CreateCollection_Click(object sender, RoutedEventArgs e)
        {
            CreateWebCollectionDialog dialog = new(sender, this.ViewModel);

            await dialog.ShowAsync();
        }
    }
}
