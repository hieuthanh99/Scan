using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class UserAuthenticateServices
    {
        //private readonly WrapperRepo repo;

        //public UserAuthenticateServices()
        //{
        //    repo = repo ?? new WrapperRepo();
        //}

        //public UserAuthenticate GetByUserName(string username)
        //{
        //    return repo.UserAuthenticateRepo.GetByUserName(username);
        //}

        //public UserAuthenticate InsertToken(UserInfo userInfo)
        //{
        //    // clear table before insert
        //    repo.UserAuthenticateRepo.DeleteUserAuthenticate();

        //    var entity = new UserAuthenticate
        //    {
        //        UserName = userInfo.UserName,
        //        AccessToken = userInfo.Token,
        //        RefreshToken = userInfo.RefreshToken,
        //        ExpAccessToken = userInfo.ExpAccessToken,
        //        ExpRefreshToken = userInfo.ExpRefreshToken,
        //        CreatedTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        //    };

        //    return repo.UserAuthenticateRepo.InsertToken(entity);
        //}

        //public int UpdateToken(UserInfo userInfo)
        //{
        //    return repo.UserAuthenticateRepo.UpdateToken(userInfo);
        //}

        //public int DeleteUserAuthenticate()
        //{
        //    return repo.UserAuthenticateRepo.DeleteUserAuthenticate();
        //}
    }
}
