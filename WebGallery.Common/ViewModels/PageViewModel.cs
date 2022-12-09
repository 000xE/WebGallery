using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.ViewModels.Interfaces;

namespace WebGallery.Common.ViewModels
{
    public class PageViewModel : BaseViewModel, IPageViewModel
    {
        public PageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
