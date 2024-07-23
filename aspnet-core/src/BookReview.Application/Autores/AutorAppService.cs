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
using BookReview.Suscripciones;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Autores
{
    [AbpAllowAnonymous]
    [TypeFilter(typeof(AppExceptionFilter))]
    public class AutorAppService : AsyncCrudAppService<Autor, AutorDto, int, PagedAutorResultRequestDto, CreateAutorDto, AutorDto>, IAutorAppService
    {
        
        public AutorAppService(
            IRepository<Autor, int> repository)
            : base(repository)
        {
        }
        
        public override async Task<AutorDto> CreateAsync(CreateAutorDto input)
        {
            var author = ObjectMapper.Map<Autor>(input);

            await Repository.InsertAsync(author);

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(author);
        }

        public async Task<AutorQueryDto> GetByIdAsync(int authorId)
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

