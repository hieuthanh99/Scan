using log4net;
using Newtonsoft.Json;
using RestSharp;
using Scan.App;
using Scan.Core.DTO;
using Scan.Core.Repositories;
using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core.Services
{
    public class ApiService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ApiService));

        private static readonly int API_TIMEOUT = 30000;
        public static readonly string ApiBaseUrl = AppConfig.CaptureApiVersion2;

        private static ApiService _authServices { get; set; }
        public static ApiService Instance()
        {
            if (_authServices == null)
            {
                _authServices = new ApiService();
            }

            return _authServices;
        }

        public async Task<DateTimeOffset?> GetCurrentTime()
        {
            try
            {
                var url = "/sscan-rs/v2.0/ws-utils/current-time";
                var request = new RestRequest(url, Method.GET);
                var client = new RestClient(ApiBaseUrl)
                {
                    Timeout = 5000
                };
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponseApi<string> responseContent;
                    try
                    {
                        var responseV2 = JsonConvert.DeserializeObject<ResponseApiV2<string>>(response.Content);
                        responseContent = (responseV2.status != 200 || responseV2.data.result != null) ? new ResponseApi<string>(responseV2) : JsonConvert.DeserializeObject<ResponseApi<string>>(response.Content);
                    }
                    catch
                    {
                        responseContent = JsonConvert.DeserializeObject<ResponseApi<string>>(response.Content);
                    }

                    return DateTimeOffset.Parse(responseContent.data, CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return null;
        }

        public async Task<ResponseApi<T>> HttpExecute<T>(string url, Method method = Method.GET, object body = null, Dictionary<string, object> parameters = null) where T : class
        {
            try
            {
                var token = (IoC.Get<ApplicationViewModel>().CurrentUser ?? new UserInfo()).Token;
                var request = _createRequest(url, method, body, parameters);
                if (!string.IsNullOrEmpty(token))
                {
                    request.AddHeader("Authorization", "Bearer " + token);
                }

                var client = new RestClient(ApiBaseUrl)
                {
                    Timeout = API_TIMEOUT
                };
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode != HttpStatusCode.Unauthorized)
                {
                    ResponseApi<T> responseData;
                    try
                    {
                        var responseV2 = JsonConvert.DeserializeObject<ResponseApiV2<T>>(response.Content);
                        if (responseV2 == null)
                        {
                            return null;
                        }
                        responseData = (responseV2.status != 200 || responseV2.data.result != null) ? new ResponseApi<T>(responseV2) : JsonConvert.DeserializeObject<ResponseApi<T>>(response.Content);
                    }
                    catch
                    {
                        responseData = JsonConvert.DeserializeObject<ResponseApi<T>>(response.Content);
                    }

                    return responseData;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(token))
                {
                    logger.Info("Call refresh token");
                    var accessToken = await _refreshToken();
                    if (accessToken != null)
                    {
                        var recallRequest = _createRequest(url, method, body, parameters);
                        recallRequest.AddHeader("Authorization", "Bearer " + accessToken);

                        IRestResponse recallResponse = await client.ExecuteAsync(recallRequest);
                        if (recallResponse.StatusCode == HttpStatusCode.OK)
                        {
                            ResponseApi<T> responseData;
                            try
                            {
                                var responseV2 = JsonConvert.DeserializeObject<ResponseApiV2<T>>(response.Content);
                                responseData = (responseV2.status != 200 || responseV2.data.result != null) ? new ResponseApi<T>(responseV2) : JsonConvert.DeserializeObject<ResponseApi<T>>(response.Content);
                            }
                            catch
                            {
                                responseData = JsonConvert.DeserializeObject<ResponseApi<T>>(response.Content);
                            }

                            return responseData;
                        }
                    }
                }

                return new ResponseApi<T>()
                {
                    status = (int)response.StatusCode,
                    error = response.ErrorMessage
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

        private async Task<string> _refreshToken()
        {
            var token = (IoC.Get<ApplicationViewModel>().CurrentUser ?? new UserInfo()).Token;
            var request = new RestRequest($"auth/realms/{AppConfig.RealmName}/protocol/openid-connect/token", Method.POST);

            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("client_id", AppConfig.ClientId, ParameterType.GetOrPost);
            request.AddParameter("refresh_token", IoC.Get<ApplicationViewModel>().CurrentUser.RefreshToken, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "refresh_token", ParameterType.GetOrPost);

            var client = new RestClient(AppConfig.AuthAPI)
            {
                Timeout = 10000
            };
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
                   // _userAuthenticateRepo.UpdateToken(user);

                    return result["access_token"];
                }
            }

            return null;
        }

        private RestRequest _createRequest(string url, Method method = Method.GET, object body = null, Dictionary<string, object> parameters = null)
        {
            var request = new RestRequest(url, method);

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    if ("file".Equals(item.Key.ToLower()) || "thumbnail".Equals(item.Key.ToLower()))
                    {
                        request.AddFile(item.Key.ToLower(), item.Value.ToString());
                    }
                    else
                    {
                        request.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
                    }
                }
            }

            if (body != null)
            {
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }

            return request;
        }
    }
}
