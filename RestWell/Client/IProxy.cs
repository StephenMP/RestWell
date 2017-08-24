using RestWell.Client.Request;
using RestWell.Client.Response;
using System;
using System.Threading.Tasks;

namespace RestWell.Client
{
    public interface IProxy : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Invokes the specified request.
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>A ProxyResonse containing request response information</returns>
        IProxyResponse<TResponseDto> Invoke<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class;

        /// <summary>
        /// Invokes the specified request using the asynchronous framework.
        /// </summary>
        /// <typeparam name="TRequestDto">The type of the request dto.</typeparam>
        /// <typeparam name="TResponseDto">The type of the response dto.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>A ProxyResonse containing request response information</returns>
        Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class;

        #endregion Public Methods
    }
}
