using Scan.Core.Entities;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.DTO
{
    public class ImageDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int DBId { get; set; }
        public string ServerId { get; set; }
        public int CaptureId { get; set; }
        public string ImgPath { get; set; }
        public string CompressedPath { get; set; }
        public string ThumbnailPath { get; set; }
        public string OcrPath { get; set; }
        public string ExtractData { get; set; }
        public double? OcrDuration { get; set; }
        public FileType ImgType { get; set; }
        public string Status { get; set; }
        public int PageNumber { get; set; }

        public bool IsViewDetail { get; set; }
        public int CurrentPageProcess { get; set; }
        public Guid OwnerContractGuid { get; set; }
        public Guid OwnerDocumentGuid { get; set; }

        public ImageDTO()
        {
        }

        public ImageDTO(SSImage entity)
        {
            this.DBId = entity.Id;
            this.ServerId = entity.ServerId;
            this.CaptureId = entity.CaptureId;
            this.ImgPath = entity.ImgPath;
            this.CompressedPath = entity.CompressedPath;
            this.ThumbnailPath = entity.ThumbnailPath;
            this.OcrPath = entity.OcrPath;
            this.ExtractData = entity.ExtractData;
            //if (entity.OcrDuration != null)
            //{
            //    double duration;
            //    double.TryParse(entity.OcrDuration.Value, out duration);
            //    this.OcrDuration = duration;
            //}
            this.ImgType = string.IsNullOrWhiteSpace(entity.ImgType) ? FileType.hd_dichvu_none : Helpers.StringHelper.ConvertFileType(entity.ImgType);
            this.Status = entity.Status;
            this.PageNumber = entity.PageNumber;
        }
    }
}
