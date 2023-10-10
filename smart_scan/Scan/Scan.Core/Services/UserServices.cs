using log4net;
using Newtonsoft.Json;
using RestSharp;
using Scan.Core.DTO;
using Scan.Core.Entities;
using Scan.Core.Repositories;
using Scan.Core.Settings;
using Scan.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class UserServices
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserServices));

        private readonly string restBaseUrl = AppConfig.AuthAPI;
        private readonly string tokenName = ConfigurationManager.AppSettings["TokenNameAuthApi"].ToString();
        private readonly WrapperRepo repo;
        private readonly HttpAuthenServices httpServices;
        public UserServices()
        {
            repo = repo ?? new WrapperRepo();
            httpServices = HttpAuthenServices.Instance();
        }

#pragma warning disable S3776
        public async Task<UserInfo> LoginSSO(string userName, string password)
        {
            try
            {
                string clientID = AppConfig.ClientId;
                string clientSecret = AppConfig.ClientSecret;
                string grantType = AppConfig.GrantType;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("grant_type", grantType);
                param.Add("username", userName);
                param.Add("password", password);
                param.Add("client_id", clientID);
                param.Add("client_secret", clientSecret);

                var result = await httpServices.HttpExecuteNoToken<dynamic>($"auth/realms/{AppConfig.RealmName}/protocol/openid-connect/token", Method.POST, null, param);

                // test SS
                //var result = await httpServices.HttpExecuteNoToken<dynamic>($"realms/{AppConfig.RealmName}/protocol/openid-connect/token", Method.POST, null, param);
                if (result != null && result["access_token"] != null)
                {
                    var jwtHandle = new JwtSecurityTokenHandler();
                    var decodedValueAccessToken = jwtHandle.ReadJwtToken((string)result["access_token"]);

                    var user = new UserInfo();
                    user.Token = result["access_token"];
                    user.RefreshToken = result["refresh_token"];
                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "exp") != null)
                    {
                        user.ExpAccessToken = int.Parse(decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "exp").Value);
                    }
                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "email") != null)
                    {
                        user.Email = decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "email").Value;
                    }
                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "user_fullname") != null)
                    {
                        var fullname = decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "user_fullname").Value;
                        if (string.IsNullOrWhiteSpace(fullname))
                        {
                            fullname = userName;
                        }
                        user.FullName = fullname;
                    }
                    else
                    {
                        user.FullName = userName;
                    }
                    user.Address = "";
                    user.LoginTime = DateTime.Now.ToString(AppConfig.dateFormat);
                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "preferred_username") != null)
                    {
                        user.UserName = decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value;
                    }

                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "resource_access") != null)
                    {
                        var resourceAccess = decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "resource_access").Value;
                        user.Roles = BuildRole(resourceAccess);
                    }
                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "groups") != null)
                    {
                        var groups = decodedValueAccessToken.Claims.Where(x => x.Type == "groups").ToList();
                        user.Groups = BuildGroup(groups);
                    }

                    var decodedValueRefreshToken = jwtHandle.ReadJwtToken((string)result["refresh_token"]);
                    if (decodedValueRefreshToken.Claims.FirstOrDefault(x => x.Type == "exp") != null)
                    {
                        user.ExpRefreshToken = int.Parse(decodedValueRefreshToken.Claims.FirstOrDefault(x => x.Type == "exp").Value);
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }
#pragma warning restore S3776

        //public async Task refreshToken()
        //{
        //    try
        //    {
        //        string clientID = AppConfig.ClientId;

        //        Dictionary<string, object> param = new Dictionary<string, object>();
        //        param.Add("client_id", clientID);
        //        param.Add("refresh_token", IoC.Get<ApplicationViewModel>().CurrentUser.RefreshToken);
        //        param.Add("grant_type", "refresh_token");

        //        var result = await httpServices.HttpExecute<dynamic>($"auth/realms/{AppConfig.RealmName}/protocol/openid-connect/token", Method.POST, null, param);
        //        if (result != null && result["access_token"] != null)
        //        {
        //            var jwtHandle = new JwtSecurityTokenHandler();
        //            var decodedValueAccessToken = jwtHandle.ReadJwtToken((string)result["access_token"]);

        //            IoC.Get<ApplicationViewModel>().CurrentUser.Token = result["access_token"];
        //            IoC.Get<ApplicationViewModel>().CurrentUser.RefreshToken = result["refresh_token"];
        //            if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "exp") != null)
        //            {
        //                var expAccessToken = int.Parse(decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "exp").Value);
        //                IoC.Get<ApplicationViewModel>().CurrentUser.ExpAccessToken = expAccessToken;
        //            }
        //            var decodedValueRefressToken = jwtHandle.ReadJwtToken((string)result["refresh_token"]);
        //            if (decodedValueRefressToken.Claims.FirstOrDefault(x => x.Type == "exp") != null)
        //            {
        //                var expRefreshToken = int.Parse(decodedValueRefressToken.Claims.FirstOrDefault(x => x.Type == "exp").Value);
        //                IoC.Get<ApplicationViewModel>().CurrentUser.ExpRefreshToken = expRefreshToken;
        //            }

        //            var user = IoC.Get<ApplicationViewModel>().CurrentUser;
        //            ////logger.Info($"====> AFTER: ExpAccessToken: {user.ExpAccessToken}, access_token: {user.Token.Substring(user.Token.Length - 10)}");
        //            repo.UserAuthenticateRepo.UpdateToken(user);

        //            HttpServices.Instance();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex);
        //    }
        //}

        private List<Role> BuildRole(string resourceAccess)
        {
            try
            {
                dynamic _resource = JsonConvert.DeserializeObject(resourceAccess);
                dynamic _dRoles = _resource[AppConfig.ClientId];

                List<Role> roles = new List<Role>();
                foreach (var item in _dRoles["roles"])
                {
                    roles.Add(new Role
                    {
                        RoleName = item.ToString()
                    });
                }
                return roles;
            }
            catch
            {
                return new List<Role>();
            }
        }

        private List<Group> BuildGroup(List<Claim> originalGroup)
        {
            try
            {
                List<Group> groups = new List<Group>();
                foreach (var item in originalGroup)
                {
                    var groupValue = item.Value;
                    if (groupValue != null)
                    {
                        var groupName = groupValue.Split('/').Last();
                        groups.Add(new Group
                        {
                            GroupName = groupName
                        });
                    }
                }

                return groups;
            }
            catch
            {
                return new List<Group>();
            }

        }

        //public int UpdatePIN(int id, string pin)
        //{
        //    return repo.User.UpdatePIN(id, pin);
        //}

    }
}
