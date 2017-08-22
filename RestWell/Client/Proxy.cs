using Nito.AsyncEx;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Proxy;
using System;
using System.Threading.Tasks;

namespace RestWell.Client
{
    public sealed class Proxy : IProxy
    {
        #region Private Fields

        private readonly RequestInvoker requestInvoker;

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public Proxy() : this(new ProxyConfiguration())
        {
        }

        public Proxy(IProxyConfiguration proxyConfiguration)
        {
            proxyConfiguration = proxyConfiguration ?? new ProxyConfiguration();
            this.requestInvoker = new RequestInvoker(proxyConfiguration);
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Invokes the ProxyRequest
        /// </summary>
        /// <typeparam name="TRequestDto"></typeparam>
        /// <typeparam name="TResponseDto"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public IProxyResponse<TResponseDto> Invoke<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return AsyncContext.Run(() => this.requestInvoker.InvokeAsync(request));
        }

        /// <summary>
        /// Invokes the ProxyRequest using the async framework
        /// </summary>
        /// <typeparam name="TRequestDto"></typeparam>
        /// <typeparam name="TResponseDto"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return await this.requestInvoker.InvokeAsync(request);
        }

        #endregion Public Methods

        #region Private Methods

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.requestInvoker?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Private Methods
    }
}
