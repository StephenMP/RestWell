using RestWell.Client.Request;
using RestWell.Client.Response;
using System.Threading.Tasks;

namespace RestWell.Client
{
    public interface IProxy
    {
        #region Public Methods

        IProxyResponse<TResponseDto> Invoke<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class;
        Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request) where TRequestDto : class where TResponseDto : class;

        #endregion Public Methods
    }
}
