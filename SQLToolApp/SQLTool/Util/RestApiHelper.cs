﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SQLTool.Services
{
    public class RestApiHelper : IDisposable
    {
        string baseUrl;
        string prefixUrl;
        string bodyType;
        string bodyContent;
        string apiToken;
        RestService restService;
        HttpClient client;
        public RestApiHelper()
        {
            baseUrl = "";
            prefixUrl = "";
            bodyType = "application/json";
            restService = new RestService();
        }
        public RestApiHelper(string _baseUrl, string _prefixUrl)
        {
            baseUrl = _baseUrl;
            prefixUrl = _prefixUrl;
            bodyType = "application/json";
            restService = new RestService();
        }
        private void InitHttpClient()
        {
            if(client == null)
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
            }
        }
        public void Dispose()
        {
            if(restService != null)
            {
                restService.Dispose();
            }
        }
        public RestApiHelper AddRequestHeader(string key, string value)
        {
            restService.AddHeader(key, value);
            return this;
        }
        public RestApiHelper SetToken(RestService.Scheme scheme, string token)
        {
            restService.SetToken(RestService.Scheme.Bearer, token);
            return this;
        }
        public string GetToken()
        {
            return restService.GetToken();
        }
        private void SetParameters(IEnumerable<KeyValuePair<string, object>> pairs)
        {
            JObject obj = new JObject();
            pairs.ToList().ForEach(x => obj.Add(x.Key, new JValue(x.Value)));
            bodyContent = obj.ToString();
        }
        private string SetUrl(string url)
        {
            return string.Join("/", baseUrl, prefixUrl, url);
        }
        private Task<ApiResult> ProgressAsync()
        {
            return Task.Run<ApiResult>(async delegate
            {
                var res = await restService.Run<ApiResult>();
                ApiResult result = res.Target;
                if (res.Errors.Count > 0)
                {
                    result = new ApiResult();
                    result.message = res.Errors[0].Exception.Message;
                }
                result.statusCode = restService.StatusCode;
                return result;
            });
        }
        public RestApiHelper AddDefaultHeader()
        {
            AddRequestHeader("Accept", "application/json");
            if(!string.IsNullOrEmpty(apiToken))
            {
                restService.SetToken(RestService.Scheme.Bearer, apiToken);
            }
            return this;
        }
        public RestApiHelper AddDefaultClientHeader()
        {
            InitHttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            if (!string.IsNullOrEmpty(apiToken))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(RestService.Scheme.Bearer.ToString(), apiToken);
            }
            return this;
        }
        public Task<ApiResult> GetAsync(string url)
        {
            restService.Url = SetUrl(url);
            restService.Timeout = 30;
            restService.Method = HttpMethodType.Get.ToString().ToLower();
            return ProgressAsync();
        }
        public Task<ApiResult> PostAsync(string url, IEnumerable<KeyValuePair<string, object>> pairs)
        {
            SetParameters(pairs);
            restService.Url = SetUrl(url);
            restService.Body = bodyContent;
            restService.Method = HttpMethodType.Post.ToString().ToLower();
            return ProgressAsync();
        }
        public Task<ApiResult> PutAsync(string url, IEnumerable<KeyValuePair<string, object>> pairs)
        {
            SetParameters(pairs);
            restService.Url = SetUrl(url);
            restService.Body = bodyContent;
            restService.Method = HttpMethodType.Put.ToString().ToLower();
            return ProgressAsync();
        }
        public Task<ApiResult> DeleteAsync(string url, IEnumerable<KeyValuePair<string, object>> pairs)
        {
            SetParameters(pairs);
            restService.Url = SetUrl(url);
            restService.Body = bodyContent;
            restService.Method = HttpMethodType.Delete.ToString().ToLower();
            return ProgressAsync();
        }
        private async Task<ApiResult> ProgressClientAsync(HttpResponseMessage response)
        {
            ApiResult apiResult = null;
            try
            {
                string result = await response.Content.ReadAsStringAsync();
                if(!string.IsNullOrEmpty(result))
                {
                    apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult>(result);
                    apiResult.statusCode = response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult();
                apiResult.statusCode = response.StatusCode;
                apiResult.success = false;
                apiResult.message = ex.Message;
            }
            return apiResult;
        }
        public RestApiHelper SetClientTimeOut(double timeout)
        {
            this.TimeOut = timeout;
            return this;
        }
        private Task<ApiResult> GetClientAsync(string url)
        {
            return Task.Run<ApiResult>(async delegate
            {
                ApiResult result = null;
                try
                {
                    System.Threading.CancellationTokenSource source = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(TimeOut));
                    HttpResponseMessage message = await client.GetAsync(SetUrl(url), source.Token);
                    if (message.IsSuccessStatusCode)
                    {
                        result = await ProgressClientAsync(message);
                    }
                    return result;
                }
                catch (AggregateException ae)
                {
                    foreach (Exception e in ae.InnerExceptions)
                    {
                        if (e is TaskCanceledException)
                            Console.WriteLine("Unable to compute mean: {0}",
                                              ((TaskCanceledException)e).Message);
                        else
                            Console.WriteLine("Exception: " + e.GetType().Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            });
        }
        public Task<ApiResult> Run(string url, HttpMethodType methodType, IEnumerable<KeyValuePair<string, object>> pairs = null)
        {
            MethodInfo method = this.GetType().GetMethod(string.Format("{0}Async", methodType.ToString()), ((BindingFlags)BindingFlags.NonPublic) | (((BindingFlags)BindingFlags.Public) | ((BindingFlags)BindingFlags.Instance)));
            if (method != null)
            {
                List<object> param = new List<object> { url };
                if (method.GetParameters().Length > 1)
                    param.Add(pairs);
                return (Task<ApiResult>)method.Invoke(this, param.ToArray());
            }
            return null;
        }
        public Task<ApiResult> RunClient(string url, HttpMethodType methodType, IEnumerable<KeyValuePair<string, object>> pairs = null)
        {
            InitHttpClient();
            MethodInfo method = this.GetType().GetMethod(string.Format("{0}ClientAsync", methodType.ToString()), ((BindingFlags)BindingFlags.NonPublic) | (((BindingFlags)BindingFlags.Public) | ((BindingFlags)BindingFlags.Instance)));
            if (method != null)
            {
                List<object> param = new List<object> { url };
                if (method.GetParameters().Length > 1)
                    param.Add(pairs);
                return (Task<ApiResult>)method.Invoke(this, param.ToArray());
            }
            return null;
        }

        public double TimeOut { private get; set; }
    }
    public class ApiResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public JObject validation { get; set; }
        public JObject data { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
    public static class JObjectExtra
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IList<TResult> GetListValue<TResult>(this JObject obj)
        {
            return obj.ToObject<IList<TResult>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TResult GetDataValue<TResult>(this JObject obj)
        {
            return obj.ToObject<TResult>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IList<TResult> GetListValue<TResult>(this JObject obj, string key)
        {
            return obj.GetValue(key).ToObject<IList<TResult>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TResult GetDataValue<TResult>(this JObject obj, string key)
        {
            return obj.GetValue(key).ToObject<TResult>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDataValue(this JObject obj, string key)
        {
            return GetDataValue<string>(obj, key);
        }
    }
    public enum HttpMethodType
    {
        Get,
        Post,
        Put,
        Delete
    }
}