using PruCinema.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruCinema.Server.Repositories
{
    public interface IPeliculasRepository
    {
        Task<IEnumerable<PeliculaResponse>> GetPeliculas(long idPelicula, string estado);
        Task<int?> SetPeliculas(PeliculaRequest peliculaRequest);
        Task<int?> DelPeliculas(long idPelicula);
    }
}
