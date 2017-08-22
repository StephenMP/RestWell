using RestWell.Domain.Proxy;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestWell.Client
{
    public class ProxyConfigurationBuilder
    {
        #region Private Fields

        private readonly ProxyConfiguration proxyConfiguration;

        #endregion Private Fields

        #region Public Constructors

        public ProxyConfigurationBuilder()
        {
            this.proxyConfiguration = new ProxyConfiguration();
        }

        #endregion Public Constructors

        #region Public Methods

        public static ProxyConfigurationBuilder CreateBuilder() => new ProxyConfigurationBuilder();

        public ProxyConfigurationBuilder AddDelegatingHandlers(params DelegatingHandler[] handlers)
        {
            var currentHandlers = this.proxyConfiguration.DelegatingHandlers.ToList();
            currentHandlers.AddRange(handlers);

            this.proxyConfiguration.DelegatingHandlers = currentHandlers;

            return this;
        }

        public IProxyConfiguration Build()
        {
            return this.proxyConfiguration;
        }

        public ProxyConfigurationBuilder UseDefaultAcceptHeader(MediaTypeWithQualityHeaderValue mediaType)
        {
            this.proxyConfiguration.DefaultProxyRequestHeaders.Accept.Add(mediaType);
            return this;
        }

        public ProxyConfigurationBuilder UseDefaultAuthorizationHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            this.proxyConfiguration.DefaultProxyRequestHeaders.Authorization = authenticationHeaderValue;
            return this;
        }

        public ProxyConfigurationBuilder UseDefaultHeader(string header, params string[] values)
        {
            this.proxyConfiguration.DefaultProxyRequestHeaders.AdditionalHeaders.Add(header, values);
            return this;
        }

        #endregion Public Methods
    }
}
