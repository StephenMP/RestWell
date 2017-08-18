using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Factories;
using RestWell.Domain.Proxy;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestWell.Domain.Invokers
{
    internal abstract class Invoker
    {
        #region Private Fields

        private readonly IProxyConfiguration proxyConfiguration;

        #endregion Private Fields

        #region Protected Constructors

        protected Invoker(IProxyConfiguration proxyConfiguration)
        {
            this.proxyConfiguration = proxyConfiguration;
        }

        #endregion Protected Constructors

        #region Protected Properties

        protected HttpClient CreateHttpClient()
        {
            if (this.proxyConfiguration?.DelegatingHandlers.Count() > 0)
            {
                return HttpClientFactory.Create(this.proxyConfiguration.DelegatingHandlers.ToArray());
            }

            return HttpClientFactory.Create();
        }

        #endregion Protected Properties

        #region Public Methods

        public abstract Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class;

        #endregion Public Methods
    }
}
