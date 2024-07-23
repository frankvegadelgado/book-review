using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using BookReview.Authorization.Users;

namespace BookReview.Libros.Dto
{
    [AutoMapTo(typeof(Libro))]
    public class CreateLibroDto
    {
        [Required]
        [MaxLength(AbpUserBase.MaxNameLength)]
        public string Titulo { get; set; }

        [Required]
        public string Editorial { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public int PaginasTotal { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        [Url]
        public string EnlaceDescarga { get; set; }

        [Required]
        [RegularExpression(("^(?=(?:\\D*\\d){10}(?:(?:\\D*\\d){3})?$)[\\d-]+$"))]
        public string ISBN { get; set; }


    }
}
