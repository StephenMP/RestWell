using RestWell.Client.Enums;
using RestWell.Client.Request;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RestWell.Test.Component.Client.Testing
{
    internal class ProxyRequestBuilderSteps
    {
        #region Private Fields

        private string baseUriString;
        private Uri completeUri;
        private HttpRequestMethod requestMethod;
        private IProxyRequest<Missing, Missing> simpleProxyRequest;
        private Action simpleProxyRequestAction;

        #endregion Private Fields

        #region Internal Methods

        internal void GivenIAmUsingTheHttpRequestMethodOf(HttpRequestMethod requestMethod)
        {
            this.requestMethod = requestMethod;
        }

        internal void GivenIHaveABaseUri(string baseUriString)
        {
            this.baseUriString = baseUriString;
        }

        internal void GivenIHaveACompleteUri(Uri completeUri)
        {
            this.completeUri = completeUri;
        }

        internal void ThenICanBuildSimpleProxyRequestUsingStringUri()
        {
            this.simpleProxyRequest.ShouldNotBeNull();
            this.simpleProxyRequest.RequestUri.ToString().ShouldBe(this.baseUriString);
        }

        internal void ThenICanVerifyICannotIssueBasicRequestWithUnrecognizedRequestType()
        {
            this.simpleProxyRequestAction.ShouldThrow<ArgumentException>($"requestMethod cannot be {HttpRequestMethod.None}. You must use a valid request type when using AsRequestType");
        }

        internal void ThenICanVerifyICanProperlyBuildRequestUri()
        {
            this.simpleProxyRequest.RequestUri.ShouldBe(this.completeUri);
        }

        internal void ThenICanVerifyICanProperlySetRequestType()
        {
            this.simpleProxyRequest.ShouldNotBeNull();
            this.simpleProxyRequest.HttpRequestMethod.ShouldBe(this.requestMethod);
        }

        internal void WhenIBuildAProxyRequestForRequestTypeOnly()
        {
            this.simpleProxyRequest = ProxyRequestBuilder
                                        .CreateBuilder(this.baseUriString, this.requestMethod)
                                        .Build();
        }

        internal void WhenIBuildAProxyRequestForUriOnly(IEnumerable<string> appendages, IEnumerable<string> pathArguments, IDictionary<string, string> queryParameters)
        {
            var proxyRequestBuilder = ProxyRequestBuilder
                                    .CreateBuilder(this.baseUriString, this.requestMethod)
                                    .AppendToRoute(appendages.ToArray())
                                    .AddPathArguments(pathArguments.ToArray());

            foreach (var queryParam in queryParameters)
            {
                proxyRequestBuilder.AddQueryParameter(queryParam.Key, queryParam.Value);
            }

            this.simpleProxyRequest = proxyRequestBuilder.Build();
        }

        internal void WhenIBuildASimpleProxyRequestUsingString()
        {
            this.simpleProxyRequest = ProxyRequestBuilder.CreateBuilder(this.baseUriString, this.requestMethod).Build();
        }

        internal void WhenICreateAProxyRequestAsAnAction()
        {
            this.simpleProxyRequestAction = new Action(() => ProxyRequestBuilder.CreateBuilder(this.baseUriString, this.requestMethod));
        }

        #endregion Internal Methods
    }
}
