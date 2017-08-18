using Newtonsoft.Json;
using RestWell.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestWell.Extensions
{
    internal static class HttpClientExtension
    {
        #region Public Methods

        public static async Task<HttpResponseMessage> InvokeDeleteAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsync(request.RequestUri, "DELETE");
        }

        public static async Task<HttpResponseMessage> InvokeGetAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsync(request.RequestUri, "GET");
        }

        public static async Task<HttpResponseMessage> InvokeHeadAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsync(request.RequestUri, "HEAD");
        }

        public static async Task<HttpResponseMessage> InvokeOptionsAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsync(request.RequestUri, "OPTIONS");
        }

        public static async Task<HttpResponseMessage> InvokePatchAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsJsonAsync(request.RequestUri, "PATCH", request.RequestDto);
        }

        public static async Task<HttpResponseMessage> InvokePostAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsJsonAsync(request.RequestUri, "POST", request.RequestDto);
        }

        public static async Task<HttpResponseMessage> InvokePutAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);
            return await client.InvokeAsJsonAsync(request.RequestUri, "PUT", request.RequestDto);
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddHeaders(HttpClient client, IDictionary<string, IEnumerable<string>> headers)
        {
            foreach (var key in headers.Keys)
            {
                client.DefaultRequestHeaders.Add(key, headers[key]);
            }
        }

        private static async Task<HttpResponseMessage> InvokeAsJsonAsync<T>(this HttpClient client, Uri requestUri, string requestType, T value)
        {
            var jsonContent = JsonConvert.SerializeObject(value);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(requestType), requestUri) { Content = stringContent };
            return await client.SendAsync(request);
        }

        private static async Task<HttpResponseMessage> InvokeAsync(this HttpClient client, Uri requestUri, string requestType)
        {
            var request = new HttpRequestMessage(new HttpMethod(requestType), requestUri);
            return await client.SendAsync(request);
        }

        #endregion Private Methods
    }
}
