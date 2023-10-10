using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Entities
{
    public class Version
    {
        public int Id { get; set; }
        public string VersionName { get; set; }
        public string VersionDate { get; set; }
        public int Status { get; set; }
        public string DBUpLastestTime { get; set; }
    }
}
