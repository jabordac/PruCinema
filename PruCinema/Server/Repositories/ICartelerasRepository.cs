using PruCinema.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruCinema.Server.Repositories
{
    public interface ICartelerasRepository
    {
        Task<IEnumerable<CarteleraResponse>> GetCarteleras(CarteleraRequest carteleraRequest);
    }
}
