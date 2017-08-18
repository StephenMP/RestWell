using RestWell.Test.Resource.TestEnvironment.WebApi;
using System;
using System.Collections.Generic;

namespace RestWell.Test.Resource.TestEnvironment.Environment.Configuration
{
    public class ResourceWebApiConfiguration : Dictionary<Type, IResourceWebApi>, IDisposable
    {
        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Methods

        public void AddWebService<TStartup>() where TStartup : class
        {
            var baseUri = new Uri($"http://localhost:{TcpPortUtility.GetFreeTcpPort()}");
            this.Add(typeof(TStartup), ResourceWebApi.Create<TStartup>(baseUri));
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
                    foreach (var webHost in this.Values)
                    {
                        webHost?.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
