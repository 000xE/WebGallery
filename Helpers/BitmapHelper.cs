﻿using System;
using System.Threading.Tasks;
using WebGallery.Helpers.Interfaces;
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
        public async Task SaveMediaAsync(StorageFile storageFile, MediaType mediaType, int width, int height, byte[] data)
        {
            using IRandomAccessStream randomAccessStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);

            switch (mediaType)
            {
                case MediaType.Video:
                case MediaType.Audio:
                    break;
                case MediaType.Animated:
                    await this.SaveBitmap(randomAccessStream, BitmapEncoder.GifEncoderId, width, height, data);
                    break;
                case MediaType.Image:
                    await this.SaveBitmap(randomAccessStream, BitmapEncoder.JpegEncoderId, width, height, data);
                    break;
            }

        }

        private async Task SaveBitmap(IRandomAccessStream randomAccessStream, Guid bitmapEncoderId, int width, int height, byte[] data)
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
