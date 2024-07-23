using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BookReview.Libros.Dto;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Libros
{
    public interface ILibroAppService : IAsyncCrudAppService<LibroDto, int, PagedLibroResultRequestDto, CreateLibroDto, LibroDto>
    {
        Task AddReviewAsync([FromRoute] int bookId, [FromRoute] Guid userId, [FromBody] CreateReviewDto input);

        ListResultDto<LibroQueryDto> GetAllBooks([FromQuery] int? authorId,
            [FromQuery] string editorialName,
            [FromQuery] DateTime? before,
            [FromQuery] DateTime? after,
            [FromQuery] bool? sort,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 50);

        ListResultDto<ReviewQueryDto> GetAllReviews([FromRoute] int bookId,
            [FromQuery, Range(1, 5)] int? reviewType,
            [FromQuery] bool? sort,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 50);
    }
}
