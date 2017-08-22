using RestWell.Domain.Request;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        #region Public Constructors

        public ProxyConfiguration()
        {
            this.DelegatingHandlers = Enumerable.Empty<DelegatingHandler>();
            this.DefaultProxyRequestHeaders = new DefaultProxyRequestHeaders();
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<DelegatingHandler> DelegatingHandlers { get; set; }

        public DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; set; }

        #endregion Public Properties
    }
}
