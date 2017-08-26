using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RestWell.Domain.Request
{
    public sealed class DefaultProxyRequestHeaders
    {
        #region Public Constructors

        public DefaultProxyRequestHeaders()
        {
            this.Accept = new List<MediaTypeWithQualityHeaderValue>();
            this.AdditionalHeaders = new Dictionary<string, IList<string>>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<MediaTypeWithQualityHeaderValue> Accept { get; }
        public IDictionary<string, IList<string>> AdditionalHeaders { get; }
        public AuthenticationHeaderValue Authorization { get; set; }

        #endregion Public Properties
    }
}
