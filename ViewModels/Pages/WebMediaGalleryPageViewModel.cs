using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Managers.Interfaces;
using WebGallery.Models;

namespace WebGallery.ViewModels.Pages
{
    public class WebMediaGalleryPageViewModel : ItemsPageViewModel<WebMedia, IWebMediaManager>
    {
        public WebMediaGalleryPageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
