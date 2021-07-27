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
    public class CartelerasRepository : ICartelerasRepository
    {
        private readonly SqlConfiguration configuration;

        public CartelerasRepository(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<CarteleraResponse>> GetCarteleras(CarteleraRequest carteleraRequest)
        {
            using var connection = dbConnection();
            string sqlProcedure = "dbo.sp_Carteleras_GET";

            var parametros = new
            {
                IdCartelera = carteleraRequest.IdCartelera,
                IdSala = carteleraRequest.IdSala
            };

            return await connection.QueryAsync<CarteleraResponse>(sqlProcedure, parametros, null, null, CommandType.StoredProcedure);
        }

    }
}
