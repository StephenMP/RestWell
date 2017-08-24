using RestWell.Client.Enums;
using System;
using System.Collections.Generic;

namespace RestWell.Client.Request
{
    public class ProxyRequest<TRequestDto, TResponseDto> : IProxyRequest<TRequestDto, TResponseDto>
    {
        #region Public Constructors

        public ProxyRequest()
        {
            this.Headers = new Dictionary<string, IEnumerable<string>>();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The headers which will be used on this request. Note that the DefaultProxyRequestHeaders
        /// will be used Unless specifically overriden here.
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Headers { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request method used for the request.
        /// </summary>
        /// <value>The HTTP request method.</value>
        public HttpRequestMethod HttpRequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the request dto representing the request body.
        /// </summary>
        /// <value>The request dto.</value>
        public TRequestDto RequestDto { get; set; }

        /// <summary>
        /// Gets or sets the request URI for this request.
        /// </summary>
        /// <value>The request URI.</value>
        public Uri RequestUri { get; set; }

        #endregion Public Properties
    }
}
