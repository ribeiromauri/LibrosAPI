﻿using AutoMapper;
using System.Collections.Generic;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();

            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));

            CreateMap<Libro, LibroDTO>()
                .ForMember(libro => libro.Autores, opciones => opciones.MapFrom(MapLibroDTOAutores));
            
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
        }

        private List<AutorDTO> MapLibroDTOAutores(Libro libro, LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();

            if(libro.AutoresLibros == null) { return resultado; }

            foreach (var autorLibro in libro.AutoresLibros)
            {
                resultado.Add(new AutorDTO()
                {
                    Id = autorLibro.AutorId,
                    Nombre = autorLibro.Autor.Nombre
                });
            }
            return resultado;
        }
        private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        {
            var resultado = new List<AutorLibro>();

            if (libroCreacionDTO.AutoresIds == null) { return resultado; }

            foreach (var autorId in libroCreacionDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro() { AutorId = autorId });
            }

            return resultado;
        }
    }
}
