using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using WebGallery.UI.Dialogs;
using WebGallery.UI.Pages;
using WebGallery.ViewModels;
using WebGallery.ViewModels.Windows;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.MainFrame.Navigate(typeof(MainPage));
            this.ViewModel = Ioc.Default.GetService<MainWindowViewModel>();
        }

        public MainWindowViewModel ViewModel { get; set; }
    }
}
