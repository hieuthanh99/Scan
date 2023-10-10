using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Scan.App
{
    public class ImageToImageBitmap : BaseValueConverter<ImageToImageBitmap>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                try
                {
                    using (var bitmap = new Bitmap(value.ToString()))
                    {
                        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                        memory.Position = 0;
                        BitmapImage bitmapimage = new BitmapImage();
                        bitmapimage.BeginInit();
                        bitmapimage.StreamSource = memory;
                        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapimage.EndInit();

                        return bitmapimage;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ListImagesToImageBitmap : BaseValueConverter<ListImagesToImageBitmap>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                try
                {
                    string[] fileUrls = ((IEnumerable)value).Cast<object>().Select(x => x.ToString()).ToArray();
                    if (fileUrls != null && fileUrls.Length > 0)
                    {
                        if (fileUrls.Length == 1)
                        {
                            using (var bitmap = new Bitmap(fileUrls[0]))
                            {
                                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                                memory.Position = 0;
                                BitmapImage bitmapimage = new BitmapImage();
                                bitmapimage.BeginInit();
                                bitmapimage.StreamSource = memory;
                                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapimage.EndInit();

                                return bitmapimage;
                            }
                        }
                        else
                        {
                            using (var bitmap = Core.Helpers.FileHelper.CombineBitmap(fileUrls))
                            {
                                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                                memory.Position = 0;
                                BitmapImage bitmapimage = new BitmapImage();
                                bitmapimage.BeginInit();
                                bitmapimage.StreamSource = memory;
                                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapimage.EndInit();

                                return bitmapimage;
                            }
                        }
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }

            throw new NotImplementedException();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
