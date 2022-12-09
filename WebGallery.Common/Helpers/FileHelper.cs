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

        public string GetFolderPath(params string[] paths)
        {
            string folderPath = Path.Combine(paths);

            //To create folders within the path
            System.IO.Directory.CreateDirectory(folderPath);

            return folderPath;
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

        public IAsyncOperation<StorageFolder> GetFolderAsync(params string[] paths)
        {
            return StorageFolder.GetFolderFromPathAsync(this.GetFolderPath(paths));
        }

        public async Task<StorageFile> GetFileAsync(DirectoryType directoryType, params string[] paths)
        {
            var separatedPaths = this.GetSeparatedPaths(directoryType, paths);

            var folder = await this.GetFolderAsync(directoryType, separatedPaths[0]);

            return await this.CreateFileAsync(folder, separatedPaths[1]);
        }

        public async Task<StorageFile> GetFileAsync(params string[] paths)
        {
            var separatedPaths = this.GetSeparatedPaths(paths);

            var folder = await this.GetFolderAsync(separatedPaths[0]);

            return await this.CreateFileAsync(folder, separatedPaths[1]);
        }

        public IAsyncOperation<StorageFile> CreateFileAsync(StorageFolder storageFolder, string fileName)
        {
            return storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
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

        private string[] GetSeparatedPaths(params string[] paths)
        {
            List<string> combinedPaths = new List<string>();

            if (paths.Length > 1)
            {
                combinedPaths.Add(this.GetFolderPath(paths.Take(paths.Length - 1).ToArray()));
            }

            combinedPaths.Add(paths.LastOrDefault());

            return combinedPaths.ToArray();
        }
    }
}