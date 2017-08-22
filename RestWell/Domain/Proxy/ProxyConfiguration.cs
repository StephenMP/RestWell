using RestWell.Domain.Request;
using System;
using System.Collections.Generic;

namespace RestWell.Domain.Proxy
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        #region Public Constructors

        public ProxyConfiguration()
        {
            this.DelegatingHandlerTypes = new Dictionary<Type, List<object>>();
            this.DefaultProxyRequestHeaders = new DefaultProxyRequestHeaders();
        }

        #endregion Public Constructors

        #region Public Properties

        public IDictionary<Type, List<object>> DelegatingHandlerTypes { get; set; }

        public DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; set; }

        #endregion Public Properties
    }
}
