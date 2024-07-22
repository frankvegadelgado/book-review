using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BookReview.Autores.Dto;
using BookReview.Roles.Dto;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;

namespace BookReview.Autores
{
    public interface IAutorAppService : IAsyncCrudAppService<AutorDto, int, PagedAutorResultRequestDto, CreateAutorDto, AutorDto>
    {
        Task<AutorQueryDto> GetById(int authorId);
    }
}
