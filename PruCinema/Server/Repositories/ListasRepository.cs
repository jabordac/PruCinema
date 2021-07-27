using Dapper;
using PruCinema.Data;
using PruSalud.Shared;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PruCinema.Server.Repositories
{
    public class ListasRepository : IListasRepository
    {
        private readonly SqlConfiguration configuration;

        public ListasRepository(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<ListaResponse>> GetLista(ListaRequest listaRequest)
        {
            using var connection = dbConnection();
            string sqlProcedure = "dbo.sp_Listas_GET";

            var parametros = new
            {
                CodTabla = listaRequest.CodTabla,
                Estado = listaRequest.Estado
            };

            return await connection.QueryAsync<ListaResponse>(sqlProcedure, parametros, null, null, CommandType.StoredProcedure);
        }

    }
}
