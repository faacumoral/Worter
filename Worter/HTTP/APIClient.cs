using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FMCW.Common.Results;
using Worter.DTO.Login;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using Worter.Models;
using System.Net.Http.Headers;

namespace Worter.HTTP
{
    public class APIClient
    {
        private readonly HttpClient client;

        private readonly ISessionStorageService sessionStorage;

        public APIClient(HttpClient client, ISessionStorageService sessionStorage)
        {
            this.client = client;
            this.sessionStorage = sessionStorage;
        }

        public async Task<Tresult> Post<Tsend, Tresult>(APIRequest<Tsend> request)
            where Tresult : IBaseErrorResult, new()
        {
            var jwtToken = await sessionStorage.GetStringAsync(Constants.JWT_TOKEN);

            if (string.IsNullOrEmpty(jwtToken) && request.CheckAuth)
            {
                return new Tresult()
                {
                    ResultError = ErrorResult.Build("JWT cannot be blank"),
                    Success = false,
                    ResultOperation = ResultOperation.Unauthorized,
                };
            }

            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(client.BaseAddress.OriginalString + request.Url)
            };

            if (request.Parameter != null)
            {
                requestMessage.Content = JsonContent.Create(request.Parameter);
            }

            if (request.CheckAuth)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var response = await client.SendAsync(requestMessage);
            // var responseStatusCode = response.StatusCode; 
            return await response.Content.ReadFromJsonAsync<Tresult>();
        }
    }
}
