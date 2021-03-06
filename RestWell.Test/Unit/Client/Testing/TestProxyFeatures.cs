﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using RestWell.Client.Enums;
using RestWell.Test.Unit.TestingResource;
using Xunit;

namespace RestWell.Test.Unit.Client.Testing
{
    public class TestProxyFeatures
    {
        #region Private Fields

        private TestProxySteps steps;

        #endregion Private Fields

        #region Public Constructors

        public TestProxyFeatures()
        {
            this.steps = new TestProxySteps();
        }

        #endregion Public Constructors

        #region Public Methods

        public static IEnumerable<object[]> GetTestData()
        {
            var testData = new List<object[]>();
            var requestMethods = new[] { HttpRequestMethod.Delete, HttpRequestMethod.Get, HttpRequestMethod.Head, HttpRequestMethod.Options, HttpRequestMethod.Patch, HttpRequestMethod.Post, HttpRequestMethod.Put };
            var statusCodes = new[] { HttpStatusCode.Accepted, HttpStatusCode.OK, HttpStatusCode.Unauthorized, HttpStatusCode.InternalServerError };
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var baseUri = $"https://www.{Guid.NewGuid().ToString().Substring(0, 4)}.com";
                var requestMethod = requestMethods[random.Next(0, requestMethods.Length)];
                var statusCode = statusCodes[random.Next(0, statusCodes.Length)];
                var isSuccessfulStatusCode = (int)statusCode >= 200 && (int)statusCode < 300;
                var nonce = Guid.NewGuid().ToString();

                testData.Add(new object[] { baseUri, requestMethod, statusCode, isSuccessfulStatusCode, nonce });
            }

            return testData;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task CanMockRequestWithAnyRequestAndAResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveARequestTypeOf(httpRequestMethod);
            this.steps.GivenIHaveAProxyRequest<Missing, TestResponseDto>(nonce);
            this.steps.GivenIHaveAProxyResponse<TestResponseDto>(httpStatusCode, isSuccessfulStatusCode, nonce);
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<Missing, TestResponseDto>(true);
            await this.steps.WhenIInvokeTheMockedRequest<Missing, TestResponseDto>();

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequest<TestResponseDto>();
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task CanMockRequestWithAnyRequestAndNoResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveARequestTypeOf(httpRequestMethod);
            this.steps.GivenIHaveAProxyRequest<Missing, Missing>(nonce);
            this.steps.GivenIHaveAProxyResponse<Missing>(httpStatusCode, isSuccessfulStatusCode, nonce);
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<Missing, Missing>(true);
            await this.steps.WhenIInvokeTheMockedRequest<Missing, Missing>();

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequest<Missing>();
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task CanMockRequestWithNoRequestDtoAndAResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveARequestTypeOf(httpRequestMethod);
            this.steps.GivenIHaveAProxyRequest<Missing, TestResponseDto>(nonce);
            this.steps.GivenIHaveAProxyResponse<TestResponseDto>(httpStatusCode, isSuccessfulStatusCode, nonce);
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<Missing, TestResponseDto>(false);
            await this.steps.WhenIInvokeTheMockedRequest<Missing, TestResponseDto>();

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequest<TestResponseDto>();
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task CanMockRequestWithNoRequestDtoAndNoResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveARequestTypeOf(httpRequestMethod);
            this.steps.GivenIHaveAProxyRequest<Missing, Missing>(nonce);
            this.steps.GivenIHaveAProxyResponse<Missing>(httpStatusCode, isSuccessfulStatusCode, nonce);
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<Missing, Missing>(false);
            await this.steps.WhenIInvokeTheMockedRequest<Missing, Missing>();

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequest<Missing>();
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task CanMockRequestWithRequestDtoAndAResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveARequestTypeOf(httpRequestMethod);
            this.steps.GivenIHaveAProxyRequest<TestRequestDto, TestResponseDto>(nonce);
            this.steps.GivenIHaveAProxyResponse<TestResponseDto>(httpStatusCode, isSuccessfulStatusCode, nonce);
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<TestRequestDto, TestResponseDto>(false);
            await this.steps.WhenIInvokeTheMockedRequest<TestRequestDto, TestResponseDto>();

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequest<TestResponseDto>();
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public async Task CanMockRequestWithRequestDtoAndNoResponseDto(string baseUri, HttpRequestMethod httpRequestMethod, HttpStatusCode httpStatusCode, bool isSuccessfulStatusCode, string nonce)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveARequestTypeOf(httpRequestMethod);
            this.steps.GivenIHaveAProxyRequest<TestRequestDto, Missing>(nonce);
            this.steps.GivenIHaveAProxyResponse<Missing>(httpStatusCode, isSuccessfulStatusCode, nonce);
            this.steps.GivenIHaveATestProxy();

            this.steps.WhenISetupMockedRequest<TestRequestDto, Missing>(false);
            await this.steps.WhenIInvokeTheMockedRequest<TestRequestDto, Missing>();

            this.steps.ThenICanVerifyIGotAResponse();
            this.steps.ThenICanVerifyICanMockRequest<Missing>();
        }

        #endregion Public Methods
    }
}
