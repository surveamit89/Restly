using RestSharp.Portable.HttpClient.Impl;
using System;
using System.Net.Http;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using MvvmCross;
using Restly.Helper.HelperInterface;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Restly.Models.ApiRequestResponse;
using Restly.Utility;
using ModernHttpClient;

namespace Restly.Services
{
    public static class BaseWebService
    {
        public static string AppBaseUrl = "https://restly.deventure.ro/api/";
        #region Available Constructors
        public static RestClient _restClient = new RestClient(AppBaseUrl)
        {
            IgnoreResponseStatusCode = true,
            Timeout = TimeSpan.FromMinutes(1)
        };
        #endregion

        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            return client;
        }

        #region Execute Requests
        public static async Task<T> ExecuteGet<T>(RestRequest request) where T : new()
        {
            request.Method = Method.GET;
            //request.AddParameter("language_code", Mvx.IoCProvider.Resolve<IPersistData>().GetLanguageCode(), ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader);
           
            if (AppUtility.IsConnectedToInternet())
            {
                try
                {
                    PrintRequest(request);
                    var response = await _restClient.Execute(request);

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            {
                                JsonSerializerSettings settings = new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                };
                                PrintResponce(response.Content);
                                var result = JsonConvert.DeserializeObject<BaseResponse>(response.Content, settings);
                                if (result.Code == 401)
                                {
                                    UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                                }
                                return JsonConvert.DeserializeObject<T>(response.Content, settings);
                            }

                        case HttpStatusCode.Gone:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                            }
                            break;

                        case HttpStatusCode.Unauthorized:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + "");
                               
                                //Mvx.IoCProvider.Resolve<IPersistData>().SetIsUserLogin(false);
                                //var messege = Mvx.IoCProvider.Resolve<IMvxMessenger>();
                                //messege.Publish(new LoginMessage(new ViewModels.BaseViewModel()));
                            }
                            break;

                        case HttpStatusCode.InternalServerError:
                            {;
                                UserDialogs.Instance.Alert(response.StatusDescription);// response.StatusCode);
                            }
                            break;

                        case HttpStatusCode.BadRequest:
                            {
                                return JsonConvert.DeserializeObject<T>(response.Content);
                            }
                            break;

                        case HttpStatusCode.NotFound:
                            {
                                //TODO: Need to handle
                            }
                            break;
                        case HttpStatusCode.RequestTimeout:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                            }
                            break;

                    }
                }
                catch (WebException ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, AppResources.Lbl_OK);
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, AppResources.Lbl_OK);
                }
            }
            else
            {

                UserDialogs.Instance.Alert(AppResources.Lbl_NoInternet, null, AppResources.Lbl_OK);
            }
            return default(T);
        }

        public static async Task<T> ExecutePost<T>(RestRequest request) where T : class, new()
        {
            request.Method = Method.POST;
            request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader);
           

            if (AppUtility.IsConnectedToInternet())
            {
                try
                {
                    PrintRequest(request);
                    var response = await _restClient.Execute(request);
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            {
                                JsonSerializerSettings settings = new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                };
                                PrintResponce(response.Content);
                                var result = JsonConvert.DeserializeObject<BaseResponse>(response.Content, settings);
                                if (result.Code == 401)
                                {
                                    UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                                }
                                return JsonConvert.DeserializeObject<T>(response.Content, settings);
                            }

                        case HttpStatusCode.Gone:
                            {
                                UserDialogs.Instance.Alert(response.StatusDescription + " " + response.StatusCode);
                            }
                            break;

                        case HttpStatusCode.Unauthorized:
                            {
                                //Mvx.IoCProvider.Resolve<IPersistData>().SetIsUserLogin(false);
                                //var messege = Mvx.IoCProvider.Resolve<IMvxMessenger>();
                                //messege.Publish(new LoginMessage(new ViewModels.BaseViewModel()));
                            }
                            break;

                        case HttpStatusCode.InternalServerError:
                            {
                                //var res = await DashboardService.ProcessToLogOut();
                                UserDialogs.Instance.Alert(response.StatusDescription);// response.StatusCode);
                            }
                            break;

                        case HttpStatusCode.BadRequest:
                            {
                                return JsonConvert.DeserializeObject<T>(response.Content);
                            }

                        case HttpStatusCode.NotFound:
                            {
                                UserDialogs.Instance.Alert(response.Content, null, AppResources.Lbl_OK);
                            }
                            break;
                        case HttpStatusCode.RequestTimeout:
                            {
                                UserDialogs.Instance.Alert(AppResources.Err_ServiceRequestTimeOut + " " + response.StatusCode);
                            }
                            break;
                    }
                }
                catch (WebException ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, AppResources.Lbl_OK);
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, AppResources.Lbl_OK);
                }
            }
            else
            {
                UserDialogs.Instance.Alert(AppResources.Lbl_NoInternet, null, AppResources.Lbl_OK);
            }

            return null;
        }

        public static void PrintRequest(RestRequest request)
        {
            var logger = Mvx.IoCProvider.Resolve<IAppLogger>();
            var sb = new StringBuilder();
            foreach (var param in request.Parameters)
            {
                if (param.Type == ParameterType.RequestBody)
                {
                    var valueBytes = (byte[])param.Value;
                    var body = System.Text.Encoding.UTF8.GetString(valueBytes, 0, valueBytes.Length);

                    logger.DebugLog(typeof(BaseWebService).Name, "body:" + body);
                }
                else
                {
                    sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
                }
            }
            logger.DebugLog(typeof(BaseWebService).Name, string.Format("\n\n Request: {0} \n {1}", request.Resource, sb.ToString()));
        }
        private static void PrintResponce(string content)
        {
            var logger = Mvx.IoCProvider.Resolve<IAppLogger>();
            var sb = new StringBuilder();
            logger.DebugLog(typeof(BaseWebService).Name, string.Format("\n\n Request: {0} \n {1}", content, ""));

        }
        #endregion
    }
    public class BVClientFactory : DefaultHttpClientFactory
    {
        protected override HttpMessageHandler CreateMessageHandler(IRestClient client)
        {
            return new NativeMessageHandler(false, false);
            //return new NativeMessageHandler();
        }
    }
}
