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
using System.Net.Http;
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
        private bool disposedValue;
        private HttpRequestMethod httpRequestMethod;
        private IProxyRequest<MessageRequestDto, MessageResponseDto> messageDtoRequestProxyRequest;
        private IProxyResponse<MessageResponseDto> messageDtoRequestProxyResponse;
        private IProxyRequest<Missing, MessageResponseDto> messageDtoResponseRequestProxyRequest;
        private IProxyResponse<MessageResponseDto> messageDtoResponseRequestProxyResponse;
        private MessageRequestDto messageRequestDto;
        private IProxy proxy;
        private TestEnvironment testEnvironment;
        private IProxyRequest<Missing, MessageResponseDto> secureRequestProxyRequest;
        private IProxyConfiguration proxyConfiguration;
        private List<DelegatingHandler> delegatingHandlers;
        private IProxyResponse<MessageResponseDto> secureRequestProxyResponse;

        #endregion Private Fields

        public ProxySteps()
        {
            this.delegatingHandlers = new List<DelegatingHandler>();
        }

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

        internal void GivenIHaveABasicRequestProxyRequest()
        {
            this.basicRequestProxyRequest = new ProxyRequestBuilder<string>(testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri)
                              .AddHeader("Accept", this.acceptHeaderValue)
                              .AppendToRoute($"api/{nameof(BasicRequestController).Replace("Controller", "")}")
                              .AddPathArguments(this.basicMessage)
                              .AsRequestType(this.httpRequestMethod)
                              .Build();
        }

        internal void GivenIHaveAMessageDtoRequestProxyRequest()
        {
            this.messageDtoRequestProxyRequest = new ProxyRequestBuilder<MessageRequestDto, MessageResponseDto>(testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri)
                              .AddHeader("Accept", this.acceptHeaderValue)
                              .AppendToRoute($"api/{nameof(MessageDtoRequestController).Replace("Controller", "")}")
                              .SetRequestDto(this.messageRequestDto)
                              .AsRequestType(this.httpRequestMethod)
                              .Build();
        }

        internal void GivenIHaveAMessageDtoResponseRequestProxyRequest()
        {
            this.messageDtoResponseRequestProxyRequest = new ProxyRequestBuilder<MessageResponseDto>(testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri)
                              .AddHeader("Accept", this.acceptHeaderValue)
                              .AppendToRoute($"api/{nameof(MessageDtoResponseRequestController).Replace("Controller", "")}")
                              .AddPathArguments(this.basicMessage)
                              .AsRequestType(this.httpRequestMethod)
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

        internal void GivenIHaveATestEnvironment()
        {
            this.testEnvironment = TestEnvironmentBuilder.CreateBuilder()
                                                         .AddResourceWebApi<Resource.WebApi.Startup>()
                                                         .BuildEnvironment();
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

        internal async Task WhenIInvokeAsyncForSecureRequest()
        {
            this.secureRequestProxyResponse = await this.proxy.InvokeAsync(this.secureRequestProxyRequest);
        }

        internal void GivenIHaveAProxyConfiguration()
        {
            var proxyConfigurationBuilder = ProxyConfigurationBuilder.CreateBuilder();

            if (this.delegatingHandlers != null)
            {
                proxyConfigurationBuilder.AddDelegatingHandlers(this.delegatingHandlers.ToArray());
            }

            this.proxyConfiguration = proxyConfigurationBuilder.Build();
        }

        internal void GivenIHaveASecureRequestProxyRequest()
        {
            this.secureRequestProxyRequest = new ProxyRequestBuilder<Missing, MessageResponseDto>(testEnvironment.GetResourceWebService<Resource.WebApi.Startup>().BaseUri)
                                          .AddHeader("Accept", this.acceptHeaderValue)
                                          .AppendToRoute($"api/{nameof(SecureRequestController).Replace("Controller", "")}")
                                          .AddPathArguments(this.basicMessage)
                                          .AsRequestType(this.httpRequestMethod)
                                          .Build();
        }

        internal void GivenIHaveASecureRequestDelegatingHandler()
        {
            var secureRequestDelegatingHandler = new SecureRequestDelegatingHandler();
            this.delegatingHandlers.Add(secureRequestDelegatingHandler);
        }

        internal void ThenICanVerifyIHaveAProxy()
        {
            this.proxy.ShouldNotBeNull();
        }

        internal async Task WhenIInvokeAsyncForBasicRequest()
        {
            this.basicRequestProxyResponse = await this.proxy.InvokeAsync(this.basicRequestProxyRequest);
        }

        internal async Task WhenIInvokeAsyncForMessageDtoRequest()
        {
            this.messageDtoRequestProxyResponse = await this.proxy.InvokeAsync(this.messageDtoRequestProxyRequest);
        }

        internal async Task WhenIInvokeAsyncForMessageDtoResponseRequest()
        {
            this.messageDtoResponseRequestProxyResponse = await this.proxy.InvokeAsync(this.messageDtoResponseRequestProxyRequest);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.testEnvironment?.Dispose();

                    if (this.delegatingHandlers != null)
                    {
                        foreach (var handler in this.delegatingHandlers)
                        {
                            handler?.Dispose();
                        }
                    }
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

            proxyResponse.IsSuccessfulStatusCode.ShouldBe(isSuccessfulStatusCode);

            proxyResponse.RequestHeaders.ShouldNotBeNull();
            proxyResponse.RequestHeaders.ShouldNotBeEmpty();

            proxyResponse.RequestUri.ToString().ShouldBe(proxyRequest.RequestUri.ToString());

            proxyResponse.ResponseHeaders.ShouldNotBeNull();
            proxyResponse.ResponseHeaders.ShouldNotBeEmpty();

            proxyResponse.StatusCode.ShouldBe(statusCode);
        }

        #endregion Private Methods
    }
}
