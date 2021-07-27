using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruCinema.Shared
{
    public class CarteleraRequest
    {
        public long IdCartelera { get; set; }
        public int IdSala { get; set; }
        public string Accion { get; set; }
    }
}
