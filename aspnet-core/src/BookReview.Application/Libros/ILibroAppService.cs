using System;
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
        Task AddReviewAsync(int bookId, Guid userId, [FromBody] CreateReviewDto input);

        IQueryable<LibroQueryDto> GetAllBooks(PagedLibroResultRequestDto input);

        IQueryable<ReviewQueryDto> GetAllReviews(int bookId, [FromBody] PagedReviewResultRequestDto input);
    }
}
