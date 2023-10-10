using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Helpers
{
    public class SystemHelper
    {
        public static Tuple<string, long> AvailableFreeSpace()
        {
            FileInfo file = new FileInfo(Environment.CurrentDirectory);
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);

            return new Tuple<string, long>(drive.Name, drive.AvailableFreeSpace);
        }

        public static string CalculateMD5(string filename)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
                }
            }
        }

        public static string CalculateMD5(byte[] buffer)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(buffer);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }
    }
}
