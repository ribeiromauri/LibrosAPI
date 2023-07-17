using System.Collections.Generic;

namespace WebApiAutores.DTOs
{
    public class ColeccionDeRecursos<T> : Recurso where T : Recurso
    {
        //La clase hereda de Recurso y la colección que tengamos tiene que heredar de Recurso, como autorDTO etc...
        public List<T> Valores { get; set; }
    }
}
