using Scan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.DTO
{
    public class BusinessTypeDto
    {
        public int Id { get; set; }
        public string BusinessCode { get; set; }
        public string Name { get; set; }
        public string BusinessGroup { get; set; }
        public int UserId { get; set; }
        public string Icon { get; set; }
        public int? SortOrder { get; set; }
        public bool IsSelected { get; set; } = false;
        public string BusinessExtendString { get; set; }
        public ScanBusiness CurrentScanBusiness { get; set; }
    }
}
