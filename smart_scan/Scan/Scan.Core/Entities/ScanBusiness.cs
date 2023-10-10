using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Entities
{
    public class ScanBusiness
    {
        public int Id { get; set; }
        public string ServerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        public string Describe { get; set; }
        public string AccessRole { get; set; }
        public string IconId { get; set; }
        public string IconUrl { get; set; }
        public int MaxPagesPerCapture { get; set; }
        public int MaxPagesPerDoc { get; set; }
        public int MaxSizePerPage { get; set; }
        public int AllowAttachment { get; set; }
        public string AllowAttachExts { get; set; }
        public int? MaxAttachPerCapture { get; set; }
        public int? MaxSizePerAttach { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }

        public ScanBusiness()
        {

        }

        public ScanBusiness(ScanBusinessDTO dto)
        {
            this.ServerId = dto.id;
            this.Code = dto.code;
            this.Name = dto.name;
            this.IsActive = dto.activated ? 1 : 0;
            this.SortOrder = dto.orderNumber;
            this.Describe = dto.describe;
            this.AccessRole = dto.accessRole;
            this.IconId = dto.iconId;
            this.MaxPagesPerCapture = dto.maxPagesPerCapture ?? 0;
            this.MaxPagesPerDoc = dto.maxPagesPerDoc ?? 0;
            this.MaxSizePerPage = dto.maxSizePerPage ?? 0;
            this.AllowAttachment = dto.allowAttachment ? 1 : 0;
            this.AllowAttachExts = dto.allowExtensions;
            this.MaxAttachPerCapture = dto.maxAttachmentsPerCapture;
            this.MaxSizePerAttach = dto.maxSizePerAttachment;
        }
    }

    public class ScanBusinessSync
    {
        public string SyncTime { get; set; }
    }
}
