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
    public class AutorQueryDto : EntityDto<int>
    {
        public string Nombre { get; set; }

        public string Nacionalidad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public List<AutorLibroDto> Libros { get; set; }

        public int TotalUsuarios { get; set; }


    }
}
