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
    public class SalasController : ControllerBase
    {
        private readonly ISalasRepository salasRepository;

        public SalasController(ISalasRepository salasRepository)
        {
            this.salasRepository = salasRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaResponse>>> Get()
        {
            try
            {
                var result = await salasRepository.GetSalas();

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