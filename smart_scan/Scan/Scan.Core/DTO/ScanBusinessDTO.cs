using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core
{
    public class ScanBusinessDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string describe { get; set; }
        public string code { get; set; }
        public bool activated { get; set; }
        public int? orderNumber { get; set; }
        public string iconId { get; set; }
        public string accessRole { get; set; }
        public int? maxPagesPerCapture { get; set; }
        public int? maxPagesPerDoc { get; set; }
        public int? maxSizePerPage { get; set; }
        public bool allowAttachment { get; set; }
        public string allowExtensions { get; set; }
        public int? maxAttachmentsPerCapture { get; set; }
        public int? maxSizePerAttachment { get; set; }
    }

    public class ScanBusinessIconDTO
    {
        public string fileName { get; set; }
        public string iconData { get; set; }
    }
}
