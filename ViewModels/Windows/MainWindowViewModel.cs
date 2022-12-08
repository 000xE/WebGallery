using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.ViewModels;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.ViewModels.Windows
{
    [ObservableRecipient]
    public partial class MainWindowViewModel : WindowViewModel
    {
        public MainWindowViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
