using System.Collections.Generic;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public interface IProxyConfiguration
    {
        #region Public Properties

        IEnumerable<DelegatingHandler> DelegatingHandlers { get; }

        #endregion Public Properties
    }
}
