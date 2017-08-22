using RestWell.Client.Request;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RestWell.Test.Component.Request
{
    internal class ProxyRequestBuilderSteps
    {
        private string baseUriString;
        private IProxyRequest<Missing, Missing> simpleProxyRequest;
        private Uri completeUri;

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

        internal void GivenIHaveACompleteUri(Uri completeUri)
        {
            this.completeUri = completeUri;
        }

        internal void WhenIBuildAProxyRequest(IEnumerable<string> appendages, IEnumerable<string> pathArguments, IDictionary<string, string> queryParameters)
        {
            var proxyRequestBuilder = ProxyRequestBuilder
                                    .CreateBuilder(this.baseUriString)
                                    .AppendToRoute(appendages.ToArray())
                                    .AddPathArguments(pathArguments.ToArray());

            foreach (var queryParam in queryParameters)
            {
                proxyRequestBuilder.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            this.simpleProxyRequest = proxyRequestBuilder.Build();
        }

        internal void ThenICanVerifyICanProperlyBuildRequestUri()
        {
            this.simpleProxyRequest.RequestUri.ShouldBe(this.completeUri);
        }
    }
}