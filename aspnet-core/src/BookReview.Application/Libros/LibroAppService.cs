using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Libros
{
    [AbpAllowAnonymous]
    [TypeFilter(typeof(AppExceptionFilter))]
    public class LibroAppService : AsyncCrudAppService<Libro, LibroDto, int, PagedLibroResultRequestDto, CreateLibroDto, LibroDto>, ILibroAppService
    {
        private readonly IRepository<Usuario, Guid> _userRepository;
        private readonly IRepository<Suscripcion> _suscripcionRepository;
        private readonly IRepository<Autor> _autorRepository;
        private readonly IRepository<Review> _reviewRepository;
        private readonly ICorreoSender _correoSender;

        public LibroAppService(
            IRepository<Libro, int> repository,
            IRepository<Usuario, Guid> userRepository,
            IRepository<Suscripcion> suscripcionRepository,
            IRepository<Autor> autorRepository,
            IRepository<Review> reviewRepository,
            ICorreoSender correoSender)
            : base(repository)
        {
            _userRepository = userRepository;
            _suscripcionRepository = suscripcionRepository;
            _autorRepository = autorRepository;
            _reviewRepository = reviewRepository;
            _correoSender = correoSender;
        }

        public async Task<LibroDto> CreateByAuthorIdAsync(int authorId, [FromBody] CreateLibroDto input)
        {
            
            var author = await _autorRepository.GetAsync(authorId);

            if (author == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("AuthorError"), L("AuthorNotFound"));
            }

            var book = ObjectMapper.Map<Libro>(input);

            book.Autor = author;

            book.Calificacion = Enums.LibroClasificacion.None;

            await Repository.InsertAsync(book);

            CurrentUnitOfWork.SaveChanges();

            var users = _suscripcionRepository.GetAllIncluding(x => x.Autor, x => x.Usuario)
                .Where(x => x.Autor.Id == author.Id)
                .Select(x => x.Usuario)
                .ToList();

            //await NotifyByEmail(users);

            return MapToEntityDto(book);
        }
        /*
        protected async Task NotifyByEmail(List<Usuario> users)
        {
            Task.Run(users.ForEach(u => _correoSender.SendEmailNotificationAsync(u)))
        }
        */

        public IQueryable<LibroQueryDto> GetAllBooks(PagedLibroResultRequestDto input)
        {
            var bookQuery = Repository.GetAllIncluding(x => x.Autor)
                .WhereIf(input.AutorId.HasValue, x => x.Autor.Id == input.AutorId.Value)
                .WhereIf(!string.IsNullOrEmpty(input.Editorial), x => x.Editorial == input.Editorial)
                .WhereIf(input.AfterDate.HasValue, x => x.FechaPublicacion >= input.AfterDate.Value)
                .WhereIf(input.BeforeDate.HasValue, x => x.FechaPublicacion <= input.BeforeDate.Value)
                .Skip(input.SkipCount).Take(input.MaxResultCount);
            var bookQuerySorted = !input.SortRanking.HasValue ? bookQuery : ((input.SortRanking.Value) ? bookQuery.OrderBy(x => (int)x.Calificacion) : bookQuery.OrderByDescending(x => (int)x.Calificacion));
            var books = ObjectMapper.ProjectTo<LibroQueryDto>(bookQuerySorted);
            return books;

        }

        public IQueryable<ReviewQueryDto> GetAllReviews(int bookId, [FromBody] PagedReviewResultRequestDto input)
        {
            var reviewQuery = _reviewRepository.GetAllIncluding(x => x.Libro, x => x.Usuario)
                .Where(x => x.Libro.Id == bookId)
                .WhereIf(input.ReviewType.HasValue, x => x.Calificacion == (LibroClasificacion)input.ReviewType)
                .Skip(input.SkipCount).Take(input.MaxResultCount);
            var reviewQuerySorted = !input.SortByDateCreation.HasValue ? reviewQuery : ((input.SortByDateCreation.Value) ? reviewQuery.OrderBy(x => x.FechaCalificacion) : reviewQuery.OrderByDescending(x => x.FechaCalificacion)); 
            var reviews = ObjectMapper.ProjectTo<ReviewQueryDto>(reviewQuerySorted);
            return reviews;

        }

        public async Task AddReviewAsync(int bookId, Guid userId, [FromBody] CreateReviewDto input)
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


        protected virtual LibroClasificacion CalculateCalificacion(List<Review> reviews)
        {
            var rankings = reviews.Select(x => (int)x.Calificacion).ToList();
            var average = rankings.Average();
            var integerValue = Math.Round(average);
            return (LibroClasificacion)integerValue;

        }

    }
}

