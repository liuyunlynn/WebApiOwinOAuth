using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestfulWebService.Infrastructure.Common;

namespace RestfulWebService.Infrastructure.Extensions
{
    public static class  HttpClientExtensions
    {
        public static async Task<HttpWebApiResponse<T>> ExtensionGetAsync<T>(this HttpClient client, string requestUri,
            IDictionary<string, object> parameters = null)
        {
            try
            {
                if (parameters != null && parameters.Any())
                {
                    var encodedQueryStrings = parameters.SelectMany(x => UrlEncode(x.Key, x.Value));
                    requestUri = string.Format("{0}?{1}", requestUri, string.Join("&", encodedQueryStrings));
                }

                var message = new HttpRequestMessage(HttpMethod.Get, requestUri);
               
                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new HttpWebApiResponse<T>
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                }
                var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<T>(responseResult);
                return new HttpWebApiResponse<T> {  ModelObject = model};
            }
            catch (Exception ex)
            {
                return new HttpWebApiResponse<T>
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public static async Task<HttpWebApiResponse> ExtensionPostAsJsonAsync(this HttpClient client, string requestUri, object value)
        {
            try
            {
                var jsonBody = JsonConvert.SerializeObject(value);
                var buffer = Encoding.UTF8.GetBytes(jsonBody);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var message = new HttpRequestMessage(HttpMethod.Post, requestUri);
                message.Content = byteContent;
                
                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    var response = new HttpWebApiResponse
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                    return response;
                }
               // var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return new HttpWebApiResponse() { ResponseStatus = ResponseStatuses.Success};
            }
            catch (Exception ex)
            {
                return new HttpWebApiResponse
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public static async Task<HttpWebApiResponse<T>> ExtensionPostAsJsonAsync<T>(this HttpClient client, string requestUri, object value)
        {
            try
            {
                var jsonBody = JsonConvert.SerializeObject(value);
                var buffer = Encoding.UTF8.GetBytes(jsonBody);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var message = new HttpRequestMessage(HttpMethod.Post, requestUri);
                message.Content = byteContent;

                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);//PostAsJsonAsync(requestUri, value).ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new HttpWebApiResponse<T>
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                }
                var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<T>(responseResult);
                return new HttpWebApiResponse<T> { ModelObject = model };
            }
            catch (Exception ex)
            {
                return new HttpWebApiResponse<T>
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public static async Task<HttpWebApiResponse<T>> ExtensionPostAsByteArrayAsync<T>(this HttpClient client, string requestUri, byte[] bytes)
        {
            try
            {
                var byteContent = new ByteArrayContent(bytes);
                var message = new HttpRequestMessage(HttpMethod.Post, requestUri);
                message.Content = byteContent;

                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);//PostAsJsonAsync(requestUri, value).ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new HttpWebApiResponse<T>
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                }
                var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<T>(responseResult);
                return new HttpWebApiResponse<T> { ModelObject = model };
            }
            catch (Exception ex)
            {
                return new HttpWebApiResponse<T>
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public static async Task<HttpWebApiResponse<T>> UploadFileAsync<T>(this HttpClient client,string requestUrl, Stream fileStream, string fileName, string fileFormName,
           Dictionary<string, string> values, CancellationToken token)
        {
            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri(requestUrl),
                    Method = HttpMethod.Post
                };
                var multiContent = new MultipartFormDataContent("boundary=----" + DateTime.Now.Ticks.ToString("x"));

                // add json content 
                var json = JsonConvert.SerializeObject(values);
                var buffer = Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                multiContent.Add(byteContent);

                //add stream content
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                multiContent.Add(new StreamContent(fileStream), fileFormName, fileName);
                requestMessage.Content = multiContent;
                var cancelSource = new CancellationTokenSource();
                var httpResponse = await client.SendAsync(requestMessage, cancelSource.Token);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new HttpWebApiResponse<T>
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                }
                //sucess
                var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<T>(responseResult);
                return new HttpWebApiResponse<T> { ModelObject = model };

            } 
            catch (Exception ex)
            {
                return new HttpWebApiResponse<T>
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public static async Task<HttpWebApiResponse<T>> ExtensionPutAsJsonAsync<T>(this HttpClient client, string requestUri, object value)
        {
            try
            {
                var jsonBody = JsonConvert.SerializeObject(value);
                var buffer = Encoding.UTF8.GetBytes(jsonBody);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var message = new HttpRequestMessage(HttpMethod.Put, requestUri);
                message.Content = byteContent;

                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);//PostAsJsonAsync(requestUri, value).ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new HttpWebApiResponse<T>
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                }
                var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var model = JsonConvert.DeserializeObject<T>(responseResult);
                return new HttpWebApiResponse<T> { ModelObject = model };
            }
            catch (Exception ex)
            {
                return new HttpWebApiResponse<T>
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public static async Task<HttpWebApiResponse<T>> ExtensionPostAsync<T>(this HttpClient client, string requestUri, object value)
        {
            try
            {
                var httpResponse = await client.PostAsync(requestUri, new FormUrlEncodedContent(value as List<KeyValuePair<string, string>>));

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new HttpWebApiResponse<T>
                    {
                        ResponseStatus = ResponseStatuses.Failed,
                        ErrorMessage = httpResponse.StatusCode + " " + httpResponse.ReasonPhrase
                    };
                }
                var responseResult = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return new HttpWebApiResponse<T> { ModelObject = (T)Convert.ChangeType(responseResult, typeof(T)) };
            }
            catch (Exception ex)
            {
                return new HttpWebApiResponse<T>
                {
                    ResponseStatus = ResponseStatuses.Failed,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        #region UrlEncode
        private static IList<string> UrlEncode(string name, object value)
        {
            var encodedQueryStrings = new List<string>();
            if (value == null || RegularTypes.Any(x => x.Name == value.GetType().Name))
            {
                encodedQueryStrings.Add(string.Format("{0}={1}", name, WebUtility.UrlEncode(value == null ? null : value.ToString())));
            }
            else if (value is IEnumerable)
            {
                var encodedList = new List<string>();
                var i = 0;
                foreach (var item in (IEnumerable)value)
                {
                    encodedList.Add(string.Format("{0}[{1}]={2}", name, i, WebUtility.UrlEncode(item == null ? null : item.ToString())));
                    i++;
                }
                encodedQueryStrings.AddRange(encodedList);
            }
            else
            {
                //encodedQueryStrings.AddRange(value.GetType().GetProperties().SelectMany(x => UrlEncode(name + "." + x.Name, x.GetValue(value))));
            }
            return encodedQueryStrings;
        } 
        #endregion

        #region RegularTypes
        private static Type[] RegularTypes
        {
            get
            {
                return new Type[]
                {
                    typeof(bool),
                    typeof(byte),
                    typeof(sbyte),
                    typeof(int),
                    typeof(char),
                    typeof(decimal),
                    typeof(double),
                    typeof(float),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    //typeof(object),
                    typeof(short),
                    typeof(ushort),
                    typeof(string),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(Guid),
                };
            }
        } 
        #endregion
    }
}
