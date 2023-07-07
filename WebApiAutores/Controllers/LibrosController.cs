using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            var libros = await context.Libros.ToListAsync();
            return mapper.Map<List<LibroDTO>>(libros);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros
                .Include(libroDb => libroDb.AutoresLibros) //sería como un inner join y el thenInclude anidado
                .ThenInclude(autorLibroDb  => autorLibroDb.Autor)
                .FirstOrDefaultAsync(libroDB => libroDB.Id == id);

            libro.AutoresLibros = libro.AutoresLibros.OrderBy(x => x.Orden).ToList(); //ordena la lista por numero de orden del autor 

            return mapper.Map<LibroDTO>(libro);            
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds == null)
            {
                return BadRequest("No se puede crear un libro sin autores");
            }

            var autoresIds = await context.Autores.Where(autorBD => libroCreacionDTO.AutoresIds.Contains(autorBD.Id)).Select(x => x.Id).ToListAsync();

            if (libroCreacionDTO.AutoresIds.Count != autoresIds.Count)
            {
                return BadRequest("No existe uno de los autores enviados");
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
