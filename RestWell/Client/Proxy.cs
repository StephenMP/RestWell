using Nito.AsyncEx;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Extensions;
using RestWell.Domain.Factories;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestWell.Client
{
    public sealed class Proxy : IProxy
    {
        #region Private Fields

        private readonly HttpClient httpClient;
        private readonly IProxyConfiguration proxyConfiguration;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public Proxy() : this(new ProxyConfiguration())
        {
        }

        public Proxy(IProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration ?? new ProxyConfiguration();
            this.httpClient = HttpClientFactory.Create(this.proxyConfiguration.DelegatingHandlers.Values);
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Invokes the specified request.
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>A ProxyResonse containing request response information</returns>
        public IProxyResponse<TResponseDto> Invoke<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return AsyncContext.Run(() => this.InvokeRequestAsync(request));
        }

        /// <summary>
        /// Invokes the specified request using the asynchronous framework.
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>A ProxyResonse containing request response information</returns>
        public async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return await this.InvokeRequestAsync(request);
        }

        #endregion Public Methods

        #region Private Methods

        private void Dispose(bool disposing)
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

        private async Task<IProxyResponse<TResponseDto>> InvokeRequestAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
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
                using (var response = await this.httpClient.InvokeAsync(this.proxyConfiguration.DefaultProxyRequestHeaders, request))
                {
                    return await ProxyResponseFactory.CreateAsync<TResponseDto>(response);
                }
            }
            catch (Exception e)
            {
                return ProxyResponseFactory.Create<TResponseDto>(e);
            }
        }

        #endregion Private Methods
    }
}
