using RestWell.Test.Resource.TestEnvironment.Environment.Configuration;

namespace RestWell.Test.Resource.TestEnvironment.Environment
{
    public class TestEnvironmentBuilder
    {
        #region Private Fields

        private ResourceWebApiConfiguration resourceWebServiceConfiguration;

        #endregion Private Fields

        #region Public Methods

        public static TestEnvironmentBuilder CreateBuilder() => new TestEnvironmentBuilder();

        public TestEnvironmentBuilder AddResourceWebApi<TStartup>() where TStartup : class
        {
            this.resourceWebServiceConfiguration = this.resourceWebServiceConfiguration ?? new ResourceWebApiConfiguration();
            this.resourceWebServiceConfiguration.AddWebService<TStartup>();
            return this;
        }

        public TestEnvironment BuildEnvironment()
        {
            return new TestEnvironment(resourceWebServiceConfiguration);
        }

        #endregion Public Methods
    }
}
