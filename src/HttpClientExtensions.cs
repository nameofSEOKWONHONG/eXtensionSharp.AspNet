using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace eXtensionSharp.AspNet
{
    /// <summary>
    /// Implement HttpClient Extension
    /// ****Caution****
    /// It is important to follow the Http Method rules. If you consider security and logging, you should follow the Http Method rules.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// call http get method(get search data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="headerHandler">If you're using IHttpClientFactory and you're already making a declaration about Headers, don't use headerHandler.</param>
        /// <returns></returns>
        public static async Task<T> xHttpGetAsync<T>(this HttpClient client, string url, Action<HttpRequestHeaders> headerHandler = null)
        {
            if(headerHandler.xIsNotEmpty())
            {
                client.DefaultRequestHeaders.Clear();
                headerHandler(client.DefaultRequestHeaders);
            }
            var response = await client.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default(T);
        }

        /// <summary>
        /// call http post method(add new data or modify all data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headerHandler">If you're using IHttpClientFactory and you're already making a declaration about Headers, don't use headerHandler.</param>
        /// <returns></returns>
        public static async Task<T> xHttpPostAsync<T>( this HttpClient client, string url, T data, Action<HttpRequestHeaders> headerHandler = null)
        {
            if (headerHandler.xIsNotEmpty())
            {
                client.DefaultRequestHeaders.Clear();
                headerHandler(client.DefaultRequestHeaders);
            }

            var content = new StringContent(data.xToJson(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default(T);
        }

        /// <summary>
        /// call http put method(update data - all of data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headerHandler">If you're using IHttpClientFactory and you're already making a declaration about Headers, don't use headerHandler.</param>
        /// <returns></returns>
        public static async Task<T> xHttpPutAsync<T>(this HttpClient client, string url, T data, Action<HttpRequestHeaders> headerHandler = null)
        {
            if(headerHandler.xIsNotEmpty())
            {
                client.DefaultRequestHeaders.Clear();
                headerHandler(client.DefaultRequestHeaders);
            }

            var content = new StringContent(data.xToJson(), Encoding.UTF8 , "application/json");
            var response = await client.PutAsync(url, content);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default(T);
        }

        /// <summary>
        /// call http patch method(update data - part of data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headerHandler">If you're using IHttpClientFactory and you're already making a declaration about Headers, don't use headerHandler.</param>
        /// <returns></returns>
        public static async Task<T> xHttpPatchAsync<T>(this HttpClient client, string url, T data, Action<HttpRequestHeaders> headerHandler = null)
        {
            if (headerHandler.xIsNotEmpty())
            {
                client.DefaultRequestHeaders.Clear();
                headerHandler(client.DefaultRequestHeaders);
            }

            var content = new StringContent(data.xToJson(), Encoding.UTF8, "application/json");
            var response = await client.PatchAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default(T);
        }

        /// <summary>
        /// call http delete method(remove single data or row)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="headerHandler">If you're using IHttpClientFactory and you're already making a declaration about Headers, don't use headerHandler.</param>
        /// <returns></returns>
        public static async Task<T> xHttpDeleteAsync<T>(this HttpClient client, string url, Action<HttpRequestHeaders> headerHandler = null)
        {
            if (headerHandler.xIsNotEmpty())
            {
                client.DefaultRequestHeaders.Clear();
                headerHandler(client.DefaultRequestHeaders);
            }

            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            return default(T);
        }
    }
}
