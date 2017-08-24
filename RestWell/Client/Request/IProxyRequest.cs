using RestWell.Client.Enums;
using System;
using System.Collections.Generic;

namespace RestWell.Client.Request
{
    public interface IProxyRequest<TRequestDto, TResponseDto>
    {
        #region Public Properties

        /// <summary>
        /// The headers which will be used on this request. Note that the DefaultProxyRequestHeaders
        /// will be used Unless specifically overriden here.
        /// </summary>
        IDictionary<string, IEnumerable<string>> Headers { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request method used for the request.
        /// </summary>
        /// <value>The HTTP request method.</value>
        HttpRequestMethod HttpRequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the request dto representing the request body.
        /// </summary>
        /// <value>The request dto.</value>
        TRequestDto RequestDto { get; set; }

        /// <summary>
        /// Gets or sets the request URI for this request.
        /// </summary>
        /// <value>The request URI.</value>
        Uri RequestUri { get; set; }

        #endregion Public Properties
    }
}
