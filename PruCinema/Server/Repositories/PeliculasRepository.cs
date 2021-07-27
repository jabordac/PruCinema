using System;
using Dapper;
using PruCinema.Data;
using PruCinema.Shared;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruCinema.Server.Repositories
{
    public class PeliculasRepository : IPeliculasRepository
    {
        private readonly SqlConfiguration configuration;

        public PeliculasRepository(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<IEnumerable<PeliculaResponse>> GetPeliculas(long idPelicula, string estado)
        {
            using var connection = dbConnection();
            string sqlProcedure = "dbo.sp_Peliculas_GET";

            var parametros = new
            {
                IdPelicula = idPelicula,
                Estado = estado
            };

            try
            {
                var result = await connection.QueryAsync<PeliculaResponse>(sqlProcedure, parametros, null, null,
                    CommandType.StoredProcedure);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int?> SetPeliculas(PeliculaRequest peliculaRequest)
        {
            using var connection = dbConnection();
            string sqlProcedure = "dbo.sp_Peliculas_SET";

            var parametros = new
            {
                IdPelicula = peliculaRequest.IdPelicula,
                Codigo = peliculaRequest.Codigo,
                Nombre = peliculaRequest.Nombre,
                Descripcion = peliculaRequest.Descripcion,
                Imagen = peliculaRequest.Imagen,
                Accion = peliculaRequest.Accion
            };

            try
            {
                return await connection.ExecuteAsync(sqlProcedure, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int?> DelPeliculas(long idPelicula)
        {
            using var connection = dbConnection();
            string sqlProcedure = "dbo.sp_Peliculas_SET";

            var parametros = new
            {
                IdPelicula = idPelicula,
                Accion = "ELIMINAR"
            };

            try
            {
                return await connection.ExecuteAsync(sqlProcedure, parametros, null, null, CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
