using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using WebGallery.Models.Interfaces;
using WebGallery.Models.Structures;

namespace WebGallery.UI.Converters
{
    public class MediaToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                if (value is IThumbnailMedia media)
                {
                    var data = media.Thumbnail;

                    return this.GetBitmap(data.Data);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private WriteableBitmap GetBitmap(ThumbnailData mediaData)
        {
            var writeableBitmap = new WriteableBitmap(mediaData.Size.Width, mediaData.Size.Height);

            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                stream.Write(mediaData.Data, 0, mediaData.Data.Length);
            }

            return writeableBitmap;
        }
    }
}
