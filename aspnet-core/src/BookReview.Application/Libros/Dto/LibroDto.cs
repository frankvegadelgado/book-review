using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace BookReview.Libros.Dto
{
    [AutoMapFrom(typeof(Libro))]
    public class LibroDto : EntityDto<int>
    {

        [Required]
        [MaxLength(AbpUserBase.MaxNameLength)]
        public string Titulo { get; set; }

        [Required]
        public int AutorId { get; set; }

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

        [Required]
        public int Calificacion { get; set; }
    }
}
