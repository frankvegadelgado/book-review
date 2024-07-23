using AutoMapper;
using BookReview.Reviews;

namespace BookReview.Libros.Dto
{
    public class LibroMapProfile : Profile
    {
        public LibroMapProfile()
        {
            CreateMap<LibroDto, Libro>()
                .ForMember(x => x.Autor, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore());

            CreateMap<Libro, LibroDto> ()
                .ForMember(x => x.AutorId, opt => opt.MapFrom(input => input.Autor.Id))
                .ForMember(x => x.Calificacion, opt => opt.MapFrom(input => (int)input.Calificacion));

            CreateMap<CreateLibroDto, Libro>()
                .ForMember(x => x.Autor, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore())
                .ForMember(x => x.Calificacion, opt => opt.Ignore());

            CreateMap<LibroQueryDto, Libro>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Autor, opt => opt.Ignore())
                .ForMember(x => x.EnlaceDescarga, opt => opt.Ignore())
                .ForMember(x => x.FechaPublicacion, opt => opt.Ignore())
                .ForMember(x => x.Reviews, opt => opt.Ignore())
                .ForMember(x => x.Calificacion, opt => opt.Ignore());

            CreateMap<Libro, LibroQueryDto>()
                .ForMember(x => x.AutorName, opt => opt.MapFrom(input => input.Autor.Nombre));


            CreateMap<ReviewQueryDto, Review>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Libro, opt => opt.Ignore())
                .ForMember(x => x.Usuario, opt => opt.Ignore());

            CreateMap<Review, ReviewQueryDto>()
                .ForMember(x => x.LibroTitulo, opt => opt.MapFrom(input => input.Libro.Titulo))
                .ForMember(x => x.Calificacion, opt => opt.MapFrom(input => (int)input.Calificacion))
                .ForMember(x => x.UsuarioNombre, opt => opt.MapFrom(input => input.Usuario.Nombre));

        }
    }
}
