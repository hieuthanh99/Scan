using log4net;
using RestSharp;
using Scan.App;
using Scan.Core.Repositories;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class HttpServices
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(HttpServices));
        public string restBaseUrl { get; set; }

        public string restBaseUrlV2 { get; set; }
        public string tokenName { get; set; }
        public string tokenValue { get; set; }

        private static HttpServices _httpServices { get; set; }
        public static HttpServices Instance()
        {
            if (_httpServices == null)
            {
                _httpServices = new HttpServices();
            }

            _httpServices.restBaseUrl = AppConfig.CaptureAPI;
            _httpServices.restBaseUrlV2 = AppConfig.CaptureApiVersion2;
            _httpServices.tokenName = ConfigurationManager.AppSettings["TokenName"].ToString();
            _httpServices.tokenValue = (IoC.Get<ApplicationViewModel>().CurrentUser ?? new UserInfo()).Token;

            return _httpServices;
        }

        public HttpServices()
        {
            restBaseUrl = AppConfig.CaptureAPI;
            restBaseUrlV2 = AppConfig.CaptureApiVersion2;
            tokenName = ConfigurationManager.AppSettings["TokenName"].ToString();
            tokenValue = (IoC.Get<ApplicationViewModel>().CurrentUser ?? new UserInfo()).Token;
        }

        public async Task<T> HttpExecute<T>(string url, Method method = Method.GET, object obj = null, Dictionary<string, object> param = null) where T : class
        {
            try
            {
                var client = new RestClient(restBaseUrl)
                {
                    Timeout = 30000
                };

                var request = CreateRequest(url, method, obj, param);
                request.AddHeader("Authorization", "Bearer " + tokenValue);

                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (!url.StartsWith("auth/realms"))
                        {
                            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response.Content);
                            var status = result.GetValue("status");

                            if (!"200".Equals(Convert.ToString(status)) && !"201".Equals(Convert.ToString(status)) && !"204".Equals(Convert.ToString(status)))
                            {
                                logger.Info($"API result: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                                    $"Status Code: {Convert.ToString(status)}" + Environment.NewLine +
                                    $"ErrorMessage: {Convert.ToString(result.GetValue("error"))}");
                            }
                            return result;
                        }
                        else
                        {
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    logger.Info("Call refresh token");
                    var accessToken = await RefreshToken();
                    if (accessToken != null)
                    {
                        var recallRequest = CreateRequest(url, method, obj, param);
                        recallRequest.AddHeader("Authorization", "Bearer " + accessToken);
                        logger.Info($"Recall API after refresh token: {url}");

                        IRestResponse recallResponse = await client.ExecuteAsync(recallRequest);
                        if (recallResponse.StatusCode == HttpStatusCode.OK || recallResponse.StatusCode == HttpStatusCode.Created)
                        {
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(recallResponse.Content);
                        }
                        else
                        {
                            logger.Info($"Recall API error: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                                $"Status Code: {(int)recallResponse.StatusCode}" + Environment.NewLine +
                                $"ErrorMessage: {recallResponse.ErrorMessage}" + Environment.NewLine +
                                $"Content: {recallResponse.Content}");
                        }
                    }
                }
                else
                {
                    logger.Info($"Call API error: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                        $"Status Code: {(int)response.StatusCode}" + Environment.NewLine +
                        $"ErrorMessage: {response.ErrorMessage}" + Environment.NewLine +
                        $"Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

        public async Task<T> HttpExecuteV2<T>(string url, Method method = Method.GET, object obj = null, Dictionary<string, object> param = null) where T : class
        {
            try
            {
                var client = new RestClient(restBaseUrlV2)
                {
                    Timeout = 30000
                };

                var request = CreateRequest(url, method, obj, param);
                request.AddHeader("Authorization", "Bearer " + tokenValue);

                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (!url.StartsWith("auth/realms"))
                        {
                            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response.Content);
                            var status = result.GetValue("status");

                            if (!"200".Equals(Convert.ToString(status)) && !"201".Equals(Convert.ToString(status)) && !"204".Equals(Convert.ToString(status)))
                            {
                                logger.Info($"API result: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                                    $"Status Code: {Convert.ToString(status)}" + Environment.NewLine +
                                    $"ErrorMessage: {Convert.ToString(result.GetValue("error"))}");
                            }
                            return result;
                        }
                        else
                        {
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    logger.Info("Call refresh token");
                    var accessToken = await RefreshToken();
                    if (accessToken != null)
                    {
                        var recallRequest = CreateRequest(url, method, obj, param);
                        recallRequest.AddHeader("Authorization", "Bearer " + accessToken);
                        logger.Info($"Recall API after refresh token: {url}");

                        IRestResponse recallResponse = await client.ExecuteAsync(recallRequest);
                        if (recallResponse.StatusCode == HttpStatusCode.OK || recallResponse.StatusCode == HttpStatusCode.Created)
                        {
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(recallResponse.Content);
                        }
                        else
                        {
                            logger.Info($"Recall API error: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                                $"Status Code: {(int)recallResponse.StatusCode}" + Environment.NewLine +
                                $"ErrorMessage: {recallResponse.ErrorMessage}" + Environment.NewLine +
                                $"Content: {recallResponse.Content}");
                        }
                    }
                }
                else
                {
                    logger.Info($"Call API error: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                        $"Status Code: {(int)response.StatusCode}" + Environment.NewLine +
                        $"ErrorMessage: {response.ErrorMessage}" + Environment.NewLine +
                        $"Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

        private RestRequest CreateRequest(string url, Method method = Method.GET, object obj = null, Dictionary<string, object> param = null)
        {
            var request = new RestRequest(url, method);

            if (param != null)
            {
                foreach (var item in param)
                {
                    if (new List<string>() { "images", "image" }.Contains(item.Key.ToLower()))
                    {
                        request.AddFile("files", item.Value.ToString());
                    }
                    else if ("file-upload".Equals(item.Key.ToLower()))
                    {
                        request.AddFile("file", item.Value.ToString());
                    }
                    else
                    {
                        request.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
                    }
                }
            }

            if (obj != null)
            {
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(obj);
            }

            return request;
        }

        private async Task<string> RefreshToken()
        {
            var client = new RestClient(restBaseUrl)
            {
                Timeout = 10000
            };

            var request = new RestRequest($"auth/realms/{AppConfig.RealmName}/protocol/openid-connect/token", Method.POST);

            request.AddHeader("Authorization", "Bearer " + tokenValue);
            request.AddParameter("client_id", AppConfig.ClientId, ParameterType.GetOrPost);
            request.AddParameter("refresh_token", IoC.Get<ApplicationViewModel>().CurrentUser.RefreshToken, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "refresh_token", ParameterType.GetOrPost);

            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response.Content);
                if (result != null && result["access_token"] != null)
                {
                    var jwtHandle = new JwtSecurityTokenHandler();
                    var decodedValueAccessToken = jwtHandle.ReadJwtToken((string)result["access_token"]);

                    IoC.Get<ApplicationViewModel>().CurrentUser.Token = result["access_token"];
                    IoC.Get<ApplicationViewModel>().CurrentUser.RefreshToken = result["refresh_token"];
                    if (decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "exp") != null)
                    {
                        var _expAccessToken = int.Parse(decodedValueAccessToken.Claims.FirstOrDefault(x => x.Type == "exp").Value);
                        IoC.Get<ApplicationViewModel>().CurrentUser.ExpAccessToken = _expAccessToken;
                    }
                    var decodedValueRefressToken = jwtHandle.ReadJwtToken((string)result["refresh_token"]);
                    if (decodedValueRefressToken.Claims.FirstOrDefault(x => x.Type == "exp") != null)
                    {
                        var _expRefreshToken = int.Parse(decodedValueRefressToken.Claims.FirstOrDefault(x => x.Type == "exp").Value);
                        IoC.Get<ApplicationViewModel>().CurrentUser.ExpRefreshToken = _expRefreshToken;
                    }

                    var _userAuthenticateRepo = new UserAuthenticateRepo();
                    var user = IoC.Get<ApplicationViewModel>().CurrentUser;
                 //   _userAuthenticateRepo.UpdateToken(user);

                    return result["access_token"];
                }
            }

            return null;
        }

        public async Task<T> HttpExecuteNoToken<T>(string url, Method method = Method.GET, object obj = null, Dictionary<string, object> param = null) where T : class
        {
            try
            {
                var client = new RestClient(restBaseUrl)
                {
                    Timeout = 30000
                };

                var request = new RestRequest(url, method);

                if (param != null)
                {
                    foreach (var item in param)
                    {
                        request.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
                    }
                }

                if (obj != null)
                {
                    request.AddHeader("Content-Type", "application/json");
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(obj);
                }

                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized
                         && $"auth/realms/{AppConfig.RealmName}/protocol/openid-connect/token".Equals(url))
                    {
                        logger.Info("Login fail: Incorrect username or password");
                    }
                    else
                    {
                        logger.Info($"Call API no token error: " + Environment.NewLine + $"URL ({method}): {url} " + Environment.NewLine +
                            $"Status Code: {(int)response.StatusCode}" + Environment.NewLine +
                            $"ErrorMessage: {response.ErrorMessage}" + Environment.NewLine +
                            $"Content: {response.Content}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

        public async Task<T> HttpDownloadUpdateApp<T>(string url) where T : class
        {
            try
            {
                var client = new RestClient(restBaseUrl)
                {
                    Timeout = 1800000
                };

                var request = new RestRequest(url, Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized
                         && $"auth/realms/{AppConfig.RealmName}/protocol/openid-connect/token".Equals(url))
                    {
                        logger.Info("Login fail: Incorrect username or password");
                    }
                    else
                    {
                        logger.Info($"Download update app error: " + Environment.NewLine + $"URL (GET): {url} " + Environment.NewLine +
                            $"Status Code: {(int)response.StatusCode}" + Environment.NewLine +
                            $"ErrorMessage: {response.ErrorMessage}" + Environment.NewLine +
                            $"Content: {response.Content}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }
    }
}
