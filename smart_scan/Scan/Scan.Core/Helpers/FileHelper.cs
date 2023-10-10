using Scan.Core.DTO;
using Scan.Core.Entities;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Helpers
{
    public class FileHelper
    {
        protected FileHelper()
        {
        }

        public static void DeleteFile(CaptureDetail item)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (!string.IsNullOrWhiteSpace(item.ImgPath))
            {
                TempData.Instance.Cleanup(item.ImgPath);
                // clean if compress file
                string fileName = Path.GetFileNameWithoutExtension(item.ImgPath);
                string ext = Path.GetExtension(item.ImgPath);
                string compressImgPath = Path.GetDirectoryName(item.ImgPath) + "\\" + fileName + ".comp" + ext;
                TempData.Instance.Cleanup(compressImgPath);
            }

            if (item.CompressedPath != null)
            {
                TempData.Instance.Cleanup(item.CompressedPath);
            }
            if (item.ThumbnailPath != null)
            {
                TempData.Instance.Cleanup(item.ThumbnailPath);
            }
            if (item.OcrPath != null)
            {
                TempData.Instance.Cleanup(item.OcrPath);
            }
        }

        public static void DeleteImage(ImageDTO item)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (!string.IsNullOrWhiteSpace(item.ImgPath))
            {
                TempData.Instance.Cleanup(item.ImgPath);
                // clean if compress file
                string fileName = Path.GetFileNameWithoutExtension(item.ImgPath);
                string ext = Path.GetExtension(item.ImgPath);
                string compressImgPath = Path.GetDirectoryName(item.ImgPath) + "\\" + fileName + ".comp" + ext;
                TempData.Instance.Cleanup(compressImgPath);
            }

            if (item.CompressedPath != null)
            {
                TempData.Instance.Cleanup(item.CompressedPath);
            }
            if (item.ThumbnailPath != null)
            {
                TempData.Instance.Cleanup(item.ThumbnailPath);
            }
            if (item.OcrPath != null)
            {
                TempData.Instance.Cleanup(item.OcrPath);
            }
        }

        public static void DeleteFile(ContractFile item)
        {
            CaptureDetail image = new CaptureDetail
            {
                ImgPath = item.FileName,
                CompressedPath = item.CompressedPath,
                ThumbnailPath = item.ThumbnailPath,
                OcrPath = item.OcrPath
            };

            DeleteFile(image);
        }

        public static Bitmap CombineBitmap(string[] files)
        {
            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(image);

                    //update the size of the final bitmap
                    height += bitmap.Height + 1;
                    width = bitmap.Width > width ? bitmap.Width : width;

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.LightGray);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image, new System.Drawing.Rectangle(0, offset, image.Width, image.Height));
                        offset += image.Height + 1;
                    }
                }
                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                {
                    finalImage.Dispose();
                }
                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }
    }
}
