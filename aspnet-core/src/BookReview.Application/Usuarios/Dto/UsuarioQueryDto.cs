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

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string Correo { get; set; }

        [Url]
        public string ImagenEnlace { get; set; }

        public DateTime CreationTime { get; set; }

        public int TotalAutores { get; set; }
    }
}
