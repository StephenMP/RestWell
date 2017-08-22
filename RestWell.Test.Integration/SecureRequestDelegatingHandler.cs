using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RestWell.Test.Integration
{
    public class SecureRequestDelegatingHandler : DelegatingHandler
    {
        #region Protected Methods

        private readonly string scheme;
        private readonly string token;

        public SecureRequestDelegatingHandler(string scheme, string token)
        {
            this.scheme = scheme;
            this.token = token;
        }

        public SecureRequestDelegatingHandler() : this("Basic", "Username:Password")
        {
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "Username:Password");

            return await base.SendAsync(request, cancellationToken);
        }

        #endregion Protected Methods
    }
}
