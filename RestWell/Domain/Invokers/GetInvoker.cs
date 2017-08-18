using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Proxy;
using RestWell.Extensions;
using System.Threading.Tasks;

namespace RestWell.Domain.Invokers
{
    internal sealed class GetInvoker : Invoker
    {
        #region Public Constructors

        public GetInvoker(IProxyConfiguration proxyConfiguration) : base(proxyConfiguration)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request)
        {
            using (var client = this.CreateHttpClient())
            {
                using (var response = await client.InvokeGetAsync(request))
                {
                    return await ProxyResponseFactory.CreateAsync<TResponseDto>(response);
                }
            }
        }

        #endregion Public Methods
    }
}
