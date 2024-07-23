using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BookReview.Autores.Dto;
using BookReview.Libros.Dto;
using BookReview.Roles.Dto;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Autores
{
    public interface IAutorAppService : IAsyncCrudAppService<AutorDto, int, PagedAutorResultRequestDto, CreateAutorDto, AutorDto>
    {
        Task<LibroDto> CreateBookByAuthorIdAsync([FromRoute] int authorId, [FromBody] CreateLibroDto input);
        Task<AutorQueryDto> GetByIdAsync([FromRoute] int authorId);

        Task<AutorDto> CreateAuthorAsync(CreateAutorDto input);
    }
}
