using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BookReview.Roles.Dto;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Usuarios
{
    public interface IUsuarioAppService : IAsyncCrudAppService<UsuarioDto, Guid, PagedUsuarioResultRequestDto, CreateUsuarioDto, UsuarioDto>
    {
        Task ChangeImageUrlAsync([FromRoute] Guid userId, [FromBody] ChangeUsuarioImagenDto input);
        Task DeleteByIdAsync([FromRoute] Guid userId);
        Task SubscribeAsync([FromRoute] Guid userId, [FromRoute] int authorId);
        Task UnSubscribeAsync([FromRoute] Guid userId, [FromRoute] int authorId);
        PagedResultDto<UsuarioQueryDto> GetAllUsers([FromQuery] int offset, [FromQuery] int limit);

        Task<UsuarioDto> CreateUserAsync(CreateUsuarioDto input);
    }
}
