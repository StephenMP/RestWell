using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Proxy;
using RestWell.Extensions;
using System.Threading.Tasks;

namespace RestWell.Domain.Invokers
{
    internal sealed class PutInvoker : Invoker
    {
        #region Public Constructors

        public PutInvoker(IProxyConfiguration proxyConfiguration) : base(proxyConfiguration)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request)
        {
            using (var client = this.CreateHttpClient())
            {
                using (var response = await client.InvokePutAsync(request))
                {
                    return await ProxyResponseFactory.CreateAsync<TResponseDto>(response);
                }
            }
        }

        #endregion Public Methods
    }
}
