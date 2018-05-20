using System;
using System.Net;
using System.Net.Http.Headers;
using RestWell.Client.Enums;

namespace RestWell.Client.Response
{
    public class ProxyResponse<TResponseDto> : IProxyResponse<TResponseDto>
    {
        #region Public Properties

        /// <summary>
        /// Gets the HTTP request method used for the request.
        /// </summary>
        /// <value>The HTTP request method.</value>
        public HttpRequestMethod HttpRequestMethod { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        /// <value>
        /// <c>true</c> if the status code returned is a success status code; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessfulStatusCode { get; set; }

        /// <summary>
        /// Gets the request headers used for the request.
        /// </summary>
        /// <value>The request headers.</value>
        public HttpRequestHeaders RequestHeaders { get; set; }

        /// <summary>
        /// Gets the request URI used for the request.
        /// </summary>
        /// <value>The request URI.</value>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Gets the response dto returned in the response body.
        /// </summary>
        /// <value>The response dto.</value>
        public TResponseDto ResponseDto { get; set; }

        /// <summary>
        /// Gets the response headers returned by the service.
        /// </summary>
        /// <value>The response headers.</value>
        public HttpResponseHeaders ResponseHeaders { get; set; }

        /// <summary>
        /// Gets the response message used for the request.
        /// </summary>
        /// <value>The response message.</value>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Gets the status code returned by the service.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; set; }

        #endregion Public Properties
    }
}
