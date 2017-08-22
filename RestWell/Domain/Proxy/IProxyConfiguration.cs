using RestWell.Domain.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public interface IProxyConfiguration
    {
        #region Public Properties

        DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; }
        IDictionary<Type, DelegatingHandler> DelegatingHandlers { get; }

        #endregion Public Properties
    }
}
