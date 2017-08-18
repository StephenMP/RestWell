using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        public IEnumerable<DelegatingHandler> DelegatingHandlers { get; set; }

        public ProxyConfiguration()
        {
            this.DelegatingHandlers = Enumerable.Empty<DelegatingHandler>();
        }
    }
}
