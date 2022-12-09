using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Storage;

namespace WebGallery.Extensions
{
    public static class StorageFileExtensions
    {
        public static async Task<byte[]> GetPixelBufferFromFile(this StorageFile file)
        {
            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                    BitmapTransform transform = new BitmapTransform()
                    {
                        ScaledWidth = Convert.ToUInt32(50),
                        ScaledHeight = Convert.ToUInt32(50)
                    };

                    PixelDataProvider pixelDataProvider = await decoder.GetPixelDataAsync(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Straight,
                            transform,
                            ExifOrientationMode.IgnoreExifOrientation,
                            ColorManagementMode.DoNotColorManage);

                    byte[] sourcePixels = pixelDataProvider.DetachPixelData();

                    return sourcePixels;
                }
            }

            return null;
        }
    }
}
