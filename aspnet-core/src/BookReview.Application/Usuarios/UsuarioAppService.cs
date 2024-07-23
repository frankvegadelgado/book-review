﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using BookReview.Autores;
using BookReview.Exceptions;
using BookReview.Exceptions.Filter;
using BookReview.Suscripciones;
using BookReview.Usuarios.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Usuarios
{
    [ApiExplorerSettings(GroupName = "v1")]
    [AllowAnonymous]
    [Route("api/v1.0/library")]
    [AbpAllowAnonymous]
    [TypeFilter(typeof(AppExceptionFilter))]
    public class UsuarioAppService : AsyncCrudAppServiceBase<Usuario, UsuarioDto, Guid, PagedUsuarioResultRequestDto, CreateUsuarioDto, UsuarioDto>, IUsuarioAppService
    {
        private readonly IRepository<Autor> _autorRepository;
        private readonly IRepository<Suscripcion> _suscripcionRepository;
        
        public UsuarioAppService(
            IRepository<Usuario, Guid> repository,
            IRepository<Autor> autorRepository,
            IRepository<Suscripcion> suscripcionRepository)
            : base(repository)
        {
            _autorRepository = autorRepository;
            _suscripcionRepository = suscripcionRepository;
        }
        [HttpPost]
        [Route("users")]
        public async Task<UsuarioDto> CreateUserAsync(CreateUsuarioDto input)
        {
            var user = await Repository.FirstOrDefaultAsync(u => u.Correo == input.Correo);

            if (user != null)
            {
                throw new ErrorResponseException(HttpStatusCode.Found, L("UserError"), L("EmailFound"));
            }

            user = ObjectMapper.Map<Usuario>(input);

            user.Id = Guid.NewGuid();

            user.CreationTime = DateTime.Now;

            await Repository.InsertAsync(user);

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [HttpPut]
        [Route("users/{userId}")]
        public async Task ChangeImageUrlAsync([FromRoute] Guid userId, [FromBody] ChangeUsuarioImagenDto input)
        {
            var user = await Repository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }

            user.ImagenEnlace = input.ImagenEnlace; 

            await Repository.UpdateAsync(user);

            CurrentUnitOfWork.SaveChanges();

        }
        [HttpDelete]
        [Route("users/{userId}")]
        public async Task DeleteByIdAsync([FromRoute] Guid userId)
        {
            var user = await Repository.GetAsync(userId);

            if (user == null)
            {
                throw new ErrorResponseException(HttpStatusCode.NotFound, L("UserError"), L("UserNotFound"));
            }

            await _suscripcionRepository.DeleteAsync(s => s.Usuario.Id == userId);

            await Repository.DeleteAsync(user);

            CurrentUnitOfWork.SaveChanges();
        }
        [HttpPost]
        [Route("users/{userId}/subscribe-to-author/{authorId}")]
        public async Task SubscribeAsync([FromRoute] Guid userId, [FromRoute] int authorId)
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

            CurrentUnitOfWork.SaveChanges();
        }
        [HttpDelete]
        [Route("users/{userId}/subscribe-to-author/{authorId}")]
        public async Task UnSubscribeAsync([FromRoute] Guid userId, [FromRoute] int authorId)
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

            CurrentUnitOfWork.SaveChanges();
        }
        [HttpGet]
        [Route("users")]
        public PagedResultDto<UsuarioQueryDto> GetAllUsers([FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var usersQuery = Repository.GetAllIncluding(x => x.Autores).Skip(offset).Take(limit);
            var users = (usersQuery.Any()) ? ObjectMapper.ProjectTo<UsuarioQueryDto>(usersQuery).ToList(): new List<UsuarioQueryDto>();
            return new PagedResultDto<UsuarioQueryDto>(users.Count, users);

        }

    }
}

