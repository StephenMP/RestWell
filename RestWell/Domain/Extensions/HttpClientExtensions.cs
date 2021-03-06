using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Domain.Request;

namespace RestWell.Domain.Extensions
{
    internal static class HttpClientExtension
    {
        #region Public Methods

        public static async Task<HttpResponseMessage> InvokeAsync<TRequestDto, TResponseDto>(this HttpClient client, DefaultProxyRequestHeaders defaultProxyRequestHeaders, IProxyRequest<TRequestDto, TResponseDto> request)
        {
            client.ClearCurrentHeaders()
                  .PopulateDefaultRequestHeaders(defaultProxyRequestHeaders)
                  .AddRequestHeaders(request.Headers);

            if (request.RequestDto == null)
            {
                return await client.InvokeAsync(request.RequestUri, request.HttpRequestMethod);
            }

            return await client.InvokeAsJsonAsync(request.RequestUri, request.HttpRequestMethod, request.RequestDto);
        }

        #endregion Public Methods

        #region Private Methods

        private static HttpClient AddRequestHeaders(this HttpClient client, IDictionary<string, IEnumerable<string>> requestHeaders)
        {
            foreach (var requestHeader in requestHeaders)
            {
                if (client.DefaultRequestHeaders.Contains(requestHeader.Key))
                {
                    client.DefaultRequestHeaders.Remove(requestHeader.Key);
                }

                client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
            }

            return client;
        }

        private static HttpClient ClearCurrentHeaders(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            return client;
        }

        private static async Task<HttpResponseMessage> InvokeAsJsonAsync<T>(this HttpClient client, Uri requestUri, HttpRequestMethod httpRequestMethod, T value)
        {
            var jsonContent = JsonConvert.SerializeObject(value);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(httpRequestMethod.ToString()), requestUri) { Content = stringContent };

            return await client.SendAsync(request);
        }

        private static async Task<HttpResponseMessage> InvokeAsync(this HttpClient client, Uri requestUri, HttpRequestMethod httpRequestMethod)
        {
            var request = new HttpRequestMessage(new HttpMethod(httpRequestMethod.ToString()), requestUri);
            return await client.SendAsync(request);
        }

        private static HttpClient PopulateDefaultRequestHeaders(this HttpClient client, DefaultProxyRequestHeaders defaultProxyRequestHeaders)
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

            return client;
        }

        #endregion Private Methods
    }
}
