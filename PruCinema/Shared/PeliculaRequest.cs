using System.ComponentModel.DataAnnotations;

namespace PruCinema.Shared
{
    public class PeliculaRequest
    {
        [Key]
        public long IdPelicula { get; set; }
        
        [Required(ErrorMessage = "Código es requerido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripción es requerida")]
        public string Descripcion { get; set; }

        //[Required(ErrorMessage = "Imagen es requerida")]
        public string Imagen { get; set; }

        public string Accion { get; set; }

	}
}
