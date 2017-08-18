using RestWell.Client.Request;
using RestWell.Client.Response;
using System.Net;
using System.Threading.Tasks;

namespace RestWell.Domain.Invokers
{
    internal sealed class UnknownInvoker : Invoker
    {
        #region Public Constructors

        public UnknownInvoker() : base(null)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override async Task<IProxyResponse<TResponseDto>> InvokeAsync<TRequestDto, TResponseDto>(IProxyRequest<TRequestDto, TResponseDto> request)
        {
            return await Task.FromResult(BuildUnrecognizedRequestType<TResponseDto>());
        }

        #endregion Public Methods

        #region Private Methods

        private static IProxyResponse<TResponseDto> BuildUnrecognizedRequestType<TResponseDto>()
        {
            return new ProxyResponse<TResponseDto>
            {
                StatusCode = HttpStatusCode.MethodNotAllowed,
                ResponseMessage = $"Unrecognized request type. Valid types are DELETE, GET, HEAD, OPTIONS, PATCH, POST, and PUT."
            };
        }

        #endregion Private Methods
    }
}
