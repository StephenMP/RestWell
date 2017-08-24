using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Client.Response;
using RestWell.Test.Component.TestingResource;
using RestWell.Testing;
using Shouldly;
using System.Net;
using System.Threading.Tasks;

namespace RestWell.Test.Component.Testing
{
    internal class TestProxySteps
    {
        #region Private Fields

        private string baseUri;
        private HttpRequestMethod httpRequestMethod;
        private object proxyRequest;
        private object proxyResponse;
        private object proxyReturnedResponse;
        private TestProxy testProxy;

        #endregion Private Fields

        #region Internal Methods

        internal void GivenIHaveABaseUri(string baseUri)
        {
            this.baseUri = baseUri;
        }

        internal void GivenIHaveAProxyRequest<TRequestDto, TResponseDto>(string nonce) where TRequestDto : class where TResponseDto : class
        {
            var proxyRequestBuilder = ProxyRequestBuilder<TRequestDto, TResponseDto>
                                    .CreateBuilder(this.baseUri, this.httpRequestMethod);

            if (typeof(TRequestDto) == typeof(TestRequestDto))
            {
                proxyRequestBuilder.SetRequestDto(new TestRequestDto { Message = nonce } as TRequestDto);
            }

            this.proxyRequest = proxyRequestBuilder.Build();
        }

        internal void GivenIHaveAProxyResponse<TResponseDto>(HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce) where TResponseDto : class
        {
            this.proxyResponse = new ProxyResponse<TResponseDto>
            {
                StatusCode = httpStatusCode,
                IsSuccessfulStatusCode = isSuccessfulStatusCode,
                ResponseMessage = nonce
            };

            if (typeof(TResponseDto) == typeof(TestResponseDto))
            {
                ((ProxyResponse<TestResponseDto>)this.proxyResponse).ResponseDto = new TestResponseDto { Message = nonce };
            }
        }

        internal void GivenIHaveARequestTypeOf(HttpRequestMethod httpRequestMethod)
        {
            this.httpRequestMethod = httpRequestMethod;
        }

        internal void GivenIHaveATestProxy()
        {
            this.testProxy = new TestProxy();
        }

        internal void ThenICanVerifyICanMockRequest<TResponseDto>() where TResponseDto : class
        {
            var response = this.proxyResponse as IProxyResponse<TResponseDto>;
            var returnedResponse = this.proxyReturnedResponse as IProxyResponse<TResponseDto>;

            returnedResponse.StatusCode.ShouldBe(response.StatusCode);
            returnedResponse.IsSuccessfulStatusCode.ShouldBe(response.IsSuccessfulStatusCode);
            returnedResponse.ResponseMessage.ShouldBe(response.ResponseMessage);

            if (typeof(TResponseDto) == typeof(TestResponseDto))
            {
                returnedResponse.ResponseDto.ShouldBeOfType(response.ResponseDto.GetType());

                var testResponse = response as IProxyResponse<TestResponseDto>;
                var returnedTestResponse = returnedResponse as IProxyResponse<TestResponseDto>;
                returnedTestResponse.ResponseDto.Message.ShouldBe(testResponse.ResponseDto.Message);
            }
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
                this.proxyReturnedResponse = this.testProxy.Invoke((IProxyRequest<TRequestDto, TResponseDto>)this.proxyRequest);
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

        #endregion Internal Methods
    }
}
