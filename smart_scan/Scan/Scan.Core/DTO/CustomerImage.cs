using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core
{
    /// <summary>
    /// Scan nhanh
    /// luu thong tin anh scan hoac import
    /// </summary>
    public class CustomerImage
    {
        public string FileName { get; set; }
        public string ThumbnailPath { get; set; }
        public int PageIndex { get; set; }
        public bool HasProcessed { get; set; } = false;
        public string OCRFile { get; set; }
    }
}
