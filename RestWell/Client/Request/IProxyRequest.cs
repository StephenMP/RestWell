using RestWell.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RestWell.Client.Request
{
    public interface IProxyRequest<TRequestDto, TResponseDto>
    {
        #region Public Properties

        IDictionary<string, IEnumerable<string>> Headers { get; set; }

        HttpRequestMethod HttpRequestMethod { get; set; }

        TRequestDto RequestDto { get; set; }

        Uri RequestUri { get; set; }

        #endregion Public Properties
    }
}
