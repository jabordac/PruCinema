using PruCinema.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruCinema.Server.Repositories
{
    public interface ISalasRepository
    {
        Task<IEnumerable<SalaResponse>> GetSalas();
    }
}
