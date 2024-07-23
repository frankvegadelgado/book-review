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
        Task ChangeImageUrlAsync(Guid userId, [FromBody] ChangeUsuarioImagenDto input);
        Task DeleteByIdAsync(Guid userId);
        Task SubscribeAsync(Guid userId, int authorId);
        Task UnSubscribeAsync(Guid userId, int authorId);
        PagedResultDto<UsuarioQueryDto> GetAllUsers(PagedUsuarioResultRequestDto input);
    }
}
