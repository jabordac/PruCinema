using PruCinema.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruCinema.Shared;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace PruCinema.Server.Repositories
{
    public class SalasRepository : ISalasRepository
    {
        private readonly SqlConfiguration configuration;

        public SalasRepository(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<SalaResponse>> GetSalas()
        {
            using var connection = dbConnection();
            string sqlProcedure = "dbo.sp_Listas_GET";

            var parametros = new
            {
            };

            return await connection.QueryAsync<SalaResponse>(sqlProcedure, parametros, null, null, CommandType.StoredProcedure);
        }

    }
}
