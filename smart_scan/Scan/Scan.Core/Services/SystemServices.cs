using log4net;
using RestSharp;
using Scan.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class SystemServices
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(SystemServices));

        private readonly HttpServices httpServices;
        private readonly ApiService apiService;
        private readonly WrapperRepo repo;
        public SystemServices()
        {
            httpServices = httpServices ?? new HttpServices();
            apiService = ApiService.Instance();
            repo = repo ?? new WrapperRepo();
        }

        public async Task<DateTimeOffset> GetCurrentTime()
        {
            try
            {
                var result = await apiService.GetCurrentTime();
                if (result == null)
                {
                    return DateTimeOffset.Now;
                }

                return result.Value;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return DateTimeOffset.Now;
        }

        public int InsertNewVersion(string versionName, string versionDate, int status, string dBUpLastestTime)
        {
            try
            {
               // return repo.System.InsertNewVersion(versionName, versionDate, status, dBUpLastestTime);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return -1;
        }

        public void UpdateDBUpLastestTime(int id, string dBUpLastestTime)
        {
            try
            {
              //  repo.System.UpdateDBUpLastestTime(id, dBUpLastestTime);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public int DeleteLatestVersion()
        {
            try
            {
               // return repo.System.DeleteLatestVersion();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return -1;
        }

        public string GetLocalVersion()
        {
            try
            {
                //var versionData = null;
                //if (versionData != null)
                //{
                //    return versionData.VersionName;
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return string.Empty;
        }

        public Entities.Version GetCurrentVersion()
        {
            try
            {
              //  return repo.System.GetCurrentVersion();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

       

        public async Task<string> GetServerVersion()
        {
            try
            {
                var result = await httpServices.HttpExecuteNoToken<dynamic>($"/sscan-rs/v1.0/desktop/versions/lastest", Method.GET);
                if (result == null)
                {
                    return string.Empty;
                }
                var testData = result.GetValue("data");

                if (testData != null)
                {
                    return testData.ToObject<string>();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return string.Empty;
        }

        public async Task<(byte[], string)> downloadVersionApp()
        {
            try
            {
                var result = await httpServices.HttpDownloadUpdateApp<dynamic>($"/sscan-rs/v1.0/desktop/versions/lastest/contentWithChecksum");
                if (result == null)
                {
                    return (null, null);
                }
                var status = result.GetValue("status");
                var dataResult = result.GetValue("data");
                if ("200".Equals(Convert.ToString(status)) && dataResult != null)
                {
                    var checksum = dataResult["checksum"].ToObject<string>();
                    var content = dataResult["content"].ToObject<byte[]>();

                    return (content, checksum);
                }

                return (null, null);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return (null, null);
        }

        public async Task<bool> UploadVersion(string versionName, string signatureData, string versionFilePath)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "file-upload", versionFilePath },
                    { "tag", versionName },
                    { "signature", signatureData },
                    { "isActive", 1 }
                };
                var result = await httpServices.HttpExecute<dynamic>($"/sscan-rs/v1.0/desktop/versionApp", Method.POST, null, param);
                if (result == null)
                {
                    return false;
                }

                var status = result.GetValue("status");
                var dataResult = result.GetValue("data");

                return "200".Equals(Convert.ToString(status)) && dataResult != null;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return false;
        }
    }
}
