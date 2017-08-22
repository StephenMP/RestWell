using RestWell.Domain.Request;
using System;
using System.Collections.Generic;

namespace RestWell.Domain.Proxy
{
    public interface IProxyConfiguration
    {
        #region Public Properties

        IDictionary<Type, List<object>> DelegatingHandlerTypes { get; }
        DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; }

        #endregion Public Properties
    }
}
