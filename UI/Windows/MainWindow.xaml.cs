using WebGallery.UI.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WebGallery.UI.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.MainFrame.Navigate(typeof(MainPage));
        }
    }
}
