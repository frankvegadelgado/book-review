using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BookReview.Roles.Dto;
using BookReview.Usuarios.Dto;

namespace BookReview.Usuarios
{
    public interface IUsuarioAppService : IAsyncCrudAppService<UsuarioDto, Guid, PagedUsuarioResultRequestDto, CreateUsuarioDto, UsuarioDto>
    {
        /*
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
        */
    }
}
