using RestWell.Domain.Request;
using System.Collections.Generic;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public interface IProxyConfiguration
    {
        #region Public Properties

        IEnumerable<DelegatingHandler> DelegatingHandlers { get; }
        DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; }

        #endregion Public Properties
    }
}
