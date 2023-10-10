using log4net;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Clock.Hocr;
using iTextSharp.text.pdf;

namespace Clock.ImageProcessing
{
    public class ImageProcessor
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ImageProcessor));

        private static readonly int MIN_A4_HEIGHT = 842;  // A4 72 DPI
        private static readonly int MIN_A4_WIDTH = 595;   // A4 72 DPI

        protected ImageProcessor() { }
        public static Bitmap DeskewImage(Bitmap bmp)
        {
            var bw = ConvertToBitonal(bmp);
            Deskew d = new Deskew(bw);
            var a = d.GetSkewAngle();
            return Deskew.RotateImage(bmp, a);
        }

        /// <summary>
        /// Nén ảnh, nếu dpi bé hơn vẫn thực hiện nén theo chuẩn jpeg
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static string CompressedImage(string imgPath, DpiResolution dpi)
        {
            try
            {
                float compressDpi = (int)dpi;

                using (Bitmap bmp = new Bitmap(imgPath))
                {
                    // tạo đường dẫn ảnh mới
                    string fileName = Path.GetFileNameWithoutExtension(imgPath);
                    string ext = Path.GetExtension(imgPath);
                    var compressImg = Path.GetDirectoryName(imgPath) + "\\" + fileName + ".comp" + ext;

                    var bmpWidth = bmp.Width;
                    var bmpHeight = bmp.Height;

                    float bmpHorizonalDpi = bmp.HorizontalResolution;
                    float bmpVerticalDpi = bmp.VerticalResolution;
                    var maxBmpDpi = Math.Max(bmpHorizonalDpi, bmpVerticalDpi);

                    int newWidth, newHeight;
                    float newHorizonalDpi, newVerticalDpi;
                    if (maxBmpDpi > compressDpi)
                    {
                        // thông tin ảnh mới
                        newHorizonalDpi = Math.Min(bmpHorizonalDpi, compressDpi);  // trục x
                        newVerticalDpi = Math.Min(bmpVerticalDpi, compressDpi);    // trục y 

                        newWidth = (int)Math.Round(bmpWidth * newHorizonalDpi / bmpHorizonalDpi);
                        newHeight = (int)Math.Round(bmpHeight * newVerticalDpi / bmpVerticalDpi);

                        // kiểm tra lại thông tin 
                        // là portrait layout
                        if (bmpHeight > bmpWidth)
                        {
                            newWidth = Math.Max(newWidth, MIN_A4_WIDTH);
                            newHeight = Math.Max(newHeight, MIN_A4_HEIGHT);
                        }
                        else
                        // landscape layout
                        {
                            newWidth = Math.Max(newWidth, MIN_A4_HEIGHT);
                            newHeight = Math.Max(newHeight, MIN_A4_WIDTH);
                        }
                    }
                    // Nếu dpi hiện tại <= dpi nén => bỏ qua thay đổi dpi, width, height
                    else
                    {
                        newHorizonalDpi = bmpHorizonalDpi;
                        newVerticalDpi = bmpVerticalDpi;

                        newWidth = bmpWidth;
                        newHeight = bmpHeight;
                    }

                    // nén ảnh theo chuân jpeg => giảm file size
                    ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    using (Bitmap resized = new Bitmap(bmp, new Size(newWidth, newHeight)))
                    {
                        resized.SetResolution(newHorizonalDpi, newVerticalDpi);
                        resized.Save(compressImg, encoder, null);
                        return compressImg;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return imgPath;
        }

        /// <summary>
        /// Nén ảnh chữ ký
        /// Đổi đuôi ảnh chữ ký sang jpg
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="dpi"></param>
        /// <returns></returns>
        public static string CompressedSignatureImage(string imgPath, DpiResolution dpi)
        {
            try
            {
                float compressDpi = (int)dpi;

                using (Bitmap bmp = new Bitmap(imgPath))
                {
                    // tạo đường dẫn ảnh mới
                    string fileName = Path.GetFileNameWithoutExtension(imgPath);
                    var compressImg = Path.GetDirectoryName(imgPath) + "\\" + fileName + ".comp.jpg";

                    float bmpHorizonalDpi = bmp.HorizontalResolution;
                    float bmpVerticalDpi = bmp.VerticalResolution;
                    var maxBmpDpi = Math.Max(bmpHorizonalDpi, bmpVerticalDpi);

                    // nén ảnh
                    ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    // Nếu dpi hiện tại > dpi nén => thực hiện thay đổi dpi
                    if (maxBmpDpi > compressDpi)
                    {
                        var newHorizonalDpi = Math.Min(bmpHorizonalDpi, compressDpi);  // trục x
                        var newVerticalDpi = Math.Min(bmpVerticalDpi, compressDpi);    // trục y 

                        bmp.SetResolution(newHorizonalDpi, newVerticalDpi);
                    }
                    bmp.Save(compressImg, encoder, null);

                    return compressImg;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return imgPath;
        }

        public static Bitmap CropBorders(int top, int bottom, int left, int right, Bitmap bmp)
        {
            BBox b = new BBox();
            b.Height = bmp.Height - (top + bottom);
            b.Width = bmp.Width - (left + right);
            b.Left = left;
            b.Top = top;


            return CropToRectangle(b, bmp);
        }

        //public static unsafe bool IsGrayScale(Image image)
        //{
        //    using (var bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb))
        //    {
        //        using (var g = Graphics.FromImage(bmp))
        //        {
        //            g.DrawImage(image, 0, 0);
        //        }

        //        var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

        //        var pt = (int*)data.Scan0;
        //        var res = true;

        //        for (var i = 0; i < data.Height * data.Width; i++)
        //        {
        //            var color = Color.FromArgb(pt[i]);

        //            if (color.A != 0 && (color.R != color.G || color.G != color.B))
        //            {
        //                res = false;
        //                break;
        //            }
        //        }

        //        bmp.UnlockBits(data);

        //        return res;
        //    }
        //}

        public static System.Drawing.Bitmap CropToRectangle(BBox b, Bitmap image)
        {

            Bitmap bmpImage = new Bitmap(image);
            Bitmap bmpCrop = bmpImage.Clone(b.Rectangle, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static System.Drawing.Bitmap CropToRectangle(Rectangle r, Bitmap image)
        {
            Bitmap bmpImage = new Bitmap(image);
            Bitmap bmpCrop = bmpImage.Clone(r, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static System.Drawing.Image GetImageAt(string inputImage, int pagenum)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(inputImage);

            Guid objGuid = image.FrameDimensionsList[0];
            FrameDimension frameDim = new FrameDimension(objGuid);

            for (int i = 0; i < image.GetFrameCount(frameDim); i++)
            {
                if (i == pagenum - 1)
                {

                    image.SelectActiveFrame(frameDim, i);

                    return image;
                }
            }

            return null;
        }

#pragma warning disable S3776
        public static Bitmap Blur(Bitmap image, Int32 blurSize)
        {
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
            new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    Int32 avgR = 0, avgG = 0, avgB = 0;
                    Int32 blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (Int32 x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (Int32 y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (Int32 x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                        for (Int32 y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return blurred;
        }

#pragma warning restore S3776
        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Image imgToResize, int Resolution, int Percent)
        {

            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            decimal percent = Percent / 100.00M;
            int destWidth = sourceWidth;
            int destHeight = sourceHeight;
            if (sourceWidth > 5)
                destWidth = (int)(sourceWidth * percent);

            if (sourceHeight > 5)
                destHeight = (int)(sourceHeight * percent);

            Bitmap b = new Bitmap(destWidth, destHeight);

            Graphics g = Graphics.FromImage((System.Drawing.Image)b);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            b.SetResolution(Resolution, Resolution);
            return b;
        }

        ////private static ImageCodecInfo GetEncoderInfo(string mimeType)
        ////{
        ////    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        ////    for (int i = 0; i < codecs.Length; i++)
        ////    {
        ////        if (codecs[i].MimeType == mimeType)
        ////            return codecs[i];
        ////    }

        ////    return null;
        ////}

        public static Image ResignImage(Image imgToResize)
        {
            int heightCut = 240;
            var originalWidth = imgToResize.Width;
            var originalHeight = imgToResize.Height;
            var destinationSize = new Size(originalWidth, originalHeight);

            var hRatio = (float)originalHeight / destinationSize.Height;
            var wRatio = (float)originalWidth / destinationSize.Width;

            var ratio = Math.Min(hRatio, wRatio);

            var hScale = Convert.ToInt32(destinationSize.Height * ratio);
            var wScale = Convert.ToInt32(destinationSize.Width * ratio);

            var startX = (originalWidth - wScale) / 2;
            var startY = (originalHeight - hScale) / 2;

            var sourceRectangle = new Rectangle(startX, startY, wScale, hScale - heightCut);

            var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height - heightCut);

            var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        public static void ResignImage(string imageInput, string imageOutput, double scaleFactor)
        {
            using (var srcImage = Image.FromFile(imageInput))
            {
                var newWidth = (int)(srcImage.Width * scaleFactor);
                var newHeight = (int)(srcImage.Height * scaleFactor);
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    newImage.Save(imageOutput);
                }
            }
        }

        public static ImageCodecInfo GetCodecInfoForName(string CodecType)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < info.Length; i++)
            {
                string EnumName = CodecType.ToString();
                if (info[i].FormatDescription.Equals(EnumName))
                {
                    return info[i];
                }
            }

            return null;

        }

        public static Bitmap GetAsBitmap(System.Drawing.Image image, int Dpi)
        {
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                return (Bitmap)image;
            }

            try
            {

                Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));

                bmp.SetResolution(Dpi, Dpi);


                return bmp;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public static Bitmap GetAsUnindexedBitmap(System.Drawing.Image image)
        {
            try
            {
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                bmp.SetResolution(image.HorizontalResolution * 2, image.VerticalResolution * 2);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;


                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static System.Drawing.Image ConvertToCCITTFaxTiff(string imageToConvert)
        {
            Image img = Image.FromFile(imageToConvert);
            return ConvertToCCITTFaxTiff(img);
        }

        public static System.Drawing.Image ConvertToCCITTFaxTiff(System.Drawing.Image image)
        {
            var bmg = GetAsBitmap(image, 300);
            Bitmap bitonalBmp = ConvertToBitonal(bmg);

            ImageCodecInfo codecInfo = GetCodecInfoForName("TIFF");

            EncoderParameters encoderParams = new EncoderParameters(2);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 08L);
            encoderParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.CompressionCCITT4);

            MemoryStream ms = new MemoryStream();
            bitonalBmp.Save(ms, codecInfo, encoderParams);
            bitonalBmp.Dispose();
            return System.Drawing.Image.FromStream(ms);
        }

        public static System.Drawing.Image ConvertToImage(string imageToConvert, string CodecName, long Quality, int Dpi)
        {
            Image img = Image.FromFile(imageToConvert);
            return ConvertToImage(img, CodecName, Quality, Dpi);
        }

        public static System.Drawing.Image ConvertToImage(System.Drawing.Image imageToConvert, string CodecName, long Quality, int Dpi)
        {
            Bitmap Bmp = GetAsBitmap(imageToConvert, Dpi);

            ImageCodecInfo codecInfo = GetCodecInfoForName(CodecName);

            EncoderParameters encoderParams = new EncoderParameters(1);

            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
            MemoryStream bms = new MemoryStream();
            Bmp.Save(bms, ImageFormat.Bmp);

            Bitmap newBitmap = new Bitmap(bms);

            newBitmap.SetResolution(Dpi, Dpi);

            MemoryStream ms = new MemoryStream();
            newBitmap.Save(ms, codecInfo, encoderParams);
            Bmp.Dispose();
            newBitmap.Dispose();
            return System.Drawing.Image.FromStream(ms);
        }

        public static System.Drawing.Image ConvertToJpeg(System.Drawing.Image imageToConvert, long Quality, int Dpi)
        {
            Bitmap Bmp = GetAsBitmap(imageToConvert, Dpi);
            Bmp.SetResolution(Dpi, Dpi);
            ImageCodecInfo codecInfo = GetCodecInfoForName("JPEG");

            EncoderParameters encoderParams = new EncoderParameters(1);

            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);


            MemoryStream ms = new MemoryStream();

            Bmp.Save(ms, codecInfo, encoderParams);
            Bmp.Dispose();
            return System.Drawing.Image.FromStream(ms);
        }

        public static System.Drawing.Image ConvertToPNG(System.Drawing.Image imageToConvert, long Quality, int Dpi)
        {
            Bitmap Bmp = GetAsBitmap(imageToConvert, Dpi);
            Bmp.SetResolution(Dpi, Dpi);
            ImageCodecInfo codecInfo = GetCodecInfoForName("PNG");

            EncoderParameters encoderParams = new EncoderParameters(1);

            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);


            MemoryStream ms = new MemoryStream();

            Bmp.Save(ms, codecInfo, encoderParams);
            Bmp.Dispose();
            return System.Drawing.Image.FromStream(ms);
        }

        public static Bitmap ConvertGrayscale(Bitmap image)
        {
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
            Bitmap blackAndWhite = new System.Drawing.Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = System.Drawing.Graphics.FromImage(blackAndWhite))
                graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
            new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // for every pixel in the rectangle region
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx++)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy++)
                {
                    // average the red, green and blue of the pixel to get a gray value
                    Color oc = blackAndWhite.GetPixel(xx, yy);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    blackAndWhite.SetPixel(xx, yy, nc);
                }
            }

            return blackAndWhite;
        }

        public static Bitmap ConvertToBitonal(Bitmap original)
        {
            Bitmap source = null;

            // If original bitmap is not already in 32 BPP, ARGB format, then convert
            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }
            else
            {
                source = original;
            }

            // Lock source bitmap in memory
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // Copy image data to binary array
            int imageSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

            // Unlock source bitmap
            source.UnlockBits(sourceData);

            // Create destination bitmap
            Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);
            destination.SetResolution(original.HorizontalResolution, original.VerticalResolution);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Create destination buffer
            imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int height = source.Height;
            int width = source.Width;
            int threshold = 580;

            // Iterate lines
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                int pixelValue = 128;

                // Iterate pixels
                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values) - Thanks murx
                    //                           B                             G                              R
                    pixelTotal = sourceBuffer[sourceIndex] + sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte)pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }

            // Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);

            // Unlock destination bitmap
            destination.UnlockBits(destinationData);

            // Dispose of source if not originally supplied bitmap
            if (source != original)
            {
                source.Dispose();
            }

            // Return
            return destination;
        }

        public static Bitmap ConvertToRGB(Bitmap original)
        {
            Bitmap newImage = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
            newImage.SetResolution(original.HorizontalResolution, original.VerticalResolution);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImageUnscaled(original, 0, 0);
            }
            return newImage;
        }

        public static string ConvertToBase64(Image original, ImageFormat format)
        {
            using (MemoryStream m = new MemoryStream())
            {
                original.Save(m, format);
                byte[] imageBytes = m.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static string ConvertFileToBase64(string filePath)
        {
            Byte[] bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        public static Stream ConvertFileToStream(string filePath)
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (var image = Image.FromFile(filePath))
                {
                    var ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Png);

                    return ms;
                }
            }
        }

        public static string ConvertImagesToPDF(List<System.Drawing.Image> original, string pathPDF)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER);
            try
            {

                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(pathPDF, FileMode.Create));
                document.Open();

                PdfPTable imageTable = new PdfPTable(1);
                imageTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageTable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                for (int i = 0; i < original.Count; i++)
                {
                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(original[i], System.Drawing.Imaging.ImageFormat.Png);


                    imageTable.AddCell(pic);



                    document.Add(imageTable);
                    document.NewPage();

                    imageTable = new PdfPTable(1);
                    imageTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    imageTable.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;


                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                document.Close();
            }


            return "";
        }

        //public static BitmapImage ConvertBitmapToBitmapImage(Bitmap image)
        //{
        //    using (MemoryStream memory = new MemoryStream())
        //    {
        //        image.Save(memory, ImageFormat.Bmp);
        //        memory.Position = 0;
        //        BitmapImage bitmapimage = new BitmapImage();
        //        bitmapimage.BeginInit();
        //        bitmapimage.StreamSource = memory;
        //        bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapimage.EndInit();

        //        return bitmapimage;
        //    }
        //}

        //public static Bitmap ConvertBitmapImageToBitmap(BitmapImage image)
        //{
        //    Bitmap bitmap;
        //    using (MemoryStream outStream = new MemoryStream())
        //    {
        //        BitmapEncoder enc = new BmpBitmapEncoder();
        //        enc.Frames.Add(BitmapFrame.Create(image));
        //        enc.Save(outStream);

        //        bitmap = new Bitmap(outStream);
        //    }

        //    return bitmap;
        //}

        //public static void SaveBitmapImage(BitmapImage bitmapImage, string filePath)
        //{
        //    BitmapEncoder encoder = new JpegBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        encoder.Save(fileStream);
        //    }
        //}

        public static string CreateThumbnailImage(string fileName)
        {
            try
            {
                using (Image image = Image.FromFile(fileName))
                {
                    Image thumb = image.GetThumbnailImage(64, 64, () => false, IntPtr.Zero);
                    string thumbPath = Path.ChangeExtension(fileName, "thumb");
                    thumb.Save(thumbPath);
                    return thumbPath;
                }
            }
            catch
            {
                return "";
            }
        }

        #region CropUnwantedBackground
        public static Bitmap AutoCrop(Bitmap bmp)
        {
            var backColor = GetMatchedBackColor(bmp);
            if (backColor.HasValue)
            {
                var bounds = GetImageBounds(bmp, backColor);
                var diffX = bounds[1].X - bounds[0].X + 1;
                var diffY = bounds[1].Y - bounds[0].Y + 1;
                var croppedBmp = new Bitmap(diffX, diffY);
                var g = Graphics.FromImage(croppedBmp);
                var destRect = new Rectangle(0, 0, croppedBmp.Width, croppedBmp.Height);
                var srcRect = new Rectangle(bounds[0].X, bounds[0].Y, diffX, diffY);
                g.DrawImage(bmp, destRect, srcRect, GraphicsUnit.Pixel);
                return croppedBmp;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Private Methods

        #region GetImageBounds
#pragma warning disable S3776
        private static Point[] GetImageBounds(Bitmap bmp, Color? backColor)
        {
            //--------------------------------------------------------------------
            // Finding the Bounds of Crop Area bu using Unsafe Code and Image Proccesing
            Color c;
            int width = bmp.Width, height = bmp.Height;
            bool upperLeftPointFounded = false;
            var bounds = new Point[2];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    c = bmp.GetPixel(x, y);
                    bool sameAsBackColor = ((c.R <= backColor.Value.R * 1.1 && c.R >= backColor.Value.R * 0.9) &&
                                            (c.G <= backColor.Value.G * 1.1 && c.G >= backColor.Value.G * 0.9) &&
                                            (c.B <= backColor.Value.B * 1.1 && c.B >= backColor.Value.B * 0.9));
                    if (!sameAsBackColor)
                    {
                        if (!upperLeftPointFounded)
                        {
                            bounds[0] = new Point(x, y);
                            bounds[1] = new Point(x, y);
                            upperLeftPointFounded = true;
                        }
                        else
                        {
                            if (x > bounds[1].X)
                                bounds[1].X = x;
                            else if (x < bounds[0].X)
                                bounds[0].X = x;
                            if (y >= bounds[1].Y)
                                bounds[1].Y = y;
                        }
                    }
                }
            }
            return bounds;
        }
