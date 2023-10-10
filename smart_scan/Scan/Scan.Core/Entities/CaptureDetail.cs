using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Entities
{
    public class CaptureDetail
    {
        public int Id { get; set; }
        public int CaptureId { get; set; }
        public int DocId { get; set; }
        public string ImgPath { get; set; }
        public string CompressedPath { get; set; }
        public string ThumbnailPath { get; set; }
        public string OcrPath { get; set; }
        public string ExtractData { get; set; }
        public string OcrExtractData { get; set; }
        public string ImgType { get; set; }
        public string Status { get; set; }
        public string IdServer { get; set; }
        public string ProcessTime { get; set; }
        public int PageNumber { get; set; }
        public double? OcrTime { get; set; }
        public int? IsBlankPage { get; set; }
        public string OcrLogs { get; set; }
    }
}
