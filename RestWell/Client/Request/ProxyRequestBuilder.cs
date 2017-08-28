using RestWell.Client.Enums;
using RestWell.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RestWell.Client.Request
{
    public class ProxyRequestBuilder : ProxyRequestBuilder<Missing, Missing>
    {

        #region Public Constructors

        public ProxyRequestBuilder(string baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod) { }
        public ProxyRequestBuilder(Uri baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod) { }

        #endregion Public Constructors

    }

    public class ProxyRequestBuilder<TResponseDto> : ProxyRequestBuilder<Missing, TResponseDto>
    {

        #region Public Constructors

        public ProxyRequestBuilder(string baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod) { }
        public ProxyRequestBuilder(Uri baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod) { }

        #endregion Public Constructors

    }

    public class ProxyRequestBuilder<TRequestDto, TResponseDto>
    {

        #region Private Fields

        private readonly Uri baseUri;
        private readonly Dictionary<string, List<string>> headers;
        private readonly List<string> pathArguments;
        private readonly Dictionary<string, List<object>> queryParameters;
        private readonly HttpRequestMethod requestMethod;
        private readonly List<string> routeAppendages;
        private TRequestDto requestDto;

        #endregion Private Fields

        #region Public Constructors

        public ProxyRequestBuilder(string baseUri, HttpRequestMethod requestMethod)
        {
            if (baseUri.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(baseUri)} must be a valid URI scheme (e.g. https://www.test.com/api");
            }

            if (requestMethod == HttpRequestMethod.None)
            {
                throw new ArgumentException($"{nameof(requestMethod)} cannot be {HttpRequestMethod.None}. You must use a valid request type when using AsRequestType");
            }

            this.baseUri = new Uri(baseUri);
            this.headers = new Dictionary<string, List<string>>();
            this.pathArguments = new List<string>();
            this.queryParameters = new Dictionary<string, List<object>>();
            this.requestMethod = requestMethod;
            this.routeAppendages = new List<string>();
        }

        public ProxyRequestBuilder(Uri baseUri, HttpRequestMethod requestMethod) : this(baseUri.ToString(), requestMethod) { }

        #endregion Public Constructors

        #region Public Methods

        public static ProxyRequestBuilder<TRequestDto, TResponseDto> CreateBuilder(string baseUri, HttpRequestMethod requestMethod) => new ProxyRequestBuilder<TRequestDto, TResponseDto>(baseUri, requestMethod);

        public static ProxyRequestBuilder<TRequestDto, TResponseDto> CreateBuilder(Uri baseUri, HttpRequestMethod requestMethod) => new ProxyRequestBuilder<TRequestDto, TResponseDto>(baseUri, requestMethod);

        /// <summary>
        /// Set/Modify the Accept Header
        /// </summary>
        /// <param name="mediaTypeValues">Accept header media type values (e.g. application/json)</param>
        /// <param name="mediaTypeValues">The media type values.</param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">mediaTypeValues</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> Accept(params string[] mediaTypeValues)
        {
            if (mediaTypeValues == null || mediaTypeValues.Length == 0)
            {
                throw new ArgumentException($"{nameof(mediaTypeValues)} was null or empty. You must specify media type values when using {nameof(Accept)}.");
            }

            return this.AddHeader("Accept", mediaTypeValues);
        }

        /// <summary>
        /// Add/Modify a header on the request.
        /// Note: This method does not restrict header modifications. Please ensure you follow proper standards.
        /// </summary>
        /// <param name="headerName">Name of the header to modify</param>
        /// <param name="values">Values for the header</param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">headerName or values</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddHeader(string headerName, params string[] values)
        {
            if (headerName.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(headerName)} was null or empty. You must specify a header name when using {nameof(AddHeader)}.");
            }

            if (values == null || values.Length == 0)
            {
                throw new ArgumentException($"{nameof(values)} was null or empty. You must specify header values when using {nameof(AddHeader)}");
            }

            if (!this.headers.ContainsKey(headerName))
            {
                this.headers.Add(headerName, new List<string>());
            }

            this.headers[headerName].AddRange(values);

            return this;
        }

        /// <summary>
        /// Appends path arguments onto the request (e.g. https://www.test.com/api/pathArg/pathArg)
        /// </summary>
        /// <param name="pathArguments">
        /// Must be an object with a valid ToString() which returns a value for the path argument.
        /// </param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">pathArguments</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddPathArguments(params object[] pathArguments)
        {
            if (pathArguments == null || pathArguments.Length == 0)
            {
                throw new ArgumentException($"{nameof(pathArguments)} was null or empty. You must specify path arguments when using {nameof(AddPathArguments)}");
            }

            foreach (var argument in pathArguments)
            {
                this.pathArguments.Add(argument.ToString());
            }

            return this;
        }

        /// <summary>
        /// Adds query parameters to be appeneded to the request URI (e.g. https://www.test.com/api?queryParam=value).
        /// </summary>
        /// <param name="queryParam">Must match the query parameter name.</param>
        /// <param name="value">
        /// Must have a valid ToString() which returns a value for the query parameter.
        /// </param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">queryParam or value</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddQueryParameter(string queryParam, params object[] values)
        {
            if (queryParam.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(queryParam)} was null or empty. You must specify the query parameter when using {nameof(AddQueryParameter)}");
            }

            if (values == null || values.Length == 0)
            {
                throw new ArgumentException($"{nameof(values)} was null or empty. You must specify the query parameter value when using {nameof(AddQueryParameter)}");
            }

            if (!this.queryParameters.ContainsKey(queryParam))
            {
                this.queryParameters.Add(queryParam, new List<object>());
            }

            this.queryParameters[queryParam].AddRange(values);

            return this;
        }

        /// <summary>
        /// Appends a value to the current route
        /// </summary>
        /// <param name="appendage">A value you wish to append (e.g. www.test.com/api/appendage)</param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">appendages</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> AppendToRoute(params string[] appendages)
        {
            if (appendages == null || appendages.Length == 0)
            {
                throw new ArgumentException($"{nameof(appendages)} was null or empty. You must specify the appendage when using {nameof(AppendToRoute)}");
            }

            this.routeAppendages.AddRange(appendages);

            return this;
        }

        /// <summary>
        /// Sets the authorization header of the request.
        /// </summary>
        /// <param name="scheme">The scheme you'd like to use (e.g. Bearer, Basic, etc.).</param>
        /// <param name="token">The authorization token (e.g. an api key).</param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">scheme or token</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> Authorization(string scheme, string token)
        {
            if (scheme.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(scheme)} was null or empty. You must specify a scheme when using {nameof(Authorization)}");
            }

            if (token.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(token)} was null or empty. You must specify a token when using {nameof(Authorization)}");
            }

            return this.AddHeader("Authorization", $"{scheme} {token}");
        }

        /// <summary>
        /// Builds the proxy request.
        /// </summary>
        /// <returns>An IProxyRequest representing your request.</returns>
        public IProxyRequest<TRequestDto, TResponseDto> Build()
        {
            var routeBuilder = new StringBuilder(this.baseUri.ToString().TrimEnd('/'));
            routeBuilder = BuildRoute(routeBuilder, this.routeAppendages);
            routeBuilder = BuildRoute(routeBuilder, this.pathArguments);
            routeBuilder = BuildQueryParameters(routeBuilder, this.queryParameters);

            return new ProxyRequest<TRequestDto, TResponseDto>(this.headers)
            {
                HttpRequestMethod = this.requestMethod,
                RequestDto = this.requestDto,
                RequestUri = new Uri(routeBuilder.ToString())
            };
        }

        /// <summary>
        /// Sets the request body of the request.
        /// </summary>
        /// <param name="requestDto">A DTO which represents the request body</param>
        /// <returns>Reference to the current builder</returns>
        /// <exception cref="ArgumentException">requestDto</exception>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> SetRequestDto(TRequestDto requestDto)
        {
            if (requestDto == null)
            {
                throw new ArgumentException($"{nameof(requestDto)} was null. You must specify a valid request DTO when using {nameof(SetRequestDto)}");
            }

            this.requestDto = requestDto;

            return this;
        }

        #endregion Public Methods

        #region Private Methods

        private static StringBuilder BuildQueryParameters(StringBuilder requestBuilder, IDictionary<string, List<object>> queryParameters)
        {
            if (queryParameters.Any())
            {
                requestBuilder.Append("?");

                foreach (var queryParameter in queryParameters)
                {
                    foreach (var value in queryParameter.Value)
                    {
                        requestBuilder.Append($"{queryParameter.Key}={value.ToString()}&");
                    }
                }
            }

            return new StringBuilder(requestBuilder.ToString().TrimEnd('&'));
        }

        private static StringBuilder BuildRoute(StringBuilder requestBuilder, IEnumerable<string> thingsToAppend)
        {
            if (thingsToAppend.Any())
            {
                requestBuilder.Append("/");

                foreach (var thingToAppend in thingsToAppend)
                {
                    requestBuilder.Append($"{thingToAppend}/");
                }
            }

            return new StringBuilder(requestBuilder.ToString().TrimEnd('/'));
        }

        #endregion Private Methods

    }
}
