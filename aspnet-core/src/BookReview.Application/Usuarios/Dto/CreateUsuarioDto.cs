using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using BookReview.Authorization.Users;

namespace BookReview.Usuarios.Dto
{
    [AutoMapTo(typeof(Usuario))]
    public class CreateUsuarioDto
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

    }
}
