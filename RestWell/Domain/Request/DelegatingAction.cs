using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestWell.Domain.Request
{
    internal sealed class DelegatingAction : DelegatingHandler
    {
        #region Private Fields

        private readonly Action<HttpRequestMessage, CancellationToken> actionBeforeSendingRequest;

        #endregion Private Fields

        #region Public Constructors

        public DelegatingAction(Action<HttpRequestMessage, CancellationToken> actionBeforeSendingRequest)
        {
            this.actionBeforeSendingRequest = actionBeforeSendingRequest;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.actionBeforeSendingRequest(request, cancellationToken);
            return await base.SendAsync(request, cancellationToken);
        }

        #endregion Protected Methods
    }
}
