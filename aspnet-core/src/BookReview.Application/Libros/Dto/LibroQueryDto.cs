using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace BookReview.Libros.Dto
{
    [AutoMapFrom(typeof(Libro))]
    public class LibroQueryDto
    {
        public string Titulo { get; set; }

        public string AutorName { get; set; }

        public string Editorial { get; set; }

        public string ISBN { get; set; }

    }
}
