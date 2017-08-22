using RestWell.Domain.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestWell.Domain.Proxy
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        #region Public Constructors

        public ProxyConfiguration()
        {
            this.DelegatingHandlers = new Dictionary<Type, DelegatingHandler>();
            this.DefaultProxyRequestHeaders = new DefaultProxyRequestHeaders();
        }

        #endregion Public Constructors

        #region Public Properties

        public IDictionary<Type, DelegatingHandler> DelegatingHandlers { get; set; }

        public DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; set; }

        #endregion Public Properties
    }
}
