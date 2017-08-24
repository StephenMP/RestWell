﻿using RestWell.Client;
using RestWell.Client.Enums;
using RestWell.Client.Request;
using RestWell.Client.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWell.Testing
{
    public class TestProxy : IProxy
    {
        #region Private Fields

        private readonly IDictionary<object, object> mockedRequests;
        private object genericAnyRequest;

        #endregion Private Fields

        #region Public Constructors

        public TestProxy()
        {
            this.mockedRequests = new Dictionary<object, object>();
            this.genericAnyRequest = null;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
        }

        /// <summary>
        /// Invokes the specified request.
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A ProxyResonse containing request response information
        /// </returns>
        public IProxyResponse<TResponseDto> Invoke<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            var requestKey = this.genericAnyRequest ?? request;

            if (mockedRequests.ContainsKey(requestKey))
            {
                var mockedRequest = this.mockedRequests[requestKey] as TestProxyRequest<TRequestDto, TResponseDto>;
                return mockedRequest?.ResponseToReturn;
            }

            return null;
        }

        /// <summary>
        /// Invokes the specified request using the asynchronous framework.
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A ProxyResonse containing request response information
        /// </returns>
        public async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class
        {
            return await Task.FromResult(this.Invoke(request));
        }

        /// <summary>
        /// Sets up a response for any request of these types
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <returns></returns>
        public TestProxyRequest<TRequestDto, TResponseDto> WhenIReceiveAnyRequest<TRequestDto, TResponseDto>()
        {
            var request = ProxyRequestBuilder<TRequestDto, TResponseDto>.CreateBuilder("https://www.any.test", HttpRequestMethod.None).Build();
            var mockedRequest = new TestProxyRequest<TRequestDto, TResponseDto>(request);

            this.mockedRequests.Add(request, mockedRequest);
            this.genericAnyRequest = request;

            return mockedRequest;
        }

        /// <summary>
        /// Sets up a response for a specific request of these types
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public TestProxyRequest<TRequestDto, TResponseDto> WhenIReceiveThisRequest<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request)
        {
            var mockedRequest = new TestProxyRequest<TRequestDto, TResponseDto>(request);
            this.mockedRequests.Add(request, mockedRequest);
            this.genericAnyRequest = null;

            return mockedRequest;
        }

        #endregion Public Methods
    }
}
