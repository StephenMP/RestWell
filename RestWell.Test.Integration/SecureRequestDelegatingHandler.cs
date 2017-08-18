using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestWell.Test.Integration
{
    public class SecureRequestDelegatingHandler : DelegatingHandler
    {
        #region Protected Methods

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "Username:Password");

            return await base.SendAsync(request, cancellationToken);
        }

        #endregion Protected Methods
    }
}
