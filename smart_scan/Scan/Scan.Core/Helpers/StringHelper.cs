using log4net;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scan.Core.Helpers
{
    public class StringHelper
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(StringHelper));

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None);

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();
                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[A-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[A-Z0-9.-]+$", RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidMBBankEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None);

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();
                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[A-Z0-9_!#$%&'*+/=?`{|}~^.-]+@mbbank\.com\.vn+$", RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static string SafeTrim(string value)
        {
            return string.IsNullOrEmpty(value) ? value : value.Trim();
        }

        public static double? ConvertToDouble(string s)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return null;
                }

                NumberFormatInfo provider = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ","
                };

                return Convert.ToDouble(s.Replace(" ", "").Trim(), provider);
            }
            catch (Exception) { }

            return null;
        }

        public static string ConvertDoubleToString(double? d)
        {
            try
            {
                if (d == null)
                {
                    return null;
                }

                NumberFormatInfo provider = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ","
                };

                var result = Convert.ToString(d, provider);

                return string.Format("{0:#,0.#############################}", d);
            }
            catch (Exception) { }

            return null;
        }

        public static string LimitByteLength(string input, int maxLength)
        {
            return new string(input
                .TakeWhile((c, i) => Encoding.UTF8.GetByteCount(input.Substring(0, i + 1)) <= maxLength)
                .ToArray());
        }

        public static FileType ConvertFileType(string imgType)
        {
            if (!Enum.TryParse(imgType, true, out FileType fileType))
            {
                logger.Error($"Can't convert enum: {imgType}");
                return FileType.hd_dichvu_none;
            }

            return fileType;
        }
    }
}
