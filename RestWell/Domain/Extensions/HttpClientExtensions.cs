using Newtonsoft.Json;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestWell.Domain.Extensions
{
    internal static class HttpClientExtension
    {
        #region Public Methods

        public static async Task<HttpResponseMessage> InvokeAsync<TRequestDto, TResponseDto>(this HttpClient client, DefaultProxyRequestHeaders defaultProxyRequestHeaders, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            client.ClearCurrentHeaders();
            client.PopulateDefaultRequestHeaders(defaultProxyRequestHeaders);
            client.AddRequestHeaders(request.Headers);

            if (request.RequestDto == null)
            {
                return await client.InvokeAsync(request.RequestUri, request.HttpRequestMethod);
            }

            return await client.InvokeAsJsonAsync(request.RequestUri, request.HttpRequestMethod, request.RequestDto);
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddRequestHeaders(this HttpClient client, IDictionary<string, IEnumerable<string>> requestHeaders)
        {
            foreach (var requestHeader in requestHeaders)
            {
                if (client.DefaultRequestHeaders.Contains(requestHeader.Key))
                {
                    client.DefaultRequestHeaders.Remove(requestHeader.Key);
                }

                client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
            }
        }

        private static void ClearCurrentHeaders(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
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

        private static void PopulateDefaultRequestHeaders(this HttpClient client, DefaultProxyRequestHeaders defaultProxyRequestHeaders)
        {
            if (defaultProxyRequestHeaders.Authorization != null)
            {
                client.DefaultRequestHeaders.Authorization = defaultProxyRequestHeaders.Authorization;
            }

            if (defaultProxyRequestHeaders.Accept != null)
            {
                foreach (var mediaTypeHeader in defaultProxyRequestHeaders.Accept)
                {
                    client.DefaultRequestHeaders.Accept.Add(mediaTypeHeader);
                }
            }

            if (defaultProxyRequestHeaders.AdditionalHeaders.Any())
            {
                foreach (var additionalHeader in defaultProxyRequestHeaders.AdditionalHeaders)
                {
                    client.DefaultRequestHeaders.Add(additionalHeader.Key, additionalHeader.Value);
                }
            }
        }

        #endregion Private Methods
    }
}
