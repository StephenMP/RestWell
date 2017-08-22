using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RestWell.Test.Component.Request
{
    public class ProxyRequestBuilderFeatures
    {
        private ProxyRequestBuilderSteps steps;

        public ProxyRequestBuilderFeatures()
        {
            this.steps = new ProxyRequestBuilderSteps();
        }

        public static IEnumerable<object[]> GetCanProperlyBuildRequestUriTestData()
        {
            var testData = new List<object[]>();

            for (var i = 0; i < 10; i++)
            {
                var baseUri = $"https://www.{Guid.NewGuid().ToString().Substring(0, 5)}.com";

                var appendages = new[] { Guid.NewGuid().ToString().Substring(0, 5), Guid.NewGuid().ToString().Substring(0, 5) };

                var pathArguments = new[] { Guid.NewGuid().ToString().Substring(0, 5), Guid.NewGuid().ToString().Substring(0, 5) };

                var queryParameters = new Dictionary<string, string>
                {
                    { Guid.NewGuid().ToString().Substring(0, 5), Guid.NewGuid().ToString().Substring(0, 5) },
                    { Guid.NewGuid().ToString().Substring(0, 5), Guid.NewGuid().ToString().Substring(0, 5) }
                };

                var completeUri = new Uri($"{baseUri}/{appendages[0]}/{appendages[1]}/{pathArguments[0]}/{pathArguments[1]}?{queryParameters.ElementAt(0).Key}={queryParameters.ElementAt(0).Value}&{queryParameters.ElementAt(1).Key}={queryParameters.ElementAt(1).Value}");

                testData.Add(new object[] { baseUri, completeUri, appendages, pathArguments, queryParameters });
            }

            return testData;
        }

        [Fact]
        public void CanBuildSimpleProxyRequestUsingStringUri()
        {
            this.steps.GivenIHaveABaseUri("http://www.this.is/fake");

            this.steps.WhenIBuildASimpleProxyRequestUsingString();

            this.steps.ThenICanBuildSimpleProxyRequestUsingStringUri();
        }

        [Theory]
        [MemberData(nameof(GetCanProperlyBuildRequestUriTestData))]
        public void CanProperlyBuildRequestUri(string baseUri, Uri completeUri, IEnumerable<string> appendages, IEnumerable<string> pathArguments, IDictionary<string, string> queryParameters)
        {
            this.steps.GivenIHaveABaseUri(baseUri);
            this.steps.GivenIHaveACompleteUri(completeUri);

            this.steps.WhenIBuildAProxyRequest(appendages, pathArguments, queryParameters);

            this.steps.ThenICanVerifyICanProperlyBuildRequestUri();
        }
    }
}
