using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.DTO
{
    public class ResponseApi<T>
    {
        public int status { get; set; }
        public string error { get; set; }
        public string path { get; set; }
        public string clientMessageId { get; set; }
        public string soaErrorCode { get; set; }
        public string soaErrorDesc { get; set; }
        public T data { get; set; }

        public ResponseApi() { }

        public ResponseApi(ResponseApiV2<T> apiV2)
        {
            this.status = apiV2.status;
            this.error = apiV2.error;
            this.path = apiV2.path;
            this.clientMessageId = apiV2.clientMessageId;
            this.soaErrorCode = apiV2.soaErrorCode;
            this.soaErrorDesc = apiV2.soaErrorDesc;
            if (apiV2.data != null)
            {
                this.data = apiV2.data.result;
            }
        }
    }

    public class ResponseApiV2<T>
    {
        public int status { get; set; }
        public string error { get; set; }
        public string path { get; set; }
        public string clientMessageId { get; set; }
        public string soaErrorCode { get; set; }
        public string soaErrorDesc { get; set; }
        public ResponseData<T> data { get; set; }
    }

    public class ResponseData<T>
    {
        public T result { get; set; }
    }
}
