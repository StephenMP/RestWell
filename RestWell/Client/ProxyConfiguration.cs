using RestWell.Domain.Proxy;
using RestWell.Domain.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestWell.Client
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        #region Public Constructors

        internal ProxyConfiguration()
        {
            this.DelegatingHandlers = new Dictionary<Type, DelegatingHandler>();
            this.DefaultProxyRequestHeaders = new DefaultProxyRequestHeaders();
        }

        #endregion Public Constructors

        #region Public Properties

        public DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; set; }
        public IDictionary<Type, DelegatingHandler> DelegatingHandlers { get; set; }

        #endregion Public Properties
    }
}
