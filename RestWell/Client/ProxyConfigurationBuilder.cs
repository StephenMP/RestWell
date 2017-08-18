using RestWell.Domain.Proxy;
using System.Linq;
using System.Net.Http;

namespace RestWell.Client
{
    public class ProxyConfigurationBuilder
    {
        private readonly ProxyConfiguration proxyConfiguration;

        public static ProxyConfigurationBuilder CreateBuilder() => new ProxyConfigurationBuilder();

        public ProxyConfigurationBuilder()
        {
            this.proxyConfiguration = new ProxyConfiguration();
        }

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
    }
}
