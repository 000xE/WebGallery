using System.Threading.Tasks;
using WebGallery.Common.Enums;
using Windows.Foundation;
using Windows.Storage;

namespace WebGallery.Common.Helpers.Interfaces
{
    public interface IFileHelper
    {
        IAsyncOperation<StorageFile> CreateFileAsync(StorageFolder storageFolder, string fileName);
        Task<StorageFile> GetFileAsync(DirectoryType directoryType, params string[] paths);
        string GetFilePath(DirectoryType directoryType, params string[] paths);
        IAsyncOperation<StorageFolder> GetFolderAsync(DirectoryType directoryType, params string[] paths);
        string GetFolderPath(DirectoryType directoryType, params string[] paths);
        Task SaveImageAsync(StorageFile storageFile, int width, int height, byte[] data);
    }
}