using Microsoft.AspNetCore.Hosting;
using System;

namespace RestWell.Test.Resource.TestEnvironment.WebApi
{
    public interface IResourceWebApi : IDisposable
    {
        #region Public Properties

        Uri BaseUri { get; }

        #endregion Public Properties
    }

    public class ResourceWebApi : IResourceWebApi
    {
        #region Private Fields

        private readonly IWebHost webHost;
        private bool disposedValue;

        #endregion Private Fields

        #region Internal Constructors

        internal ResourceWebApi(Uri baseUri, IWebHost webHost)
        {
            this.BaseUri = baseUri;
            this.webHost = webHost;
        }

        #endregion Internal Constructors

        #region Public Properties

        public Uri BaseUri { get; }

        #endregion Public Properties

        #region Public Methods

        public static IResourceWebApi Create<TStartup>(Uri baseUri) where TStartup : class
        {
            var webHost = new WebHostBuilder()
                            .UseKestrel()
                            .UseStartup<TStartup>()
                            .Start(baseUri.AbsoluteUri);

            return new ResourceWebApi(baseUri, webHost);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.webHost?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
