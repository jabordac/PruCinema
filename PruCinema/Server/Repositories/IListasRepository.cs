using PruSalud.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruCinema.Server.Repositories
{
    public interface IListasRepository
    {
        Task<IEnumerable<ListaResponse>> GetLista(ListaRequest listaRequest);
    }
}
