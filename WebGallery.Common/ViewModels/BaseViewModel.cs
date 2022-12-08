using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.ViewModels.Interfaces;

namespace WebGallery.Common.ViewModels
{
    public class BaseViewModel : IBaseViewModel
    {
        protected IServiceProvider ServiceProvider { get; private set; }

        public BaseViewModel(IServiceProvider serviceProvider) 
        { 
            this.ServiceProvider = serviceProvider;
        }
    }
}
