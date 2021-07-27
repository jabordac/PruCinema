using PruCinema.Server.Repositories;
using PruCinema.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruCinema.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculasRepository peliculasRepository;

        public PeliculasController(IPeliculasRepository peliculasRepository)
        {
            this.peliculasRepository = peliculasRepository;
        }

        [HttpGet("{IdPelicula}/{estado}")]
        public async Task<ActionResult<IEnumerable<PeliculaResponse>>> Get(int idPelicula, string estado)
        {
            try
            {
                var result = await peliculasRepository.GetPeliculas(idPelicula, estado);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se ha presentado un error con la obtención de datos.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int?>> Post([FromBody] PeliculaRequest peliculaRequest)
        {
            try
            {
                peliculaRequest.Accion = "AGREGAR";
                return await peliculasRepository.SetPeliculas(peliculaRequest);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error agregando datos");
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> Put([FromBody] PeliculaRequest peliculaRequest)
        {
            try
            {
                peliculaRequest.Accion = "EDITAR";
                return await peliculasRepository.SetPeliculas(peliculaRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error actualizando datos");
            }
        }

        [HttpDelete("{IdPelicula:int}")]
        public async Task<ActionResult<int>> Delete(int idPelicula)
        {
            try
            {
                return await peliculasRepository.DelPeliculas(idPelicula);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error eliminando datos");
            }
        }
    }
}