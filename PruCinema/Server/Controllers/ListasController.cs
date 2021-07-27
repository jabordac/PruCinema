using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruCinema.Server.Repositories;
using PruSalud.Shared;

namespace PruCinema.Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ListasController : ControllerBase
    {
        private readonly IListasRepository listasRepository;

        public ListasController(IListasRepository listasRepository)
        {
            this.listasRepository = listasRepository;
        }

        [HttpGet("{Codigo}/{Id}/{Estado}")]
        public async Task<ActionResult<IEnumerable<ListaResponse>>> Get(ListaRequest data)
        {
            try
            {
                var result = await listasRepository.GetLista(data);

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

    }
}