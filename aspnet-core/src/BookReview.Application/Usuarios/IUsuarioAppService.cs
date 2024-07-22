using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BookReview.Roles.Dto;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;

namespace BookReview.Usuarios
{
    public interface IUsuarioAppService : IAsyncCrudAppService<UsuarioDto, Guid, PagedUsuarioResultRequestDto, CreateUsuarioDto, UsuarioDto>
    {
        Task ChangeImageUrl(Guid userId, ChangeUsuarioImagenDto input);
        Task DeleteById(Guid userId);
        Task Subscribe(Guid userId, int authorId);
        Task UnSubscribe(Guid userId, int authorId);
        PagedResultDto<UsuarioQueryDto> GetAllUsers(PagedUsuarioResultRequestDto input);
    }
}
