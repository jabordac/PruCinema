using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace PruCinema.Client.Services
{
    public interface IRepositoryService
    {
        Task<T> Get<T>(string apiURL);

        Task<TResponse> Get<TResponse, TRequest>(string apiURL, TRequest data);

        Task<HttpResponseMessage> Set<T>(string accion, string apiURL, T data);

        Task<HttpResponseMessage> Del(string apiURL);
    }
}
