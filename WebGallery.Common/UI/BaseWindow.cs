using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGallery.Common.Helpers.Interfaces;

namespace WebGallery.Common.UI
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {
            this.WindowHelper = Ioc.Default.GetService<IWindowHelper>();
            this.Activated += this.Window_Activated;
            this.Closed += this.Window_Closed;
        }

        public IWindowHelper WindowHelper { get; private set; }

        protected void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (args.WindowActivationState != WindowActivationState.Deactivated)
            {
                this.WindowHelper.SetWindow(this);
            }
        }

        protected void Window_Closed(object sender, WindowEventArgs args)
        {
            this.Activated -= this.Window_Activated;
            this.Closed -= this.Window_Closed;
        }
    }
}
