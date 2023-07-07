using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class LibroCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(250)]
        public string Titulo { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
