using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Invokers;
using RestWell.Domain.Proxy;
using System;
using System.Threading.Tasks;

namespace RestWell.Client
{
    public sealed class Proxy : IProxy
    {
        #region Private Fields

        private readonly RequestInvoker requestInvoker;

        #endregion Private Fields

        #region Public Constructors

        public Proxy() : this(null)
        {
        }

        public Proxy(IProxyConfiguration proxyConfiguration)
        {
            this.requestInvoker = new RequestInvoker(proxyConfiguration);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            try
            {
                return await this.requestInvoker[request.HttpRequestMethod].InvokeAsync(request);
            }
            catch (Exception e)
            {
                return await ProxyResponseFactory.CreateAsync<TResponseDto>(e);
            }
        }

        #endregion Public Methods
    }
}
