using RestWell.Client.Request;
using RestWell.Client.Response;

namespace RestWell.Testing
{
    public sealed class TestProxyRequest<TRequestDto, TResponseDto>
    {
        #region Private Fields

        private readonly IProxyRequest<TRequestDto, TResponseDto> requestToMock;

        #endregion Private Fields

        #region Internal Constructors

        internal TestProxyRequest(IProxyRequest<TRequestDto, TResponseDto> request)
        {
            this.requestToMock = request;
        }

        #endregion Internal Constructors

        #region Public Properties

        public IProxyResponse<TResponseDto> ResponseToReturn { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Sets the response to be returned from the request
        /// </summary>
        /// <param name="response">The response.</param>
        public void ThenIShouldReturnThisResponse(IProxyResponse<TResponseDto> response)
        {
            this.ResponseToReturn = response;
        }

        #endregion Public Methods
    }
}
