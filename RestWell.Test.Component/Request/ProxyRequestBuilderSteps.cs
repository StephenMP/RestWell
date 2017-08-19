using RestWell.Client.Request;
using Shouldly;
using System.Reflection;

namespace RestWell.Test.Component.Request
{
    internal class ProxyRequestBuilderSteps
    {
        private string baseUriString;
        private IProxyRequest<Missing, Missing> simpleProxyRequest;

        internal void GivenIHaveABaseUri(string baseUriString)
        {
            this.baseUriString = baseUriString;
        }

        internal void WhenIBuildASimpleProxyRequestUsingString()
        {
            this.simpleProxyRequest = ProxyRequestBuilder.CreateBuilder(this.baseUriString).Build();
        }

        internal void ThenICanBuildSimpleProxyRequestUsingStringUri()
        {
            this.simpleProxyRequest.ShouldNotBeNull();
            this.simpleProxyRequest.RequestUri.ToString().ShouldBe(this.baseUriString);
        }
    }
}