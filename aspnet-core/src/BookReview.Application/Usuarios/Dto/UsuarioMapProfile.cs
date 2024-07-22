using AutoMapper;
using BookReview.Authorization.Users;

namespace BookReview.Usuarios.Dto
{
    public class UsuarioMapProfile : Profile
    {
        public UsuarioMapProfile()
        {
            CreateMap<UsuarioDto, User>();
            
            CreateMap<CreateUsuarioDto, User>();
        }
    }
}
