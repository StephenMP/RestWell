using RestWell.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RestWell.Client.Request
{
    public class ProxyRequest<TRequestDto, TResponseDto> : IProxyRequest<TRequestDto, TResponseDto>
    {
        #region Public Constructors

        public ProxyRequest()
        {
            this.Headers = new Dictionary<string, IEnumerable<string>>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IDictionary<string, IEnumerable<string>> Headers { get; set; }

        public HttpRequestMethod HttpRequestMethod { get; set; }

        public TRequestDto RequestDto { get; set; }

        public Uri RequestUri { get; set; }

        #endregion Public Properties
    }
}
