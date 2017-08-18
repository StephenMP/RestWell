using RestWell.Domain.Enums;
using RestWell.Domain.Proxy;
using System;
using System.Collections.Generic;

namespace RestWell.Domain.Invokers
{
    internal sealed class RequestInvoker : Dictionary<HttpRequestMethod, Invoker>
    {
        #region Public Constructors

        public RequestInvoker(IProxyConfiguration proxyConfiguration)
        {
            foreach (HttpRequestMethod requestMethod in Enum.GetValues(typeof(HttpRequestMethod)))
            {
                this.Add(requestMethod, this.CreateInvoker(requestMethod, proxyConfiguration));
            }
        }

        #endregion Public Constructors

        #region Private Methods

        private Invoker CreateInvoker(HttpRequestMethod requestType, IProxyConfiguration proxyConfiguration)
        {
            switch (requestType)
            {
                case HttpRequestMethod.Delete:
                    return new DeleteInvoker(proxyConfiguration);

                case HttpRequestMethod.Get:
                    return new GetInvoker(proxyConfiguration);

                case HttpRequestMethod.Head:
                    return new HeadInvoker(proxyConfiguration);

                case HttpRequestMethod.Options:
                    return new OptionsInvoker(proxyConfiguration);

                case HttpRequestMethod.Patch:
                    return new PatchInvoker(proxyConfiguration);

                case HttpRequestMethod.Post:
                    return new PostInvoker(proxyConfiguration);

                case HttpRequestMethod.Put:
                    return new PutInvoker(proxyConfiguration);

                default:
                    return new UnknownInvoker();
            }
        }

        #endregion Private Methods
    }
}
