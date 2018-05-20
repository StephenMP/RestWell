using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestWell.Client.Enums;
using Xunit;

namespace RestWell.Test.Integration.Client
{
    public class ProxyFeatures : IClassFixture<TestEnvironmentClassFixture<Resource.WebApi.Startup>>, IDisposable
    {
        #region Private Fields

        private bool disposedValue;
        private ProxySteps steps;

        #endregion Private Fields

        #region Public Constructors

        public ProxyFeatures(TestEnvironmentClassFixture<Resource.WebApi.Startup> testEnvironmentClassFixture)
        {
            this.steps = new ProxySteps(testEnvironmentClassFixture.TestEnvironment);
        }

        #endregion Public Constructors

        #region Public Methods

        public static IEnumerable<object[]> GetGenericTestData(bool includeHeadRequest)
        {
            var testData = new List<object[]>();
            var methods = new List<HttpRequestMethod> { HttpRequestMethod.Delete, HttpRequestMethod.Get, HttpRequestMethod.Options, HttpRequestMethod.Patch, HttpRequestMethod.Post, HttpRequestMethod.Put };
            if (includeHeadRequest)
            {
                methods.Add(HttpRequestMethod.Head);
            }

            var accepts = new[] { "application/json", "application/xml" };

            foreach (var method in methods)
            {
                foreach (var accept in accepts)
                {
                    testData.Add(new object[] { method, Guid.NewGuid().ToString(), accept });
                }
            }

            return testData;
        }

        [Fact]
        public void CanCreateProxy()
        {
            this.steps.GivenIHaveAProxy();
            this.steps.ThenICanVerifyIHaveAProxy();
        }

        [Fact]
        public void CanCreateProxyUsingDefaultConstructor()
        {
            this.steps.GivenIHaveAProxyUsingDefaultConstructor();
            this.steps.ThenICanVerifyIHaveAProxy();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueBasicRequest(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveABasicRequestProxyRequest();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForBasicRequest();

            this.steps.ThenICanVerifyICanIssueBasicRequest();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueBasicRequestWithDefaultAcceptHeader(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIHaveABasicRequestProxyRequestWithNoAcceptHeader();
            this.steps.GivenIHaveADefaultAcceptHeader(acceptHeaderValue);
            this.steps.GivenIHaveAProxyConfiguration();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForBasicRequest();

            this.steps.ThenICanVerifyICanIssueBasicRequest();
            this.steps.ThenICanVerifyICanIssueBasicRequestWithDefaultAcceptHeader();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), false)]
        public async Task CanIssueMessageDtoRequest(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveAMessageRequestDto(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveAMessageDtoRequestProxyRequest();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForMessageDtoRequest();

            this.steps.ThenICanVerifyICanIssueMessageDtoRequest();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueMessageDtoResponseRequest(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveAMessageDtoResponseRequestProxyRequest();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForMessageDtoResponseRequest();

            this.steps.ThenICanVerifyICanIssueMessageDtoResponseRequest();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueMultipleSecureRequestsUsingDelegatingHandler(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveASecureRequestDelegatingHandler();
            this.steps.GivenIHaveASecureRequestProxyRequest();
            this.steps.GivenIHaveAProxyConfiguration();
            this.steps.GivenIHaveAProxy();

            for (int i = 0; i < 5; i++)
            {
                await this.steps.WhenIInvokeAsyncForSecureRequest();

                this.steps.ThenICanVerifyICanIssueSecureRequestUsingDelegatingHandler();
            }
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueSecureRequestsUsingDelegatingAction(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveASecureRequestDelegatingAction();
            this.steps.GivenIHaveASecureRequestProxyRequest();
            this.steps.GivenIHaveAProxyConfiguration();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForSecureRequest();

            this.steps.ThenICanVerifyICanIssueSecureRequestUsingDelegatingHandler();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueSecureRequestUsingDefaultAuthorizationHeader(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveASecureRequestProxyRequest();
            this.steps.GivenIHaveADefaultAuthorizationHeader();
            this.steps.GivenIHaveAProxyConfiguration();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForSecureRequest();

            this.steps.ThenICanVerifyICanIssueSecureRequestUsingDelegatingHandler();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueSecureRequestUsingDelegatingHandler(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveASecureRequestDelegatingHandler();
            this.steps.GivenIHaveASecureRequestProxyRequest();
            this.steps.GivenIHaveAProxyConfiguration();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForSecureRequest();

            this.steps.ThenICanVerifyICanIssueSecureRequestUsingDelegatingHandler();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CanIssueSecureRequestUsingDelegatingHandlerWithArguments(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveASecureRequestDelegatingHandler("Basic", "Username:Password");
            this.steps.GivenIHaveASecureRequestProxyRequest();
            this.steps.GivenIHaveAProxyConfiguration();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForSecureRequest();

            this.steps.ThenICanVerifyICanIssueSecureRequestUsingDelegatingHandler();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CannotIssueBasicRequestDueToException(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveABasicRequestProxyRequestForException();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForBasicRequest();

            this.steps.ThenICanVerifyICannotIssueBasicRequestDueToException();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CannotIssueBasicRequestForNonExistingResource(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveABasicRequestProxyRequestForNonExistingResource();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForBasicRequest();

            this.steps.ThenICanVerifyICannotIssueBasicRequestForNonExistingResource();
        }

        [Theory]
        [MemberData(nameof(GetGenericTestData), true)]
        public async Task CannotIssueSecureRequestDueNoAuthHeader(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveASecureRequestProxyRequestWithNoAuthHeader();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForSecureRequest();

            this.steps.ThenICanVerifyICannotIssueSecureRequestDueNoAuthHeader();
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
                    this.steps?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
