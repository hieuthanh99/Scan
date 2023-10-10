using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Entities
{
    public class SSImage
    {
        public int Id { get; set; }
        public string ServerId { get; set; }
        public int CaptureId { get; set; }
        public string ImgPath { get; set; }
        public string CompressedPath { get; set; }
        public string ThumbnailPath { get; set; }
        public string OcrPath { get; set; }
        public string ExtractData { get; set; }
        //public RealSqliteObject OcrDuration { get; set; }
        public string ImgType { get; set; }
        public string Status { get; set; }
        public int PageNumber { get; set; }
        public string CreatedTime { get; set; }
    }
}
