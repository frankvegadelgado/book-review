using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace BookReview.Autores.Dto
{
    [AutoMapFrom(typeof(Autor))]
    public class AutorDto : EntityDto<int>
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
