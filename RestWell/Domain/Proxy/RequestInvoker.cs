using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Enums;
using RestWell.Domain.Factories;
using RestWell.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestWell.Domain.Proxy
{
    internal class RequestInvoker
    {
        #region Private Fields

        private readonly IProxyConfiguration proxyConfiguration;

        #endregion Private Fields

        #region Protected Constructors

        public RequestInvoker(IProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration;
        }

        #endregion Protected Constructors

        #region Public Methods

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

            using (var client = CreateHttpClient(this.proxyConfiguration))
            {
                try
                {
                    using (var response = await client.InvokeAsync(request))
                    {
                        return await ProxyResponseFactory.CreateAsync<TResponseDto>(response);
                    }
                }

                catch (Exception e)
                {
                    return await ProxyResponseFactory.CreateAsync<TResponseDto>(e);
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods

        private static HttpClient CreateHttpClient(IProxyConfiguration proxyConfiguration)
        {
            if (proxyConfiguration?.DelegatingHandlers.Count() > 0)
            {
                return HttpClientFactory.Create(proxyConfiguration.DelegatingHandlers.ToArray());
            }

            return HttpClientFactory.Create();
        }

        #endregion Protected Methods
    }
}
