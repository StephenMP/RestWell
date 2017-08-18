using RestWell.Domain.Proxy;
using System.Linq;
using System.Net.Http;

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

        #endregion Public Methods
    }
}
