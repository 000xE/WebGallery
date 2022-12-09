using System;
using System.Threading.Tasks;
using WebGallery.Models.Enums;
using Windows.Storage;

namespace WebGallery.Helpers.Interfaces
{
    public interface IBitmapHelper
    {
        Task SaveMediaAsync(StorageFile storageFile, MediaType mediaType, int width, int height, byte[] data);
    }
}