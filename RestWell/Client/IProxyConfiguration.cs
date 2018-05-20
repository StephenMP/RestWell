using System;
using System.Collections.Generic;
using System.Net.Http;
using RestWell.Domain.Request;

namespace RestWell.Client
{
    public interface IProxyConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The default proxy request headers used on every request. Note this can be overriden by
        /// the Headers on IProxyRequest
        /// </summary>
        /// <value>The default proxy request headers.</value>
        DefaultProxyRequestHeaders DefaultProxyRequestHeaders { get; }

        /// <summary>
        /// The delegating handlers to be injected into the request pipeline.
        /// </summary>
        /// <value>The delegating handlers.</value>
        IDictionary<Type, DelegatingHandler> DelegatingHandlers { get; }

        #endregion Public Properties
    }
}
