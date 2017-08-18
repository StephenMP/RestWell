using RestWell.Domain.Enums;
using System;
using System.Threading.Tasks;
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

        [Fact]
        public void CanCreateProxy()
        {
            this.steps.GivenIHaveAProxy();
            this.steps.ThenICanVerifyIHaveAProxy();
        }

        [Theory]
        [InlineData(HttpRequestMethod.None, "Test", "application/json")]
        [InlineData(HttpRequestMethod.None, "Test", "application/xml")]
        public async Task CannotIssueBasicRequestWithUnrecognizedRequestType(HttpRequestMethod requestMethod, string message, string acceptHeaderValue)
        {
            this.steps.GivenIAmUsingTheHttpRequestMethodOf(requestMethod);
            this.steps.GivenIHaveABasicRequestMessage(message);
            this.steps.GivenIAccept(acceptHeaderValue);
            this.steps.GivenIHaveABasicRequestProxyRequest();
            this.steps.GivenIHaveAProxy();

            await this.steps.WhenIInvokeAsyncForBasicRequest();

            this.steps.ThenICanVerifyICannotIssueBasicRequestWithUnrecognizedRequestType();
        }

        [Theory]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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

        [Theory]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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
        [InlineData(HttpRequestMethod.Delete, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/json")]
        [InlineData(HttpRequestMethod.Delete, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Get, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Head, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Options, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Patch, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Post, "Test", "application/xml")]
        [InlineData(HttpRequestMethod.Put, "Test", "application/xml")]
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
