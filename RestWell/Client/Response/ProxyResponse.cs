using RestWell.Domain.Enums;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace RestWell.Client.Response
{
    public class ProxyResponse<TResponseDto> : IProxyResponse<TResponseDto>
    {
        #region Public Properties

        public HttpRequestMethod HttpRequestMethod { get; set; }
        public bool IsSuccessfulStatusCode { get; set; }
        public HttpRequestHeaders RequestHeaders { get; set; }
        public Uri RequestUri { get; set; }
        public TResponseDto ResponseDto { get; set; }
        public HttpResponseHeaders ResponseHeaders { get; set; }
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        #endregion Public Properties
    }
}
