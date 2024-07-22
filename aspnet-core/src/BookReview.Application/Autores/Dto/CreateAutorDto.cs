using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using BookReview.Authorization.Users;

namespace BookReview.Autores.Dto
{
    [AutoMapTo(typeof(Autor))]
    public class CreateAutorDto
    {
        [Required]
        [MaxLength(AbpUserBase.MaxNameLength)]
        public string Nombre { get; set; }

        [Required]
        public string Nacionalidad { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

    }
}
