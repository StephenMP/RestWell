using RestWell.Test.Resource.TestEnvironment.Environment;
using System;

namespace RestWell.Test.Integration.Client
{
    public class TestEnvironmentClassFixture<TStartup> : IDisposable where TStartup : class
    {
        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public TestEnvironmentClassFixture()
        {
            this.TestEnvironment = TestEnvironmentBuilder.CreateBuilder()
                                                                .AddResourceWebApi<TStartup>()
                                                                .BuildEnvironment();
        }

        #endregion Public Constructors

        #region Public Properties

        public TestEnvironment TestEnvironment { get; }

        #endregion Public Properties

        #region Public Methods

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
                    this.TestEnvironment?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
