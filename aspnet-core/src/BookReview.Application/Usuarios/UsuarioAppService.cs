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
using BookReview.Exceptions;
using BookReview.Exceptions.Filter;
using BookReview.Suscripciones;
using BookReview.Users.Dto;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Usuarios
{
    [AbpAllowAnonymous]
    [TypeFilter(typeof(AppExceptionFilter))]
    public class UsuarioAppService : AsyncCrudAppService<Usuario, UsuarioDto, Guid, PagedUsuarioResultRequestDto, CreateUsuarioDto, UsuarioDto>, IUsuarioAppService
    {
        private readonly IRepository<Autor> _autorRepository;
        private readonly IRepository<Suscripcion> _suscripcionRepository;
        private readonly IAbpSession _abpSession;
        
        public UsuarioAppService(
            IRepository<Usuario, Guid> repository,
            IRepository<Autor> autorRepository,
            IRepository<Suscripcion> suscripcionRepository,
            IAbpSession abpSession)
            : base(repository)
        {
            _autorRepository = autorRepository;
            _abpSession = abpSession;
            _suscripcionRepository = suscripcionRepository;
        }
        
        public override async Task<UsuarioDto> CreateAsync(CreateUsuarioDto input)
        {
            var user = ObjectMapper.Map<Usuario>(input);

            user.Id = Guid.NewGuid();

            user.CreationTime = DateTime.Now;

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public async Task ChangeImageUrl(Guid userId, [FromBody] ChangeUsuarioImagenDto input)
        {
            var user = await Repository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }

            user.ImagenEnlace = input.ImagenEnlace; 

            await Repository.UpdateAsync(user);

        }

        public async Task DeleteById(Guid userId)
        {
            var user = await Repository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }

            await Repository.DeleteAsync(user);
        }

        public async Task Subscribe(Guid userId, int authorId)
        {
            var user = await Repository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }

            var author = await _autorRepository.GetAsync(authorId);

            if (author == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("AuthorError"), L("AuthorNotFound"));
            }

            var supscription = await _suscripcionRepository.FirstOrDefaultAsync(s => s.Autor.Id == author.Id && s.Usuario.Id == user.Id);

            if (supscription != null)
            {
                throw new ErrorResponseException(HttpStatusCode.Found, L("SupscriptionError"), L("SupscriptionFound"));
            }
            else 
            {
                supscription = new Suscripcion
                {
                    Autor = author,
                    Usuario = user,
                };
            }


            await _suscripcionRepository.InsertAsync(supscription);
        }

        public async Task UnSubscribe(Guid userId, int authorId)
        {
            var user = await Repository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }

            var author = await _autorRepository.GetAsync(authorId);

            if (author == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("AuthorError"), L("AuthorNotFound"));
            }

            var supscription = await _suscripcionRepository.FirstOrDefaultAsync(s => s.Autor.Id == author.Id && s.Usuario.Id == user.Id);

            if (supscription == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("SupscriptionError"), L("SupscriptionNotFound"));
            }

            await _suscripcionRepository.DeleteAsync(supscription);
        }

        public PagedResultDto<UsuarioQueryDto> GetAllUsers(PagedUsuarioResultRequestDto input)
        {
            var usersQuery = Repository.GetAllIncluding(x => x.Autores).Skip(input.SkipCount).Take(input.MaxResultCount);
            var users = (usersQuery.Any()) ? ObjectMapper.ProjectTo<UsuarioQueryDto>(usersQuery).ToList(): new List<UsuarioQueryDto>();
            return new PagedResultDto<UsuarioQueryDto>(users.Count, users);

        }

    }
}

