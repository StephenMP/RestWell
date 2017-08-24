using RestWell.Client.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace RestWell.Test.Component.Proxy
{
    public class TestProxyFeatures
    {
        private TestProxySteps steps;

        public TestProxyFeatures()
        {
            this.steps = new TestProxySteps();
        }

        public static IEnumerable<object[]> GetTestDataForMockRequestWithNoRequestDtoAndNoResponseDto()
        {
            var testData = new List<object[]>();
            var requestMethods = new[] { HttpRequestMethod.Delete, HttpRequestMethod.Get, HttpRequestMethod.Head, HttpRequestMethod.Options, HttpRequestMethod.Patch, HttpRequestMethod.Post, HttpRequestMethod.Put };
            var statusCodes = new[] { HttpStatusCode.Accepted, HttpStatusCode.OK, HttpStatusCode.Unauthorized, HttpStatusCode.InternalServerError };
            var runAsyncBooleans = new[] { true, false };
            var random = new Random();

            foreach (var runAsync in runAsyncBooleans)
            {
                for (int i = 0; i < 10; i++)
                {
                    var baseUri = $"https://www.{Guid.NewGuid().ToString().Substring(0, 4)}.com";
                    var requestMethod = requestMethods[random.Next(0, requestMethods.Length)];
                    var statusCode = statusCodes[random.Next(0, statusCodes.Length)];
                    var isSuccessfulStatusCode = (int)statusCode >= 200 && (int)statusCode < 300;
                    var responseMessage = Guid.NewGuid().ToString();

                    testData.Add(new object[] { baseUri, requestMethod, statusCode, isSuccessfulStatusCode, responseMessage, runAsync });
                }
            }

            return testData;
        }

        [Theory]
        [MemberData(nameof(GetTestDataForMockRequestWithNoRequestDtoAndNoResponseDto))]
        public async Task CanMockRequestWithNoRequestDtoAndNoResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string responseMessage, bool runAsync)
        {
            this.steps.GivenIHaveABaseUri("https://www.this.is/fake");
            this.steps.GivenIHaveARequestTypeOf(HttpRequestMethod.Get);
            this.steps.GivenIHaveAProxyRequest<Missing, Missing>();
            this.steps.GivenIHaveAProxyResponse<Missing>(HttpStatusCode.OK, true, "Test Message");
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<Missing, Missing>(false);
            await this.steps.WhenIInvokeTheMockedRequest<Missing, Missing>(true);

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequestWithNoRequestDtoAndNoResponseDto<Missing>();
        }
    }
}
