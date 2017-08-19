using Xunit;

namespace RestWell.Test.Component.Request
{
    public class ProxyRequestBuilderFeatures
    {
        private ProxyRequestBuilderSteps steps;

        public ProxyRequestBuilderFeatures()
        {
            this.steps = new ProxyRequestBuilderSteps();
        }

        [Fact]
        public void CanBuildSimpleProxyRequestUsingStringUri()
        {
            this.steps.GivenIHaveABaseUri("http://www.this.is/fake");

            this.steps.WhenIBuildASimpleProxyRequestUsingString();

            this.steps.ThenICanBuildSimpleProxyRequestUsingStringUri();
        }
    }
}
