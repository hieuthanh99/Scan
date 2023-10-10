using Scan.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.Services.Users.User;

namespace Scan.Core.Services
{
    public class UserStateServices
    {
        private readonly WrapperRepo repo;

        public UserStateServices()
        {
            repo = repo ?? new WrapperRepo();
        }

        //public UserState GetSettingByUser(string userName)
        //{
        //    return repo.UserState.GetStateUser(userName);
        //}

        //public int SetStateUser(string userName, string stateData)
        //{
        //    return repo.UserState.SetStateUser(userName, stateData);
        //}
    }
}
