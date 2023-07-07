using System.ComponentModel.DataAnnotations;
using System;

namespace WebApiAutores.DTOs
{
    public class LibroPatchDTO
    {        
        [StringLength(maximumLength: 250)]
        [Required]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
