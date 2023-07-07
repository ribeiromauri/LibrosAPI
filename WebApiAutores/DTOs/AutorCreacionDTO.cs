using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
