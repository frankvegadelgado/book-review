using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using BookReview.Authorization;
using BookReview.Authorization.Users;
using BookReview.Autores;
using BookReview.Enums;
using BookReview.Exceptions;
using BookReview.Exceptions.Filter;
using BookReview.Libros.Dto;
using BookReview.Reviews;
using BookReview.Suscripciones;
using BookReview.Usuarios;
using BookReview.Usuarios.Correo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Libros
{
    [ApiExplorerSettings(GroupName = "v1")]
    [AllowAnonymous]
    [Route("api/v1.0/library")]
    [AbpAllowAnonymous]
    [TypeFilter(typeof(AppExceptionFilter))]
    public class LibroAppService : AsyncCrudAppServiceBase<Libro, LibroDto, int, PagedLibroResultRequestDto, CreateLibroDto, LibroDto>, ILibroAppService
    {
        private readonly IRepository<Usuario, Guid> _userRepository;
        private readonly IRepository<Review> _reviewRepository;
        
        public LibroAppService(
            IRepository<Libro, int> repository,
            IRepository<Usuario, Guid> userRepository,
            IRepository<Review> reviewRepository)
            : base(repository)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [Route("books")]
        public ListResultDto<LibroQueryDto> GetAllBooks([FromQuery] int? authorId,
            [FromQuery] string editorialName,
            [FromQuery] DateTime? before,
            [FromQuery] DateTime? after,
            [FromQuery] int offset,
            [FromQuery] int limit,
            [FromQuery] bool? sort)
        {
            var bookQuery = Repository.GetAllIncluding(x => x.Autor)
                .WhereIf(authorId.HasValue, x => x.Autor.Id == authorId.Value)
                .WhereIf(!string.IsNullOrEmpty(editorialName), x => x.Editorial.Contains(editorialName))
                .WhereIf(after.HasValue, x => x.FechaPublicacion >= after.Value)
                .WhereIf(before.HasValue, x => x.FechaPublicacion <= before.Value)
                .Skip(offset).Take(limit);
            var bookQuerySorted = !sort.HasValue ? bookQuery : ((sort.Value) ? bookQuery.OrderBy(x => (int)x.Calificacion) : bookQuery.OrderByDescending(x => (int)x.Calificacion));
            var books = ObjectMapper.ProjectTo<LibroQueryDto>(bookQuerySorted);
            return new ListResultDto<LibroQueryDto>(books.ToList());

        }
        [HttpGet]
        [Route("books/{bookId}/reviews")]
        public ListResultDto<ReviewQueryDto> GetAllReviews([FromRoute] int bookId,
            [FromQuery, Range(1, 5)] int? reviewType,
            [FromQuery] bool? sort,
            [FromQuery] int offset,
            [FromQuery] int limit)
        {
            var reviewQuery = _reviewRepository.GetAllIncluding(x => x.Libro, x => x.Usuario)
                .Where(x => x.Libro.Id == bookId)
                .WhereIf(reviewType.HasValue, x => x.Calificacion == (LibroClasificacion)reviewType)
                .Skip(offset).Take(limit);
            var reviewQuerySorted = !sort.HasValue ? reviewQuery : ((sort.Value) ? reviewQuery.OrderBy(x => x.FechaCalificacion) : reviewQuery.OrderByDescending(x => x.FechaCalificacion)); 
            var reviews = ObjectMapper.ProjectTo<ReviewQueryDto>(reviewQuerySorted);
            return new ListResultDto<ReviewQueryDto>(reviews.ToList());

        }
        [HttpPost]
        [Route("books/{bookId}/reviews/from/users/{userId}")]
        public async Task AddReviewAsync([FromRoute] int bookId, [FromRoute] Guid userId, [FromBody] CreateReviewDto input)
        {
            var book = await Repository.GetAsync(bookId);

            if (book == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("BookError"), L("BookNotFound"));
            }

            var user = await _userRepository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }
                        
            var review = await _reviewRepository.FirstOrDefaultAsync(s => s.Libro.Id == book.Id && s.Usuario.Id == user.Id);

            if (review != null)
            {
                review.Opinion = (string.IsNullOrEmpty(input.Opinion)) ? review.Opinion: input.Opinion;
                review.Calificacion = (LibroClasificacion)input.Calificacion;
                review.FechaCalificacion = DateTime.Now;
            }
            else
            {
                review = new Review
                {
                    Opinion = input.Opinion,
                    Calificacion = (LibroClasificacion)input.Calificacion,
                    Usuario = user,
                    Libro = book,
                    FechaCalificacion = DateTime.Now,
                };
            }

            var bookReviews = new List<Review>(book.Reviews);

            bookReviews.Add(review);

            book.Calificacion = CalculateCalificacion(bookReviews);

            await _reviewRepository.InsertOrUpdateAsync(review);

            await Repository.UpdateAsync(book);

            CurrentUnitOfWork.SaveChanges();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual LibroClasificacion CalculateCalificacion(List<Review> reviews)
        {
            var rankings = reviews.Select(x => (int)x.Calificacion).ToList();
            var average = rankings.Average();
            var integerValue = Math.Round(average);
            return (LibroClasificacion)integerValue;

        }

    }
}

