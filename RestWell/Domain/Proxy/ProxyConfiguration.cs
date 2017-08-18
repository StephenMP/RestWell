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
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<DelegatingHandler> DelegatingHandlers { get; set; }

        #endregion Public Properties
    }
}
