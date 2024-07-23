using Abp.AutoMapper;
using AutoMapper;
using BookReview.Libros;
using System.Collections.Generic;

namespace BookReview.Autores.Dto
{
    public class AutorMapProfile : Profile
    {
        public AutorMapProfile()
        {
            CreateMap<AutorDto, Autor>()
                .ForMember(x => x.Usuarios, opt => opt.Ignore())
                .ForMember(x => x.Libros, opt => opt.Ignore())
                .ForMember(x => x.TotalUsuarios, opt => opt.Ignore());

            CreateMap<CreateAutorDto, Autor>()
                .ForMember(x => x.Usuarios, opt => opt.Ignore())
                .ForMember(x => x.Libros, opt => opt.Ignore())
                .ForMember(x => x.TotalUsuarios, opt => opt.Ignore());


            CreateMap<AutorLibroDto, Libro>()
                .ForMember(x => x.EnlaceDescarga, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Editorial, opt => opt.Ignore())
                .ForMember(x => x.Calificacion, opt => opt.Ignore())
                .ForMember(x => x.Autor, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore());

            CreateMap<AutorQueryDto, Autor>()
                .ForMember(x => x.Usuarios, opt => opt.Ignore())
                .ForMember(x => x.Libros, opt => opt.Ignore());
        }
    }
}
