using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RestWell.Domain.Factories
{
    internal static class HttpClientFactory
    {
        #region Public Methods

        public static HttpClient Create(IEnumerable<DelegatingHandler> delegatingHandlers)
        {
            var client = Create(new HttpClientHandler(), delegatingHandlers.ToArray());
            return client;
        }

        #endregion Public Methods

        #region Private Methods

        private static HttpClient Create(HttpMessageHandler innerHandler, params DelegatingHandler[] handlers)
        {
            var pipeline = CreatePipeline(innerHandler, handlers);
            return new HttpClient(pipeline);
        }

        private static HttpMessageHandler CreatePipeline(HttpMessageHandler innerHandler, IEnumerable<DelegatingHandler> handlers)
        {
            if (innerHandler == null)
            {
                throw new ArgumentNullException(nameof(innerHandler));
            }

            if (handlers == null)
            {
                return innerHandler;
            }

            var pipeline = innerHandler;
            var reversedHandlers = handlers.Reverse();
            foreach (var handler in reversedHandlers)
            {
                if (handler == null)
                {
                    throw new ArgumentException(nameof(handlers), "DelegatingHandlerArrayContainsNullItem " + typeof(DelegatingHandler).Name);
                }

                if (handler.InnerHandler != null)
                {
                    throw new ArgumentException(nameof(handlers), "DelegatingHandlerArrayHasNonNullInnerHandler " + typeof(DelegatingHandler).Name + " InnerHandler " + handler.GetType().Name);
                }

                handler.InnerHandler = pipeline;
                pipeline = handler;
            }

            return pipeline;
        }

        #endregion Private Methods
    }
}
