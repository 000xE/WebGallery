using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Serilog;
using System;
using WebGallery.Common.Helpers;
using WebGallery.Common.Helpers.Interfaces;
using WebGallery.Common.Services;
using WebGallery.Common.Services.Interfaces;
using WebGallery.Databases;
using WebGallery.Helpers;
using WebGallery.Helpers.Interfaces;
using WebGallery.Managers;
using WebGallery.Managers.Interfaces;
using WebGallery.UI.Windows;
using WebGallery.ViewModels.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static Window Window => m_window;

        public static ILoggingService LoggingService => App.loggingService;

        private static ILoggingService loggingService;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            App.Initialise();

            this.UnhandledException += this.App_UnhandledException;
        }

        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            App.loggingService.Fatal("Unhandled exception detected", e.Exception);
        }

        protected static void Initialise()
        {
            Ioc.Default.ConfigureServices(App.GetServiceDescriptors().BuildServiceProvider());
            App.loggingService = Ioc.Default.GetService<ILoggingService>();
        }

        protected static IServiceCollection GetServiceDescriptors()
        {
            var collection = new ServiceCollection();

            App.SetPreliminaries(collection);
            App.SetHelpers(collection);
            App.SetServices(collection);
            App.SetDatabases(collection);
            App.SetManagers(collection);
            App.SetViewModels(collection);

            return collection;
        }

        protected static void SetPreliminaries(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddSingleton<IServiceProvider, Ioc>();
        }

        protected static void SetViewModels(IServiceCollection serviceDescriptors)
        {
            //Pages
            serviceDescriptors.AddTransient<MainPageViewModel>();
            serviceDescriptors.AddTransient<WebCollectionPageViewModel>();
            serviceDescriptors.AddTransient<WebMediaGalleryPageViewModel>();
        }

        protected static void SetHelpers(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddSingleton<IWindowHelper, WindowHelper>();
            serviceDescriptors.AddSingleton<IFileHelper, FileHelper>();
            serviceDescriptors.AddSingleton<IHTMLWebScraper, HTMLAgilityWebScraper>();
            serviceDescriptors.AddSingleton<IWebHelper, WebHelper>();
            serviceDescriptors.AddSingleton<IBitmapHelper, BitmapHelper>();
        }

        protected static void SetDatabases(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddSingleton<WebDatabase>();
        }

        protected static void SetManagers(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddSingleton<IWebCollectionManager, WebCollectionManager>();
            serviceDescriptors.AddSingleton<IWebMediaManager, WebMediaManager>();
        }

        protected static void SetServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddSingleton<ILoggingService, SeriLogLoggingService>();

            serviceDescriptors.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private static Window m_window;
    }
}
