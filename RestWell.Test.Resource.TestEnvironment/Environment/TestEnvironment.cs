using RestWell.Test.Resource.TestEnvironment.Environment.Configuration;
using RestWell.Test.Resource.TestEnvironment.WebApi;
using System;

namespace RestWell.Test.Resource.TestEnvironment.Environment
{
    public class TestEnvironment : IDisposable
    {
        #region Private Fields

        private readonly ResourceWebApiConfiguration resourceWebApiConfiguration;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public TestEnvironment(ResourceWebApiConfiguration resourceWebApiConfiguration)
        {
            this.resourceWebApiConfiguration = resourceWebApiConfiguration;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IResourceWebApi GetResourceWebService<TStartup>() where TStartup : class
        {
            return this.resourceWebApiConfiguration?[typeof(TStartup)];
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.resourceWebApiConfiguration?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
