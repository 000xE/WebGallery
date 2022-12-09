using System;
using System.Threading.Tasks;
using WebGallery.Models;
using WebGallery.Models.Enums;
using Windows.Storage;

namespace WebGallery.Helpers.Interfaces
{
    public interface IBitmapHelper
    {
        Task SaveMediaAsync(StorageFile storageFile, Thumbnail thumbnail);
    }
}