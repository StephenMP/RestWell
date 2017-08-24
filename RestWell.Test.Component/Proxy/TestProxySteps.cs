using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Testing;
using Shouldly;
using System.Net;
using System.Threading.Tasks;

namespace RestWell.Test.Component.Proxy
{
    internal class TestProxySteps
    {
        private string baseUri;
        private HttpRequestMethod httpRequestMethod;
        private object proxyRequest;
        private object proxyResponse;
        private TestProxy testProxy;
        private object proxyReturnedResponse;

        internal void GivenIHaveABaseUri(string baseUri)
        {
            this.baseUri = baseUri;
        }

        internal void GivenIHaveARequestTypeOf(HttpRequestMethod httpRequestMethod)
        {
            this.httpRequestMethod = httpRequestMethod;
        }

        internal void GivenIHaveAProxyRequest<TRequestDto, TResponseDto>() where TRequestDto : class where TResponseDto : class
        {
            this.proxyRequest = ProxyRequestBuilder<TRequestDto, TResponseDto>
                                    .CreateBuilder(this.baseUri, this.httpRequestMethod)
                                    .Build();
        }

        internal void ThenICanVerifyICanMockRequestWithNoRequestDtoAndNoResponseDto<TResponseDto>() where TResponseDto : class
        {
            var response = this.proxyResponse as IProxyResponse<TResponseDto>;
            var returnedResponse = this.proxyReturnedResponse as IProxyResponse<TResponseDto>;

            returnedResponse.StatusCode.ShouldBe(response.StatusCode);
            returnedResponse.IsSuccessfulStatusCode.ShouldBe(response.IsSuccessfulStatusCode);
            returnedResponse.ResponseMessage.ShouldBe(response.ResponseMessage);
        }

        internal void ThenICanVerifyIGotAResponse()
        {
            this.proxyReturnedResponse.ShouldNotBeNull();
        }

        internal async Task WhenIInvokeTheMockedRequest<TRequestDto, TResponseDto>(bool runAsync) where TRequestDto : class where TResponseDto : class
        {
            if (runAsync)
            {
                this.proxyReturnedResponse = await this.testProxy.InvokeAsync((IProxyRequest<TRequestDto, TResponseDto>)this.proxyRequest);
            }

            else
            {

            }
        }

        internal void WhenISetupMockedRequest<TRequestDto, TResponseDto>(bool returnOnAnyRequest) where TRequestDto : class where TResponseDto : class
        {
            if (returnOnAnyRequest)
            {
                this.testProxy.WhenIReceiveAnyRequest<TRequestDto, TResponseDto>().ThenIShouldReturnThisResponse((IProxyResponse<TResponseDto>)this.proxyResponse);
            }

            else
            {
                this.testProxy.WhenIReceiveThisRequest((IProxyRequest<TRequestDto, TResponseDto>)this.proxyRequest).ThenIShouldReturnThisResponse((IProxyResponse<TResponseDto>)this.proxyResponse);
            }
        }

        internal void GivenIHaveATestProxy()
        {
            this.testProxy = new TestProxy();
        }

        internal void GivenIHaveAProxyResponse<TResponseDto>(HttpStatusCode httpStatusCode, bool v1, string v2) where TResponseDto : class
        {
            this.proxyResponse = new ProxyResponse<TResponseDto>
            {
                StatusCode = httpStatusCode,
                IsSuccessfulStatusCode = v1,
                ResponseMessage = v2
            };
        }
    }
}