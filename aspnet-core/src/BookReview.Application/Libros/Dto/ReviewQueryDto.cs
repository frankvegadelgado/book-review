using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using BookReview.Enums;
using BookReview.Reviews;
using BookReview.Usuarios;

namespace BookReview.Libros.Dto
{
    [AutoMapFrom(typeof(Review))]
    public class ReviewQueryDto
    {
        public string UsuarioNombre { get; set; }

        public DateTime FechaCalificacion { get; set; }

        public string Opinion { get; set; }

        public int Calificacion { get; set; }

        public string LibroTitulo { get; set; }

    }
}
