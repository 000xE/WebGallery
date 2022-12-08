using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebGallery.Common.Enums;
using WebGallery.Common.Helpers.Interfaces;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WebGallery.Common.Helpers
{
    public class FileHelper : IFileHelper
    {
        public FileHelper()
        {

        }

        public string GetFolderPath(DirectoryType directoryType, params string[] paths)
        {
            string folderPath;

            switch (directoryType)
            {
                case DirectoryType.Resources:
                case DirectoryType.Database:
                default:
                    List<string> combinedPaths = new()
                    {
                        Path.Combine(ApplicationData.Current.LocalFolder.Path, directoryType.ToString())
                    }; 
                    
                    combinedPaths.AddRange(paths);

                    folderPath = Path.Combine(combinedPaths.ToArray());
                    break;
            }

            //To create folders within the path
            System.IO.Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        public string GetFilePath(DirectoryType directoryType, params string[] paths)
        {
            string filePath;

            switch (directoryType)
            {
                case DirectoryType.Resources:
                case DirectoryType.Database:
                default:
                    filePath = Path.Combine(this.GetSeparatedPaths(directoryType, paths));
                    break;
            }

            return filePath;
        }

        public IAsyncOperation<StorageFolder> GetFolderAsync(DirectoryType directoryType, params string[] paths)
        {
            return StorageFolder.GetFolderFromPathAsync(this.GetFolderPath(directoryType, paths));
        }

        public async Task<StorageFile> GetFileAsync(DirectoryType directoryType, params string[] paths)
        {
            var separatedPaths = this.GetSeparatedPaths(directoryType, paths);

            var folder = await this.GetFolderAsync(directoryType, separatedPaths[0]);

            return await this.CreateFileAsync(folder, separatedPaths[1]);
        }

        public IAsyncOperation<StorageFile> CreateFileAsync(StorageFolder storageFolder, string fileName)
        {
            return storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        }

        public async Task SaveImageAsync(StorageFile storageFile, int width, int height, byte[] data)
        {
            using IRandomAccessStream randomAccessStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);

            BitmapEncoder bitmapEncoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, randomAccessStream);

            bitmapEncoder.SetPixelData(BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Ignore,
                (uint)width,
                (uint)height,
                200,
                200,
                data);

            await bitmapEncoder.FlushAsync();
        }

        private string[] GetSeparatedPaths(DirectoryType directoryType, params string[] paths)
        {
            List<string> combinedPaths = new List<string>();

            if (paths.Length > 1)
            {
                combinedPaths.Add(this.GetFolderPath(directoryType, paths.Take(paths.Length - 1).ToArray()));
            }
            else
            {
                combinedPaths.Add(this.GetFolderPath(directoryType));
            }

            combinedPaths.Add(paths.LastOrDefault());

            return combinedPaths.ToArray();
        }
    }
}