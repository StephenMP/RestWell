using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Enums;
using RestWell.Domain.Factories;
using RestWell.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RestWell.Domain.Proxy
{
    internal class RequestInvoker
    {
        #region Private Fields

        private readonly IProxyConfiguration proxyConfiguration;

        #endregion Private Fields

        #region Public Constructors

        public RequestInvoker(IProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration;
        }

        #endregion Public Constructors

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

            using (var client = HttpClientFactory.Create(this.proxyConfiguration))
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
    }
}
