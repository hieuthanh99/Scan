using log4net;
using Scan.App;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Settings
{
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public class TempData : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(TempData));

        static TempData instance;
        TempData()
        {
            TempPath = Environment.CurrentDirectory;
        }

        public void CleanAll()
        {
            foreach (string d in Directory.GetDirectories(AppTempFolder))
                if (d != CurrentJobFolder)
                    try
                    {
                        Directory.Delete(d, true);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
        }

        public static TempData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TempData();
                    instance.CreateJobFolder();
                }
                return instance;
            }
        }

        private string TempPath { get; set; }
        public string CurrentJobFolder { get; set; }

        public string CurrentDirectory { get; private set; } = Environment.CurrentDirectory;

        private string AppTempFolder
        {
            get
            {
                DateTime currentTime = DateTime.Now;
                string currentYear = currentTime.ToString("yyyy");
                string currentMonth = currentTime.ToString("MM");
                string currentDay = currentTime.ToString("dd");

                string username = IoC.Get<ApplicationViewModel>().CurrentUser == null ? "" : IoC.Get<ApplicationViewModel>().CurrentUser.UserName;

                return Path.Combine(TempPath, $@"{currentYear}\{currentMonth}\{currentDay}\{username}");
            }
        }

        private void CreateJobFolder()
        {
            if (!Directory.Exists(AppTempFolder))
                Directory.CreateDirectory(AppTempFolder);

            string pathByTime = DateTime.Now.ToString("yyyyMMddHHmm");

            CurrentJobFolder = Path.Combine(AppTempFolder, pathByTime);

            if (!Directory.Exists(CurrentJobFolder))
                Directory.CreateDirectory(CurrentJobFolder);
        }

        public string CreateBatchFolder(string subFolder = "")
        {
            if (!Directory.Exists(AppTempFolder))
                Directory.CreateDirectory(AppTempFolder);

            string pathByTime = DateTime.Now.ToString("yyyyMMddHHmm");
            string fullFolder = Path.Combine(AppTempFolder, pathByTime, subFolder);

            if (!Directory.Exists(fullFolder))
                Directory.CreateDirectory(fullFolder);

            return fullFolder;
        }

        public string CreateScanNoLoginTempFolder(string subFolder = "")
        {
            string noLoginTempFolder = Path.Combine(TempPath, AppConfig.ScanNoLoginTemp);
            if (!Directory.Exists(noLoginTempFolder))
                Directory.CreateDirectory(noLoginTempFolder);

            string pathByTime = DateTime.Now.ToString("yyyyMMddHHmm");

            string fullFolder = Path.Combine(noLoginTempFolder, pathByTime, subFolder);
            if (!Directory.Exists(fullFolder))
                Directory.CreateDirectory(fullFolder);

            return fullFolder;
        }

        public void CleanupScanNoLoginFolder()
        {
            string noLoginTempFolder = Path.Combine(TempPath, AppConfig.ScanNoLoginTemp);
            if (Directory.Exists(noLoginTempFolder))
                Directory.Delete(noLoginTempFolder, true);
        }

        public string CreateFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            return folderName;
        }

        public string CreateTempFile(string ExtensionWithDot, string prefixName = null)
        {
            string newFile = Path.Combine(CurrentJobFolder,
                (prefixName != null ? prefixName + "." : "") + Path.GetRandomFileName() + ExtensionWithDot);
            return newFile;
        }

        public void ExtractZipFileToDirectory(string sourceZipFilePath, string destinationDirectoryName, bool overwrite)
        {
            using (var archive = ZipFile.Open(sourceZipFilePath, ZipArchiveMode.Read))
            {
                if (!overwrite)
                {
                    archive.ExtractToDirectory(destinationDirectoryName);
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
                string destinationDirectoryFullPath = di.FullName;

                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                    if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                    }

                    if (file.Name == "")
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                        continue;
                    }
                    file.ExtractToFile(completeFileName, true);
                }
            }
        }


        public bool CheckCreateFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                try
                {
                    Directory.CreateDirectory(folderName);
                }
                catch (Exception ex)
                {
                    logger.Error($"An error occurred while creating the directory: {folderName}. Detail: {ex.Message}");
                    return false;
                }
            }
            return true;
        }

        public bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                using (FileStream fs = File.Create(Path.Combine(folderPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Cleanup(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{file}: {ex}");
                ////logger.Error($"Error delete file: {file}", ex);
            }
        }

        public void Cleanup()
        {
            Directory.Delete(CurrentJobFolder, true);
        }


        public void DeleteEmptyDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                DeleteEmptyDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        // version 2
        private string ScanTempFolder
        {
            get
            {
                string username = IoC.Get<ApplicationViewModel>().CurrentUser == null ? "" : IoC.Get<ApplicationViewModel>().CurrentUser.UserName;

                return Path.Combine(TempPath, "scandata", username);
            }
        }

        public string CreateScanBatchFolder(string businessCode, string batchId, string subFolder)
        {
            if (!Directory.Exists(ScanTempFolder))
                Directory.CreateDirectory(ScanTempFolder);

            string fullFolder = Path.Combine(ScanTempFolder, businessCode, batchId, subFolder);
            if (!Directory.Exists(fullFolder))
                Directory.CreateDirectory(fullFolder);

            return fullFolder;
        }

        public string GetCaptureScanFolder(string businessCode, string batchId)
        {
            return Path.Combine(ScanTempFolder, businessCode, batchId);
        }

        // save scan business icon
        private string AppThemeFolder
        {
            get
            {
                return Path.Combine(TempPath, "themes");
            }
        }

        public string GetAppThemeIconFolder()
        {
            if (!Directory.Exists(AppThemeFolder))
                Directory.CreateDirectory(AppThemeFolder);

            string iconFolder = Path.Combine(AppThemeFolder, "icons");
            if (!Directory.Exists(iconFolder))
                Directory.CreateDirectory(iconFolder);

            return iconFolder;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Directory.Delete(CurrentJobFolder, true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
