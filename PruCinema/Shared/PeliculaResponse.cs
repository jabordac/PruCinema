using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruCinema.Shared
{
    public class PeliculaResponse
    {
        public long IdPelicula { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public long Carteleras { get; set; }
        public string Estado { get; set; }
    }
}
