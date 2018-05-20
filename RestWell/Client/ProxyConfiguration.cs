using System;
using System.Collections.Generic;
using System.Net.Http;
using RestWell.Domain.Request;

namespace RestWell.Client
{
    public class ProxyConfiguration : IProxyConfiguration
    {
        #region Internal Constructors

        internal ProxyConfiguration()
        {
            this.DelegatingHandlers = new Dictionary<Type, DelegatingHandler>();
            this.DefaultProxyRequestHeaders = new DefaultProxyRequestHeaders();
        }

        #endregion Internal Constructors

        #region Public Properties

        /// <summary>
        /// The default proxy request headers used on every request. Note this can be overriden by
        /// the Headers on IProxyRequest
        /// </summary>
        /// <value>The default proxy request headers.</value>
        public DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; set; }

        /// <summary>
        /// The delegating handlers to be injected into the request pipeline.
        /// </summary>
        /// <value>The delegating handlers.</value>
        public IDictionary<Type, DelegatingHandler> DelegatingHandlers { get; set; }

        #endregion Public Properties
    }
}
