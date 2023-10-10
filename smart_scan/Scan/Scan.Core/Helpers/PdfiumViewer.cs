using log4net;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfiumViewer;

namespace Scan.Core.Helpers
{
    public class PdfiumViewers
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PdfiumViewers));

        private static readonly int A4_HEIGHT = 842;  // A4 72 DPI
        private static readonly int A4_WIDTH = 595;   // A4 72 DPI

        protected PdfiumViewers()
        {
        }

        public static bool IsPdf(string path)
        {
            try
            {
                var pdfString = "%PDF-";
                var pdfBytes = System.Text.Encoding.ASCII.GetBytes(pdfString);
                var len = pdfBytes.Length;
                var buf = new byte[len];
                var remaining = len;
                var pos = 0;
                var fileLength = 0L;

                using (var f = File.OpenRead(path))
                {
                    fileLength = f.Length;
                    while (remaining > 0)
                    {
                        var amtRead = f.Read(buf, pos, remaining);
                        if (amtRead == 0) return false;
                        remaining -= amtRead;
                        pos += amtRead;
                    }
                }

                bool isPdf = pdfBytes.SequenceEqual(buf);
                bool notEmpty = fileLength > 100;

                return isPdf && notEmpty;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return false;
        }

        public static string ConvertPDFToBitmap(string pdfPath, string outputPath, Func<string, int, DateTime, string> func, int dpi = 300)
        {
            using (var document = PdfDocument.Load(pdfPath))
            {
                var pageCount = document.PageCount;

                for (int i = 0; i < pageCount; i++)
                {
                    SizeF pdfPage = document.PageSizes[i];

                    var pdfWidth = pdfPage.Width;
                    var pdfHeight = pdfPage.Height;

                    int newWidth, newHeight;
                    // portrait layout
                    if (pdfHeight > pdfWidth)
                    {
                        newHeight = Math.Min((int)pdfHeight, A4_HEIGHT);
                        newWidth = (int)Math.Round(pdfWidth * newHeight / pdfHeight);
                    }
                    else
                    // landscape layout
                    {
                        newWidth = Math.Min((int)pdfWidth, A4_WIDTH);
                        newHeight = (int)Math.Round(pdfHeight * newWidth / pdfWidth);
                    }

                    using (var image = document.Render(i, newWidth, newHeight, dpi, dpi, PdfRenderFlags.CorrectFromDpi | PdfRenderFlags.Annotations))
                    {
                        var timeProcess = DateTime.Now;
                        var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        var encParams = new EncoderParameters(1);
                        encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                        string filePath = Path.Combine(outputPath, Path.GetRandomFileName() + ".jpg");

                        image.Save(filePath, encoder, encParams);
                        // run function
                        var result = func(filePath, i, timeProcess);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
            return null;
        }
    }
}
