using Newtonsoft.Json;
using RestWell.Client.Request;
using RestWell.Domain.Enums;
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

        public static async Task<HttpResponseMessage> InvokeAsync<TRequestDto, TResponseDto>(this HttpClient client, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            AddHeaders(client, request.Headers);

            if (request.RequestDto == null)
            {
                return await client.InvokeAsync(request.RequestUri, request.HttpRequestMethod);
            }

            return await client.InvokeAsJsonAsync(request.RequestUri, request.HttpRequestMethod, request.RequestDto);
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

        private static async Task<HttpResponseMessage> InvokeAsJsonAsync<T>(this HttpClient client, Uri requestUri, HttpRequestMethod httpRequestMethod, T value)
        {
            var jsonContent = JsonConvert.SerializeObject(value);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod(httpRequestMethod.ToString().ToUpper()), requestUri) { Content = stringContent };
            return await client.SendAsync(request);
        }

        private static async Task<HttpResponseMessage> InvokeAsync(this HttpClient client, Uri requestUri, HttpRequestMethod httpRequestMethod)
        {
            var request = new HttpRequestMessage(new HttpMethod(httpRequestMethod.ToString().ToUpper()), requestUri);
            return await client.SendAsync(request);
        }

        #endregion Private Methods
    }
}
