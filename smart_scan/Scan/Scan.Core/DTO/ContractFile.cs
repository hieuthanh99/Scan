using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.DTO
{
    public class ContractFile
    {
        public int Sort { get; set; }
        public int CaptureId { get; set; }
        public int OwnerContractId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int DBId { get; set; }
        public string ContractName { get; set; }
        public string FileName { get; set; }
        public string CompressedPath { get; set; }
        public string ThumbnailPath { get; set; }
        public FileType FileType { get; set; }
        public string Content { get; set; }
        public string OcrExtractData { get; set; }
        public string OcrPath { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime ProcessTime { get; set; }
        public string IdServer { get; set; }
        public int IsViewFile { get; set; }
        public int PageIndex { get; set; }
        public int PageNumber { get; set; }   /* sử dụng cho việc đặt tên ảnh */

        /// bổ sung trường cho luồng Hồ sơ cá nhân - Tài sản đảm bảo
        public int OwnerDocumentId { get; set; }

        /// bổ sung trường thông tin cho luồng chấm chứng từ
        public Guid OwnerContractGuid { get; set; }
        public Guid OwnerDocumentGuid { get; set; }
        // bổ sung trường tính thời gian OCR
        public double? OcrTime { get; set; }
        public bool IsBlankPage { get; set; }
        public bool IsSelected { get; set; } = false;
        public double? CompressImgTime { get; set; }
        public double? UploadS3Time { get; set; }
        public double? PustFileToContractTime { get; set; }
    }

    public class ContractType
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as ContractType;
            if (item == null)
            {
                return false;
            }

            return this.Value == item.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }

    public class DocumentType
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string EcmCode { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as DocumentType;
            if (item == null)
            {
                return false;
            }

            return this.Value == item.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SignatureType
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as SignatureType;
            if (item == null)
            {
                return false;
            }

            return this.Value == item.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
