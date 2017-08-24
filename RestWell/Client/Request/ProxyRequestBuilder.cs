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

        public ProxyRequestBuilder(string baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod)
        {
        }

        public ProxyRequestBuilder(Uri baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod)
        {
        }

        #endregion Public Constructors
    }

    public class ProxyRequestBuilder<TResponseDto> : ProxyRequestBuilder<Missing, TResponseDto>
    {
        #region Public Constructors

        public ProxyRequestBuilder(string baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod)
        {
        }

        public ProxyRequestBuilder(Uri baseUri, HttpRequestMethod requestMethod) : base(baseUri, requestMethod)
        {
        }

        #endregion Public Constructors
    }

    public class ProxyRequestBuilder<TRequestDto, TResponseDto>
    {
        #region Private Fields

        private Uri baseUri;
        private List<string> pathArguments;
        private IProxyRequest<TRequestDto, TResponseDto> proxyRequest;
        private Dictionary<string, IList<string>> queryParameters;
        private List<string> routeAppendages;

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
            this.proxyRequest = new ProxyRequest<TRequestDto, TResponseDto>();
            this.proxyRequest.HttpRequestMethod = requestMethod;
            this.routeAppendages = new List<string>();
            this.pathArguments = new List<string>();
            this.queryParameters = new Dictionary<string, IList<string>>();
        }

        public ProxyRequestBuilder(Uri baseUri, HttpRequestMethod requestMethod) : this(baseUri.ToString(), requestMethod)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public static ProxyRequestBuilder<TRequestDto, TResponseDto> CreateBuilder(string baseUri, HttpRequestMethod requestMethod) => new ProxyRequestBuilder<TRequestDto, TResponseDto>(baseUri, requestMethod);

        public static ProxyRequestBuilder<TRequestDto, TResponseDto> CreateBuilder(Uri baseUri, HttpRequestMethod requestMethod) => new ProxyRequestBuilder<TRequestDto, TResponseDto>(baseUri, requestMethod);

        /// <summary>
        /// Set/Modify the Accept Header
        /// </summary>
        /// <param name="mediaTypeValues">Accept header media type values (e.g. application/json)</param>
        /// <returns>Reference to the current builder</returns>
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

            if (this.proxyRequest.Headers.ContainsKey(headerName))
            {
                var header = this.proxyRequest.Headers[headerName].ToList();
                header.AddRange(values);

                this.proxyRequest.Headers[headerName] = header;
            }
            else
            {
                this.proxyRequest.Headers.Add(headerName, values);
            }

            return this;
        }

        /// <summary>
        /// Appends path arguments onto the request (e.g. https://www.test.com/api/pathArg/pathArg)
        /// </summary>
        /// <param name="pathArguments">
        /// Must be an object with a valid ToString() which returns a value for the path argument.
        /// </param>
        /// <returns>Reference to the current builder</returns>
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
        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddQueryParameter(string queryParam, object value)
        {
            if (queryParam.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(queryParam)} was null or empty. You must specify the query parameter when using {nameof(AddQueryParameter)}");
            }

            if (value == null || value.ToString().IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(value)} was null or empty. You must specify the query parameter value when using {nameof(AddQueryParameter)}");
            }

            if (!queryParam.IsNullOrEmptyOrWhitespace() && value != null)
            {
                if (this.queryParameters.ContainsKey(queryParam))
                {
                    this.queryParameters[queryParam].Add(value.ToString());
                }
                else
                {
                    this.queryParameters.Add(queryParam, new List<string> { value.ToString() });
                }

                return this;
            }

            throw new ArgumentException("You must specify a query param and it's corresponding value!");
        }

        /// <summary>
        /// Appends a value to the current route
        /// </summary>
        /// <param name="appendage">A value you wish to append (e.g. www.test.com/api/appendage)</param>
        /// <returns>Reference to the current builder</returns>
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

            if (this.proxyRequest.Headers.ContainsKey("Authorization"))
            {
                this.proxyRequest.Headers["Authorization"] = new[] { $"{scheme} {token}" };
            }
            else
            {
                this.proxyRequest.Headers.Add("Authorization", new[] { $"{scheme} {token}" });
            }

            return this;
        }

        /// <summary>
        /// Builds the proxy request.
        /// </summary>
        /// <returns>An IProxyRequest representing your request.</returns>
        public IProxyRequest<TRequestDto, TResponseDto> Build()
        {
            var requestBuilder = new StringBuilder(this.baseUri.ToString().TrimEnd('/'));
            requestBuilder = this.BuildAppendages(requestBuilder);
            requestBuilder = this.BuildPathArguments(requestBuilder);
            requestBuilder = this.BuildQueryParameters(requestBuilder);

            this.proxyRequest.RequestUri = new Uri(requestBuilder.ToString());

            return this.proxyRequest;
        }

        /// <summary>
        /// Sets the request body of the request.
        /// </summary>
        /// <param name="requestDto">A DTO which represents the request body</param>
        /// <returns>Reference to the current builder</returns>
        public ProxyRequestBuilder<TRequestDto, TResponseDto> SetRequestDto(TRequestDto requestDto)
        {
            if (requestDto == null)
            {
                throw new ArgumentException($"{nameof(requestDto)} was null. You must specify a valud request DTO when using {nameof(SetRequestDto)}");
            }

            this.proxyRequest.RequestDto = requestDto;
            return this;
        }

        #endregion Public Methods

        #region Private Methods

        private StringBuilder BuildAppendages(StringBuilder requestBuilder)
        {
            if (this.routeAppendages.Count > 0)
            {
                requestBuilder.Append("/");

                foreach (var appendage in this.routeAppendages)
                {
                    requestBuilder.Append($"{appendage}/");
                }
            }

            return new StringBuilder(requestBuilder.ToString().TrimEnd('/'));
        }

        private StringBuilder BuildPathArguments(StringBuilder requestBuilder)
        {
            if (this.pathArguments.Count > 0)
            {
                requestBuilder.Append("/");

                foreach (var pathArgument in this.pathArguments)
                {
                    requestBuilder.Append($"{pathArgument}/");
                }
            }

            return new StringBuilder(requestBuilder.ToString().TrimEnd('/'));
        }

        private StringBuilder BuildQueryParameters(StringBuilder requestBuilder)
        {
            if (this.queryParameters.Count > 0)
            {
                requestBuilder.Append("?");

                foreach (var queryParameter in this.queryParameters)
                {
                    foreach (var value in queryParameter.Value)
                    {
                        requestBuilder.Append($"{queryParameter.Key}={value}&");
                    }
                }
            }

            return new StringBuilder(requestBuilder.ToString().TrimEnd('&'));
        }

        #endregion Private Methods
    }
}
