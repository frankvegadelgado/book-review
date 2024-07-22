using AutoMapper;

namespace BookReview.Usuarios.Dto
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<UsuarioDto, Usuario>()
                .ForMember(x => x.Autores, opt => opt.Ignore())
                .ForMember(x => x.TotalAutores, opt => opt.Ignore());

            CreateMap<CreateUsuarioDto, Usuario>()
                .ForMember(x => x.Autores, opt => opt.Ignore())
                .ForMember(x => x.TotalAutores, opt => opt.Ignore());

            CreateMap<UsuarioQueryDto, Usuario>().ForMember(x => x.Autores, opt => opt.Ignore());
        }
    }
}
