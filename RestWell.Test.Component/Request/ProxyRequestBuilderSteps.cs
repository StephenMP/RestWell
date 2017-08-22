using RestWell.Client.Request;
using RestWell.Domain.Enums;
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
        private HttpRequestMethod requestMethod;
        private Action simpleProxyRequestAction;

        internal void GivenIHaveABaseUri(string baseUriString)
        {
            this.baseUriString = baseUriString;
        }

        internal void WhenIBuildASimpleProxyRequestUsingString()
        {
            this.simpleProxyRequest = ProxyRequestBuilder.CreateBuilder(this.baseUriString, this.requestMethod).Build();
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

        internal void ThenICanVerifyICanProperlyBuildRequestUri()
        {
            this.simpleProxyRequest.RequestUri.ShouldBe(this.completeUri);
        }

        internal void WhenIBuildAProxyRequestForRequestTypeOnly()
        {
            this.simpleProxyRequest = ProxyRequestBuilder
                                        .CreateBuilder(this.baseUriString, this.requestMethod)
                                        .Build();
        }

        internal void GivenIAmUsingTheHttpRequestMethodOf(HttpRequestMethod requestMethod)
        {
            this.requestMethod = requestMethod;
        }

        internal void ThenICanVerifyICanProperlySetRequestType()
        {
            this.simpleProxyRequest.ShouldNotBeNull();
            this.simpleProxyRequest.HttpRequestMethod.ShouldBe(this.requestMethod);
        }

        internal void WhenICreateAProxyRequestAsAnAction()
        {
            this.simpleProxyRequestAction = new Action(() => ProxyRequestBuilder.CreateBuilder(this.baseUriString, this.requestMethod));
        }

        internal void ThenICanVerifyICannotIssueBasicRequestWithUnrecognizedRequestType()
        {
            this.simpleProxyRequestAction.ShouldThrow<ArgumentException>($"requestMethod cannot be {HttpRequestMethod.None}. You must use a valid request type when using AsRequestType");
        }
    }
}