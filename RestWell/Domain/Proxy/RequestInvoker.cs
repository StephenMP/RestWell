using RestWell.Client;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Extensions;
using RestWell.Domain.Factories;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestWell.Domain.Proxy
{
    internal class RequestInvoker : IDisposable
    {
        #region Private Fields

        private readonly HttpClient httpClient;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public RequestInvoker(IProxyConfiguration proxyConfiguration)
        {
            this.httpClient = HttpClientFactory.Create(proxyConfiguration.DefaultProxyRequestHeaders, proxyConfiguration.DelegatingHandlers.Values);
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            if (request.HttpRequestMethod == HttpRequestMethod.None)
            {
                return new ProxyResponse<TResponseDto>
                {
                    IsSuccessfulStatusCode = false,
                    StatusCode = HttpStatusCode.MethodNotAllowed,
                    ResponseMessage = $"Unrecognized request type. Supported types are DELETE, GET, HEAD, OPTIONS, PATCH, POST, and PUT."
                };
            }

            try
            {
                using (var response = await this.httpClient.InvokeAsync(request))
                {
                    return await ProxyResponseFactory.CreateAsync<TResponseDto>(response);
                }
            }
            catch (Exception e)
            {
                return await ProxyResponseFactory.CreateAsync<TResponseDto>(e);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.httpClient?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
