using Nito.AsyncEx;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Proxy;
using System.Threading.Tasks;

namespace RestWell.Client
{
    public sealed class Proxy : IProxy
    {
        #region Private Fields

        private readonly RequestInvoker requestInvoker;

        #endregion Private Fields

        #region Public Constructors

        public Proxy() : this(new ProxyConfiguration())
        {
        }

        public Proxy(IProxyConfiguration proxyConfiguration)
        {
            this.requestInvoker = new RequestInvoker(proxyConfiguration);
        }

        #endregion Public Constructors

        #region Public Methods

        public IProxyResponse<TResponseDto> Invoke<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return AsyncContext.Run(() => this.requestInvoker.InvokeAsync(request));
        }

        public async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return await this.requestInvoker.InvokeAsync(request);
        }

        #endregion Public Methods
    }
}
