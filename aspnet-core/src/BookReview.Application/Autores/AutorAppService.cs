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
using BookReview.Autores.Dto;
using BookReview.Exceptions;
using BookReview.Exceptions.Filter;
using BookReview.Libros.Dto;
using BookReview.Libros;
using BookReview.Suscripciones;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookReview.Usuarios.Correo;
using System.Linq.Dynamic.Core.Tokenizer;
using BookReview.Usuarios;

namespace BookReview.Autores
{
    [ApiExplorerSettings(GroupName = "v1")]
    [AllowAnonymous]
    [Route("api/v1.0/library")]
    [AbpAllowAnonymous]
    [TypeFilter(typeof(AppExceptionFilter))]
    public class AutorAppService : AsyncCrudAppServiceBase<Autor, AutorDto, int, PagedAutorResultRequestDto, CreateAutorDto, AutorDto>, IAutorAppService
    {
        private readonly ICorreoSender _correoSender;
        private readonly IRepository<Suscripcion> _suscripcionRepository;
        private readonly IRepository<Libro> _libroRepository;

        public AutorAppService(
            IRepository<Autor, int> repository,
            IRepository<Libro> libroRepository,
            IRepository<Suscripcion> suscripcionRepository,
            ICorreoSender correoSender)
            : base(repository)
        {
            _libroRepository = libroRepository;
            _suscripcionRepository = suscripcionRepository;
            _correoSender = correoSender;
        }

        [HttpPost]
        [Route("authors/{authorId}/books")]
        public async Task<LibroDto> CreateBookByAuthorIdAsync([FromRoute] int authorId, [FromBody] CreateLibroDto input)
        {

            var author = await Repository.GetAsync(authorId);

            if (author == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("AuthorError"), L("AuthorNotFound"));
            }

            var book = ObjectMapper.Map<Libro>(input);

            book.Autor = author;

            book.Calificacion = Enums.LibroClasificacion.None;

            await _libroRepository.InsertAsync(book);

            CurrentUnitOfWork.SaveChanges();

            var users = _suscripcionRepository.GetAllIncluding(x => x.Autor, x => x.Usuario)
                .Where(x => x.Autor.Id == author.Id)
                .Select(x => x.Usuario)
                .ToList();

            await NotifyByEmail(users);

            var bookDto = ObjectMapper.Map<LibroDto>(book);

            return bookDto;
        }
        
        protected Task NotifyByEmail(List<Usuario> users)
        {

            return Task.Run(Body);

            async Task Body()
            {
                // This will await one task after another and so they're running sequential
                foreach (var user in users)
                    await _correoSender.SendEmailNotificationAsync(user);
            }

        }


        [HttpPost]
        [Route("authors")]
        public async Task<AutorDto> CreateAuthorAsync(CreateAutorDto input)
        {
            var author = ObjectMapper.Map<Autor>(input);

            await Repository.InsertAsync(author);

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(author);
        }

        [HttpGet]
        [Route("authors/{authorId}")]
        public async Task<AutorQueryDto> GetByIdAsync([FromRoute] int authorId)
        {
            var author = await Repository.GetAsync(authorId);

            if (author == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("AuthorError"), L("AuthorNotFound"));
            }

            var authorQuery = ObjectMapper.Map<AutorQueryDto>(author);

            authorQuery.Libros = ObjectMapper.ProjectTo<AutorLibroDto>(author.Libros.AsQueryable()).ToList();

            return authorQuery;
        }

    }
}

