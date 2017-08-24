using System.Net.Http;
using System.Net.Http.Headers;

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
        /// Insert a delegating handler into the request pipeline
        /// </summary>
        /// <param name="delegatingHandlers"></param>
        /// <returns>A reference to this ProxyConfigurationBuilder</returns>
        public ProxyConfigurationBuilder AddDelegatingHandlers(params DelegatingHandler[] delegatingHandlers)
        {
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
        /// Build the ProxyConfiguration
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
            this.proxyConfiguration.DefaultProxyRequestHeaders.AdditionalHeaders.Add(header, values);
            return this;
        }

        #endregion Public Methods
    }
}
