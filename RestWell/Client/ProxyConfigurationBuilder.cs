using RestWell.Domain.Extensions;
using RestWell.Domain.Request;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace RestWell.Client
{
    public class ProxyConfigurationBuilder
    {
        #region Private Fields

        private readonly ProxyConfiguration proxyConfiguration;

        #endregion Private Fields

        #region Public Constructors

        public ProxyConfigurationBuilder()
        {
            this.proxyConfiguration = new ProxyConfiguration();
        }

        #endregion Public Constructors

        #region Public Methods

        public static ProxyConfigurationBuilder CreateBuilder() => new ProxyConfigurationBuilder();

        /// <summary>
        /// Inserts a delegate into the request pipeline to be executed prior to sending the request.
        /// </summary>
        /// <param name="actionBeforeSendingRequest">The action before sending request.</param>
        /// <returns></returns>
        public ProxyConfigurationBuilder AddDelegatingAction(Action<HttpRequestMessage, CancellationToken> actionBeforeSendingRequest)
        {
            if (actionBeforeSendingRequest == null)
            {
                throw new ArgumentException($"{nameof(actionBeforeSendingRequest)} was null. This value cannot be null when using {nameof(AddDelegatingAction)}");
            }

            return this.AddDelegatingHandlers(new DelegatingAction(actionBeforeSendingRequest));
        }

        /// <summary>
        /// Insert a delegating handler into the request pipeline.
        /// </summary>
        /// <param name="delegatingHandlers"></param>
        /// <returns>A reference to this ProxyConfigurationBuilder</returns>
        public ProxyConfigurationBuilder AddDelegatingHandlers(params DelegatingHandler[] delegatingHandlers)
        {
            if (delegatingHandlers == null || delegatingHandlers.Length == 0)
            {
                throw new ArgumentException($"{nameof(delegatingHandlers)} was null or empty. This value cannot be null or empty when using {nameof(AddDelegatingHandlers)}");
            }

            foreach (var delegatingHandler in delegatingHandlers)
            {
                if (!this.proxyConfiguration.DelegatingHandlers.ContainsKey(delegatingHandler.GetType()))
                {
                    this.proxyConfiguration.DelegatingHandlers.Add(delegatingHandler.GetType(), delegatingHandler);
                }
            }

            return this;
        }

        /// <summary>
        /// Build the ProxyConfiguration.
        /// </summary>
        /// <returns>A ProxyConfiguration based off this ProxyConfigurationBuilder</returns>
        public IProxyConfiguration Build()
        {
            return this.proxyConfiguration;
        }

        /// <summary>
        /// Set the default accept header to be used on every request.
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns>A reference to this ProxyConfigurationBuilder</returns>
        public ProxyConfigurationBuilder UseDefaultAcceptHeader(MediaTypeWithQualityHeaderValue mediaType)
        {
            if (mediaType == null)
            {
                throw new ArgumentException($"{nameof(mediaType)} was null. This value cannot be null when using {nameof(UseDefaultAcceptHeader)}");
            }

            this.proxyConfiguration.DefaultProxyRequestHeaders.Accept.Add(mediaType);
            return this;
        }

        /// <summary>
        /// Set the default Authorization header to be used on every request.
        /// </summary>
        /// <param name="authenticationHeaderValue"></param>
        /// <returns>A reference to this ProxyConfigurationBuilder</returns>
        public ProxyConfigurationBuilder UseDefaultAuthorizationHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            if (authenticationHeaderValue == null)
            {
                throw new ArgumentException($"{nameof(authenticationHeaderValue)} was null. This value cannot be null when using {nameof(UseDefaultAuthorizationHeader)}");
            }

            this.proxyConfiguration.DefaultProxyRequestHeaders.Authorization = authenticationHeaderValue;
            return this;
        }

        /// <summary>
        /// Set a default header and values to be used on every request.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="values"></param>
        /// <returns>A reference to this ProxyConfigurationBuilder</returns>
        public ProxyConfigurationBuilder UseDefaultHeader(string header, params string[] values)
        {
            if (header.IsNullOrEmptyOrWhitespace())
            {
                throw new ArgumentException($"{nameof(header)} was null or empty. This value cannot be null or empty when using {nameof(UseDefaultHeader)}");
            }

            if (values == null || values.Length == 0)
            {
                throw new ArgumentException($"{nameof(values)} was null or empty. This value cannot be null or empty when using {nameof(UseDefaultHeader)}");
            }

            this.proxyConfiguration.DefaultProxyRequestHeaders.AdditionalHeaders.Add(header, values);
            return this;
        }

        #endregion Public Methods
    }
}
