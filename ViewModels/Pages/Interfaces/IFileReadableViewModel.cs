using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WebGallery.ViewModels.Pages.Interfaces
{
    public interface IFileProcessable
    {
        void ProcessFiles(List<StorageFile> files);
    }
}
