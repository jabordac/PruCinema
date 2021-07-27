using System.ComponentModel.DataAnnotations;

namespace PruSalud.Shared
{
    public class ListaResponse
    {
        [Key]
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

    }
}
