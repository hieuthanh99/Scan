using Scan.Core.DTO;
using Scan.Core.Entities;
using Scan.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Repositories
{
    public class UserAuthenticateRepo
    {
        private readonly IDBHelper db;

        public UserAuthenticateRepo()
        {
            db = db ?? new DBHelper();
        }

        public UserAuthenticateRepo(IDBHelper dBHelper)
        {
            db = dBHelper;
        }
    }
}