#pragma warning restore S3776
        #endregion

        #region GetMatchedBackColor
        private static Color? GetMatchedBackColor(Bitmap bmp)
        {
            // Getting The Background Color by checking Corners of Original Image
            var corners = new Point[]{
            new Point(0, 0),
            new Point(0, bmp.Height - 1),
            new Point(bmp.Width - 1, 0),
            new Point(bmp.Width - 1, bmp.Height - 1)
        }; // four corners (Top, Left), (Top, Right), (Bottom, Left), (Bottom, Right)
            for (int i = 0; i < 4; i++)
            {
                var cornerMatched = 0;
                var backColor = bmp.GetPixel(corners[i].X, corners[i].Y);
                for (int j = 0; j < 4; j++)
                {
                    var cornerColor = bmp.GetPixel(corners[j].X, corners[j].Y);// Check RGB with some offset
                    if ((cornerColor.R <= backColor.R * 1.1 && cornerColor.R >= backColor.R * 0.9) &&
                        (cornerColor.G <= backColor.G * 1.1 && cornerColor.G >= backColor.G * 0.9) &&
                        (cornerColor.B <= backColor.B * 1.1 && cornerColor.B >= backColor.B * 0.9))
                    {
                        cornerMatched++;
                    }
                }
                if (cornerMatched > 2)
                {
                    return backColor;
                }
            }
            return null;
        }
        #endregion

        #endregion
    }
}

