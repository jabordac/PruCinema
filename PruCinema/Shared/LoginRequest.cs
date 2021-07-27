using System.ComponentModel.DataAnnotations;

namespace PruCinema.Shared
{
    public class LoginRequest
    {
        [Key]
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Password { get; set; }
    }

}
