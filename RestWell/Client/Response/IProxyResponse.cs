using System;
using System.Net;
using System.Net.Http.Headers;
using RestWell.Client.Enums;

namespace RestWell.Client.Response
{
    public interface IProxyResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets the HTTP request method used for the request.
        /// </summary>
        /// <value>The HTTP request method.</value>
        HttpRequestMethod HttpRequestMethod { get; }

        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        /// <value>
        /// <c>true</c> if the status code returned is a success status code; otherwise, <c>false</c>.
        /// </value>
        bool IsSuccessfulStatusCode { get; }

        /// <summary>
        /// Gets the request headers used for the request.
        /// </summary>
        /// <value>The request headers.</value>
        HttpRequestHeaders RequestHeaders { get; }

        /// <summary>
        /// Gets the request URI used for the request.
        /// </summary>
        /// <value>The request URI.</value>
        Uri RequestUri { get; }

        /// <summary>
        /// Gets the response headers returned by the service.
        /// </summary>
        /// <value>The response headers.</value>
        HttpResponseHeaders ResponseHeaders { get; }

        /// <summary>
        /// Gets the response message used for the request.
        /// </summary>
        /// <value>The response message.</value>
        string ResponseMessage { get; }

        /// <summary>
        /// Gets the status code returned by the service.
        /// </summary>
        /// <value>The status code.</value>
        HttpStatusCode StatusCode { get; }

        #endregion Public Properties
    }

    public interface IProxyResponse<TResponseDto> : IProxyResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets the response dto returned in the response body.
        /// </summary>
        /// <value>The response dto.</value>
        TResponseDto ResponseDto { get; }

        #endregion Public Properties
    }
}
