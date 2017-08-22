using RestWell.Client;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Domain.Enums;
using RestWell.Domain.Proxy;
using RestWell.Test.Resource.TestEnvironment.Environment;
using RestWell.Test.Resource.WebApi.Controllers;
using RestWell.Test.Resource.WebApi.Dtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace RestWell.Test.Integration.Client
{
    internal class ProxySteps : IDisposable
    {
        #region Private Fields

        private string acceptHeaderValue;
        private string basicMessage;
        private IProxyRequest<Missing, string> basicRequestProxyRequest;
        private IProxyResponse<string> basicRequestProxyResponse;
        private Dictionary<Type, List<object>> delegatingHandlers;
        private bool disposedValue;
        private HttpRequestMethod httpRequestMethod;
        private IProxyRequest<MessageRequestDto, MessageResponseDto> messageDtoRequestProxyRequest;
        private IProxyResponse<MessageResponseDto> messageDtoRequestProxyResponse;
        private IProxyRequest<Missing, MessageResponseDto> messageDtoResponseRequestProxyRequest;
        private IProxyResponse<MessageResponseDto> messageDtoResponseRequestProxyResponse;
        private MessageRequestDto messageRequestDto;
        private IProxy proxy;
        private IProxyConfiguration proxyConfiguration;
        private IProxyRequest<Missing, MessageResponseDto> secureRequestProxyRequest;
        private IProxyResponse<MessageResponseDto> secureRequestProxyResponse;
        private TestEnvironment testEnvironment;
        private AuthenticationHeaderValue defaultAuthorizationHeader;
        private MediaTypeWithQualityHeaderValue defaultAcceptHeader;

        #endregion Private Fields

        #region Public Constructors

        public ProxySteps(TestEnvironment testEnvironment)
        {
            this.delegatingHandlers = new Dictionary<Type, List<object>>();
            this.testEnvironment = testEnvironment;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void GivenIAccept(string acceptHeaderValue)
        {
            this.acceptHeaderValue = acceptHeaderValue;
        }

        internal void GivenIAmUsingTheHttpRequestMethodOf(HttpRequestMethod requestMethod)
        {
            this.httpRequestMethod = requestMethod;
        }

        internal void GivenIHaveABasicRequestMessage(string message)
        {
            this.basicMessage = message;
        }

        internal void GivenIHaveABasicRequestProxyRequestForNonExistingResource()
        {
            this.basicRequestProxyRequest = ProxyRequestBuilder<string>
                                                .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                .Accept(this.acceptHeaderValue)
                                                .AppendToRoute($"api/does/not/exist/{nameof(BasicRequestController).Replace("Controller", "")}")
                                                .AddPathArguments(this.basicMessage)
                                                .Build();
        }

        internal void ThenICanVerifyICannotIssueBasicRequestForNonExistingResource()
        {
            this.basicRequestProxyResponse.ShouldNotBeNull();
            this.basicRequestProxyResponse.HttpRequestMethod.ShouldBe(httpRequestMethod);
            this.basicRequestProxyResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            this.basicRequestProxyResponse.IsSuccessfulStatusCode.ShouldBe(false);
            this.basicRequestProxyResponse.RequestUri.ToString().ShouldBe(this.basicRequestProxyResponse.RequestUri.ToString());
        }

        internal void GivenIHaveABasicRequestProxyRequest()
        {
            this.basicRequestProxyRequest = ProxyRequestBuilder<string>
                                                .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                .Accept(this.acceptHeaderValue)
                                                .AppendToRoute($"api/{nameof(BasicRequestController).Replace("Controller", "")}")
                                                .AddPathArguments(this.basicMessage)
                                                .Build();
        }

        internal void GivenIHaveABasicRequestProxyRequestWithNoAcceptHeader()
        {
            this.basicRequestProxyRequest = ProxyRequestBuilder<string>
                                                .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                .AppendToRoute($"api/{nameof(BasicRequestController).Replace("Controller", "")}")
                                                .AddPathArguments(this.basicMessage)
                                                .Build();
        }

        internal void GivenIHaveADefaultAcceptHeader(string mediaType)
        {
            this.defaultAcceptHeader = new MediaTypeWithQualityHeaderValue(mediaType);
        }

        internal void ThenICanVerifyICanIssueBasicRequestWithDefaultAcceptHeader()
        {
            this.basicRequestProxyResponse.RequestHeaders.Accept.ShouldContain(this.defaultAcceptHeader);
        }

        internal void GivenIHaveABasicRequestProxyRequestForException()
        {
            this.basicRequestProxyRequest = ProxyRequestBuilder<string>
                                                .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                .Accept(this.acceptHeaderValue)
                                                .AppendToRoute($"api/{nameof(BasicRequestController).Replace("Controller", "")}/error")
                                                .AddPathArguments(this.basicMessage)
                                                .Build();
        }

        internal void ThenICanVerifyICannotIssueBasicRequestDueToException()
        {
            this.basicRequestProxyResponse.ShouldNotBeNull();
            this.basicRequestProxyResponse.HttpRequestMethod.ShouldBe(httpRequestMethod);
            this.basicRequestProxyResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            this.basicRequestProxyResponse.IsSuccessfulStatusCode.ShouldBe(false);
            this.basicRequestProxyResponse.RequestUri.ToString().ShouldBe(this.basicRequestProxyResponse.RequestUri.ToString());
        }

        internal void GivenIHaveAMessageDtoRequestProxyRequest()
        {
            this.messageDtoRequestProxyRequest = ProxyRequestBuilder<MessageRequestDto, MessageResponseDto>
                                                    .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                    .Accept(this.acceptHeaderValue)
                                                    .AppendToRoute($"api/{nameof(MessageDtoRequestController).Replace("Controller", "")}")
                                                    .SetRequestDto(this.messageRequestDto)
                                                    .Build();
        }

        internal void GivenIHaveASecureRequestProxyRequestWithNoAuthHeader()
        {
            this.secureRequestProxyRequest = ProxyRequestBuilder<MessageResponseDto>
                                                .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                .Accept(this.acceptHeaderValue)
                                                .AppendToRoute($"api/{nameof(SecureRequestController).Replace("Controller", "")}")
                                                .AddPathArguments(this.basicMessage)
                                                .Build();
        }

        internal void ThenICanVerifyICannotIssueSecureRequestDueNoAuthHeader()
        {
            this.secureRequestProxyResponse.ShouldNotBeNull();
            this.secureRequestProxyResponse.HttpRequestMethod.ShouldBe(httpRequestMethod);
            this.secureRequestProxyResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
            this.secureRequestProxyResponse.IsSuccessfulStatusCode.ShouldBe(false);
            this.secureRequestProxyResponse.RequestUri.ToString().ShouldBe(this.secureRequestProxyResponse.RequestUri.ToString());
        }

        internal void GivenIHaveADefaultAuthorizationHeader()
        {
            this.defaultAuthorizationHeader = new AuthenticationHeaderValue("Basic", "Username:Password");
        }

        internal void GivenIHaveAMessageDtoResponseRequestProxyRequest()
        {
            this.messageDtoResponseRequestProxyRequest = ProxyRequestBuilder<MessageResponseDto>
                                                            .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                            .Accept(this.acceptHeaderValue)
                                                            .AppendToRoute($"api/{nameof(MessageDtoResponseRequestController).Replace("Controller", "")}")
                                                            .AddPathArguments(this.basicMessage)
                                                            .Build();
        }

        internal void GivenIHaveAMessageRequestDto(string message)
        {
            this.basicMessage = message;
            this.messageRequestDto = new MessageRequestDto { Message = this.basicMessage };
        }

        internal void GivenIHaveAProxy()
        {
            this.proxy = new Proxy(this.proxyConfiguration);
        }

        internal void GivenIHaveAProxyConfiguration()
        {
            var proxyConfigurationBuilder = ProxyConfigurationBuilder.CreateBuilder();

            if (this.delegatingHandlers != null)
            {
                foreach (var delegatingHandler in this.delegatingHandlers)
                {
                    proxyConfigurationBuilder.AddDelegatingHandler(delegatingHandler.Key, delegatingHandler.Value.ToArray());
                }
            }

            if (this.defaultAuthorizationHeader != null)
            {
                proxyConfigurationBuilder.UseDefaultAuthorizationHeader(this.defaultAuthorizationHeader);
            }

            if (this.defaultAcceptHeader != null)
            {
                proxyConfigurationBuilder.UseDefaultAcceptHeader(this.defaultAcceptHeader);
            }

            this.proxyConfiguration = proxyConfigurationBuilder.Build();
        }

        internal void GivenIHaveASecureRequestDelegatingHandler()
        {
            this.delegatingHandlers.Add(typeof(SecureRequestDelegatingHandler), new List<object>());
        }

        internal void GivenIHaveASecureRequestDelegatingHandler(string scheme, string token)
        {
            this.delegatingHandlers.Add(typeof(SecureRequestDelegatingHandler), new List<object> { scheme, token });
        }

        internal void GivenIHaveASecureRequestProxyRequest()
        {
            this.secureRequestProxyRequest = ProxyRequestBuilder<MessageResponseDto>
                                                .CreateBuilder(this.testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri, this.httpRequestMethod)
                                                .AddHeader("Accept", this.acceptHeaderValue)
                                                .AppendToRoute($"api/{nameof(SecureRequestController).Replace("Controller", "")}")
                                                .AddPathArguments(this.basicMessage)
                                                .Build();
        }

        internal void ThenICanVerifyICanIssueBasicRequest()
        {
            this.ThenICanVerifyIGotAResponse(this.basicRequestProxyResponse, this.basicRequestProxyRequest, HttpStatusCode.OK, true);

            if (this.httpRequestMethod != HttpRequestMethod.Head)
            {
                this.basicRequestProxyResponse.ResponseDto.ShouldNotBeNull();
                this.basicRequestProxyResponse.ResponseDto.ShouldBeOfType<string>();
                this.basicRequestProxyResponse.ResponseDto.ShouldNotBeEmpty();
                this.basicRequestProxyResponse.ResponseDto.ShouldBe(this.basicMessage);
            }
        }

        internal void ThenICanVerifyICanIssueMessageDtoRequest()
        {
            this.ThenICanVerifyIGotAResponse(this.messageDtoRequestProxyResponse, this.messageDtoRequestProxyRequest, HttpStatusCode.OK, true);

            if (this.httpRequestMethod != HttpRequestMethod.Head)
            {
                this.messageDtoRequestProxyResponse.ResponseDto.ShouldNotBeNull();
                this.messageDtoRequestProxyResponse.ResponseDto.ShouldBeOfType<MessageResponseDto>();
                this.messageDtoRequestProxyResponse.ResponseDto.Message.ShouldNotBeNull();
                this.messageDtoRequestProxyResponse.ResponseDto.Message.ShouldNotBeEmpty();
                this.messageDtoRequestProxyResponse.ResponseDto.Message.ShouldBe(this.basicMessage);
            }
        }

        internal void ThenICanVerifyICanIssueMessageDtoResponseRequest()
        {
            this.ThenICanVerifyIGotAResponse(this.messageDtoResponseRequestProxyResponse, this.messageDtoResponseRequestProxyRequest, HttpStatusCode.OK, true);

            if (this.httpRequestMethod != HttpRequestMethod.Head)
            {
                this.messageDtoResponseRequestProxyResponse.ResponseDto.ShouldNotBeNull();
                this.messageDtoResponseRequestProxyResponse.ResponseDto.ShouldBeOfType<MessageResponseDto>();
                this.messageDtoResponseRequestProxyResponse.ResponseDto.Message.ShouldNotBeNull();
                this.messageDtoResponseRequestProxyResponse.ResponseDto.Message.ShouldNotBeEmpty();
                this.messageDtoResponseRequestProxyResponse.ResponseDto.Message.ShouldBe(this.basicMessage);
            }
        }

        internal void ThenICanVerifyICanIssueSecureRequestUsingDelegatingHandler()
        {
            this.ThenICanVerifyIGotAResponse(this.secureRequestProxyResponse, this.secureRequestProxyRequest, HttpStatusCode.OK, true);

            if (this.httpRequestMethod != HttpRequestMethod.Head)
            {
                this.secureRequestProxyResponse.ResponseDto.ShouldNotBeNull();
                this.secureRequestProxyResponse.ResponseDto.ShouldBeOfType<MessageResponseDto>();
                this.secureRequestProxyResponse.ResponseDto.Message.ShouldNotBeNull();
                this.secureRequestProxyResponse.ResponseDto.Message.ShouldNotBeEmpty();
                this.secureRequestProxyResponse.ResponseDto.Message.ShouldBe(this.basicMessage);
            }
        }

        internal void ThenICanVerifyIHaveAProxy()
        {
            this.proxy.ShouldNotBeNull();
        }

        internal async Task WhenIInvokeAsyncForBasicRequest(bool runAsync)
        {
            if (runAsync)
            {
                this.basicRequestProxyResponse = await this.proxy.InvokeAsync(this.basicRequestProxyRequest);
            }

            else
            {
                this.basicRequestProxyResponse = this.proxy.Invoke(this.basicRequestProxyRequest);
            }
        }

        internal async Task WhenIInvokeAsyncForMessageDtoRequest(bool runAsync)
        {
            if (runAsync)
            {
                this.messageDtoRequestProxyResponse = await this.proxy.InvokeAsync(this.messageDtoRequestProxyRequest);
            }

            else
            {
                this.messageDtoRequestProxyResponse = this.proxy.Invoke(this.messageDtoRequestProxyRequest);
            }
        }

        internal async Task WhenIInvokeAsyncForMessageDtoResponseRequest(bool runAsync)
        {
            if (runAsync)
            {
                this.messageDtoResponseRequestProxyResponse = await this.proxy.InvokeAsync(this.messageDtoResponseRequestProxyRequest);
            }

            else
            {
                this.messageDtoResponseRequestProxyResponse = this.proxy.Invoke(this.messageDtoResponseRequestProxyRequest);
            }
        }

        internal async Task WhenIInvokeAsyncForSecureRequest(bool runAsync)
        {
            if (runAsync)
            {
                this.secureRequestProxyResponse = await this.proxy.InvokeAsync(this.secureRequestProxyRequest);
            }

            else
            {
                this.secureRequestProxyResponse = this.proxy.Invoke(this.secureRequestProxyRequest);
            }
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void ThenICanVerifyIGotAResponse<TRequestDto, TResponseDto>(IProxyResponse<TResponseDto> proxyResponse, IProxyRequest<TRequestDto, TResponseDto> proxyRequest, HttpStatusCode statusCode, bool isSuccessfulStatusCode)
        {
            proxyResponse.ShouldNotBeNull();

            proxyResponse.HttpRequestMethod.ShouldBe(httpRequestMethod);

            proxyResponse.StatusCode.ShouldBe(statusCode);

            proxyResponse.IsSuccessfulStatusCode.ShouldBe(isSuccessfulStatusCode);

            proxyResponse.RequestHeaders.ShouldNotBeNull();
            proxyResponse.RequestHeaders.ShouldNotBeEmpty();

            proxyResponse.RequestUri.ToString().ShouldBe(proxyRequest.RequestUri.ToString());

            proxyResponse.ResponseHeaders.ShouldNotBeNull();
            proxyResponse.ResponseHeaders.ShouldNotBeEmpty();
        }

        #endregion Private Methods
    }
}
