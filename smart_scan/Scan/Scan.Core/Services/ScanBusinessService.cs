using log4net;
using RestSharp;
using Scan.Core.Entities;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class ScanBusinessService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ScanBusinessService));

        private static readonly List<string> IconExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".PNG", ".ICO" };

        //private readonly ScanBusinessRepository scanBusinessRepository;
        //private readonly ApiService apiService;

        public ScanBusinessService()
        {
            //scanBusinessRepository = scanBusinessRepository ?? new ScanBusinessRepository();
            //apiService = ApiService.Instance();
        }
    }
}
