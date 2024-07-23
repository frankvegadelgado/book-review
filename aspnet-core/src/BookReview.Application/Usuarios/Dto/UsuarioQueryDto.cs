using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace BookReview.Usuarios.Dto
{
    [AutoMapFrom(typeof(Usuario))]
    public class UsuarioQueryDto : EntityDto<Guid>
    {

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string ImagenEnlace { get; set; }

        public DateTime CreationTime { get; set; }

        public int TotalAutores { get; set; }
    }
}
