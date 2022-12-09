using System;
using System.Threading.Tasks;
using WebGallery.Helpers.Interfaces;
using WebGallery.Models;
using WebGallery.Models.Enums;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WebGallery.Helpers
{
    public class BitmapHelper : IBitmapHelper
    {
        public BitmapHelper()
        {

        }

        public async Task SaveMediaAsync(StorageFile storageFile, Thumbnail thumbnail)
        {
            using IRandomAccessStream randomAccessStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);

            var width = thumbnail.Data.Size.Width;
            var height = thumbnail.Data.Size.Height;
            var data = thumbnail.Data.Data;

            switch (thumbnail.MediaType)
            {
                case MediaType.Video:
                case MediaType.Audio:
                    break;
                case MediaType.Animated:
                    await BitmapHelper.SaveBitmap(randomAccessStream, BitmapEncoder.GifEncoderId, width, height, data);
                    break;
                case MediaType.Image:
                    await BitmapHelper.SaveBitmap(randomAccessStream, BitmapEncoder.JpegEncoderId, width, height, data);
                    break;
            }
        }

        private async static Task SaveBitmap(IRandomAccessStream randomAccessStream, Guid bitmapEncoderId, int width, int height, byte[] data)
        {
            BitmapEncoder bitmapEncoder = await BitmapEncoder.CreateAsync(bitmapEncoderId, randomAccessStream);

            bitmapEncoder.SetPixelData(BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Ignore,
                (uint)width,
                (uint)height,
                200,
                200,
                data);

            await bitmapEncoder.FlushAsync();
        }
    }
}
