using System.Collections.Generic;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public interface IProxyConfiguration
    {
        IEnumerable<DelegatingHandler> DelegatingHandlers { get; }
    }
}
