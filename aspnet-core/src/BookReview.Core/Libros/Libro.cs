﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReview.Autores;
using BookReview.Usuarios;
using BookReview.Reviews;
using BookReview.Enums;

namespace BookReview.Libros
{
    public class Libro : Entity<int>
    {
        [Required]
        [MaxLength(BookReviewConsts.MaxNameLength)]
        public string Titulo { get; set; }

        [Required]
        public Autor Autor { get; set; }

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

        public List<Review> Reviews { get; }

        [Required]
        public LibroClasificacion Calificacion { get; set; }
        
        
    }
}
