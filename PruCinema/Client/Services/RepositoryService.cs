using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PruCinema.Client.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly HttpClient httpClient;

        public RepositoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> Get<T>(string apiURL)
        {
            var responseHttp = await httpClient.GetFromJsonAsync<T>(apiURL);

            return responseHttp;
        }

        public async Task<TResponse> Get<TResponse, TRequest>(string apiURL, TRequest data)
        {
            var responseHttp = await httpClient.PostAsJsonAsync(apiURL, data);
            var content = await responseHttp.Content.ReadFromJsonAsync<TResponse>();

            return content;
        }

        public async Task<HttpResponseMessage> Set<T>(string accion, string apiURL, T data)
        {
            HttpResponseMessage responseHttp = new();

            if (accion == "ADD")
            {
                responseHttp = await httpClient.PostAsJsonAsync(apiURL, data);
            }
            else if (accion == "EDIT")
            {
                responseHttp = await httpClient.PutAsJsonAsync(apiURL, data);
            }

            return responseHttp;
        }

        public async Task<HttpResponseMessage> Del(string apiURL)
        {
            var responseHttp = await httpClient.DeleteAsync(apiURL);

            return responseHttp;
        }
    }
}
