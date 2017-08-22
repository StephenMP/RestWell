using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RestWell.Domain.Request
{
    public sealed class DefaultProxyRequestHeaders
    {
        internal DefaultProxyRequestHeaders()
        {
            this.Accept = new List<MediaTypeWithQualityHeaderValue>();
            this.AdditionalHeaders = new Dictionary<string, IList<string>>();
        }

        public AuthenticationHeaderValue Authorization { get; set; }

        public IList<MediaTypeWithQualityHeaderValue> Accept { get; }

        public IDictionary<string, IList<string>> AdditionalHeaders { get; }
    }
}