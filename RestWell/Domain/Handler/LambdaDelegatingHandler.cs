using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestWell.Domain.Handler
{
    internal class LambdaDelegatingHandler : DelegatingHandler
    {
        private readonly Action<HttpRequestMessage, CancellationToken> actionBeforeSendingRequest;

        public LambdaDelegatingHandler(Action<HttpRequestMessage, CancellationToken> actionBeforeSendingRequest)
        {
            this.actionBeforeSendingRequest = actionBeforeSendingRequest;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.actionBeforeSendingRequest(request, cancellationToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
