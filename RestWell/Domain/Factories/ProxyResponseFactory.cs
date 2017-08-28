using Newtonsoft.Json;
using RestWell.Client.Enums;
using RestWell.Client.Response;
using RestWell.Domain.Extensions;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace RestWell.Domain.Factories
{
    internal static class ProxyResponseFactory
    {
        #region Public Methods

        public static async Task<IProxyResponse<TResponseDto>> CreateAsync<TResponseDto>(HttpResponseMessage httpResponseMessage) where TResponseDto : class
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

                if (typeof(TResponseDto) != typeof(Missing))
                {
                    var responseContentString = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (typeof(TResponseDto) == typeof(string))
                    {
                        restfulProxyResponse.ResponseDto = responseContentString.TrimStart('\\').TrimStart('\"').TrimEnd('\"').TrimEnd('\\') as TResponseDto;
                    }

                    else if (!responseContentString.IsNullOrEmptyOrWhitespace())
                    {
                        restfulProxyResponse.ResponseDto = JsonConvert.DeserializeObject<TResponseDto>(responseContentString);
                    }
                }
            }

            return restfulProxyResponse;
        }

        public static IProxyResponse<TResponseDto> Create<TResponseDto>(Exception e)
        {
            var restfulProxyResponse = new ProxyResponse<TResponseDto>
            {
                IsSuccessfulStatusCode = false,
                StatusCode = HttpStatusCode.InternalServerError,
                ResponseMessage = e.Message
            };

            return restfulProxyResponse;
        }

        #endregion Public Methods
    }
}
