using Newtonsoft.Json;
using RestWell.Domain.Enums;
using RestWell.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RestWell.Client.Response
{
    internal static class ProxyResponseFactory
    {
        #region Internal Methods

        internal static async Task<IProxyResponse<TResponseDto>> CreateAsync<TResponseDto>(HttpResponseMessage httpResponseMessage) where TResponseDto : class
        {
            var restfulProxyResponse = new ProxyResponse<TResponseDto>();

            using (httpResponseMessage)
            {
                restfulProxyResponse.IsSuccessfulStatusCode = httpResponseMessage.IsSuccessStatusCode;
                restfulProxyResponse.HttpRequestMethod = httpResponseMessage.RequestMessage.Method.Method.ToEnum<HttpRequestMethod>(true);
                restfulProxyResponse.RequestHeaders = httpResponseMessage.RequestMessage.Headers;
                restfulProxyResponse.RequestUri = httpResponseMessage.RequestMessage.RequestUri;
                restfulProxyResponse.ResponseHeaders = httpResponseMessage.Headers;
                restfulProxyResponse.StatusCode = httpResponseMessage.StatusCode;

                var responseContentString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (!responseContentString.IsNullOrEmptyOrWhitespace())
                {
                    if (typeof(TResponseDto) == typeof(string))
                    {
                        restfulProxyResponse.ResponseDto = responseContentString.TrimStart('\\').TrimStart('\"').TrimEnd('\"').TrimEnd('\\') as TResponseDto;
                    }
                    else if (httpResponseMessage.RequestMessage.Headers.Accept.Any(h => h.MediaType == "application/xml") && responseContentString.StartsWith('<') && responseContentString.EndsWith('>'))
                    {
                        var document = XDocument.Parse(responseContentString);
                        using (var reader = document.CreateReader())
                        {
                            var serializer = new XmlSerializer(typeof(TResponseDto));
                            if (serializer.CanDeserialize(reader))
                            {
                                restfulProxyResponse.ResponseDto = serializer.Deserialize(reader) as TResponseDto;
                            }
                        }
                    }
                    else
                    {
                        restfulProxyResponse.ResponseDto = JsonConvert.DeserializeObject<TResponseDto>(responseContentString);
                    }
                }
            }

            return restfulProxyResponse;
        }

        internal static async Task<IProxyResponse<TResponseDto>> CreateAsync<TResponseDto>(Exception e)
        {
            var restfulProxyResponse = new ProxyResponse<TResponseDto>
            {
                IsSuccessfulStatusCode = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ResponseMessage = e.Message
            };

            return await Task.FromResult(restfulProxyResponse);
        }

        #endregion Internal Methods
    }
}
