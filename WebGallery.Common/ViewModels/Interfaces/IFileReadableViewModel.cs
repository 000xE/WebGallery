using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WebGallery.Common.ViewModels.Interfaces
{
    public interface IFileProcessable
    {
        Task ProcessFiles(List<StorageFile> files);
    }
}
