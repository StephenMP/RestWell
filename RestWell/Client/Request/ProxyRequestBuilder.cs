using RestWell.Domain.Enums;
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

        public ProxyRequestBuilder(string baseUri) : base(baseUri)
        {
        }

        public ProxyRequestBuilder(Uri baseUri) : base(baseUri)
        {
        }

        #endregion Public Constructors
    }

    public class ProxyRequestBuilder<TResponseDto> : ProxyRequestBuilder<Missing, TResponseDto>
    {
        #region Public Constructors

        public ProxyRequestBuilder(string baseUri) : base(baseUri)
        {
        }

        public ProxyRequestBuilder(Uri baseUri) : base(baseUri)
        {
        }

        #endregion Public Constructors
    }

    public class ProxyRequestBuilder<TRequestDto, TResponseDto>
    {
        #region Private Fields

        private Uri baseUri;

        private List<object> pathArguments;

        private IProxyRequest<TRequestDto, TResponseDto> proxyRequest;

        private IDictionary<string, object> queryParameters;

        #endregion Private Fields

        #region Public Constructors

        public ProxyRequestBuilder(string baseUri) : this(new Uri(baseUri))
        {
        }

        public ProxyRequestBuilder(Uri baseUri)
        {
            this.baseUri = baseUri;
            this.proxyRequest = new ProxyRequest<TRequestDto, TResponseDto>();
            this.queryParameters = new Dictionary<string, object>();
            this.pathArguments = new List<object>();
        }

        #endregion Public Constructors

        #region Public Methods

        public static ProxyRequestBuilder<TRequestDto, TResponseDto> CreateBuilder(string baseUri) => new ProxyRequestBuilder<TRequestDto, TResponseDto>(baseUri);

        public static ProxyRequestBuilder<TRequestDto, TResponseDto> CreateBuilder(Uri baseUri) => new ProxyRequestBuilder<TRequestDto, TResponseDto>(baseUri);

        public ProxyRequestBuilder<TRequestDto, TResponseDto> Accept(params string[] values)
        {
            if (this.proxyRequest.Headers.ContainsKey("Accept"))
            {
                var acceptHeader = this.proxyRequest.Headers["Accept"].ToList();
                acceptHeader.AddRange(values);

                this.proxyRequest.Headers["Accept"] = acceptHeader;
            }
            else
            {
                this.proxyRequest.Headers.Add("Accept", values);
            }

            return this;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddHeader(string header, params string[] values)
        {
            if (this.proxyRequest.Headers.ContainsKey(header))
            {
                var acceptHeader = this.proxyRequest.Headers[header].ToList();
                acceptHeader.AddRange(values);

                this.proxyRequest.Headers[header] = acceptHeader;
            }
            else
            {
                this.proxyRequest.Headers.Add(header, values);
            }

            return this;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddPathArguments(params object[] arguments)
        {
            this.pathArguments.AddRange(arguments);
            return this;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AddQueryParameter(string parameter, object value)
        {
            this.queryParameters.Add(parameter, value);
            return this;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AppendToRoute(string appendage)
        {
            var baseUriString = this.baseUri.ToString();
            var newBaseUriString = $"{baseUriString.TrimEnd('/')}/{appendage}";
            this.baseUri = new Uri(newBaseUriString);

            return this;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsDeleteRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Delete);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsGetRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Get);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsHeadRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Head);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsOptionsRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Options);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsPatchRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Patch);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsPostRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Post);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsPutRequest()
        {
            return this.AsRequestType(HttpRequestMethod.Put);
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> AsRequestType(HttpRequestMethod requestMethod)
        {
            this.proxyRequest.HttpRequestMethod = requestMethod;
            return this;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> Authorization(string scheme, string token)
        {
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

        public IProxyRequest<TRequestDto, TResponseDto> Build()
        {
            var pathArgumentsBuilder = new StringBuilder();
            var queryParametersBuilder = new StringBuilder();
            var requestUri = this.baseUri.ToString();

            if (this.pathArguments.Count > 0)
            {
                foreach (var pathArgument in this.pathArguments)
                {
                    pathArgumentsBuilder.Append($"{pathArgument}/");
                }
            }

            if (this.queryParameters.Count > 0)
            {
                queryParametersBuilder.Append('?');

                foreach (var queryParameter in this.queryParameters.Keys)
                {
                    queryParametersBuilder.Append($"{queryParameter}={this.queryParameters[queryParameter.ToString()]}&");
                }
            }

            this.proxyRequest.RequestUri = new Uri($"{requestUri.TrimEnd('/')}/{pathArgumentsBuilder.ToString().TrimEnd('/')}{queryParametersBuilder.ToString().TrimEnd('&')}".TrimEnd('/'));

            return this.proxyRequest;
        }

        public ProxyRequestBuilder<TRequestDto, TResponseDto> SetRequestDto(TRequestDto requestDto)
        {
            this.proxyRequest.RequestDto = requestDto;
            return this;
        }

        #endregion Public Methods
    }
}
