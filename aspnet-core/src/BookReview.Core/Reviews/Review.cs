using Abp.Domain.Entities;
using BookReview.Enums;
using BookReview.Libros;
using BookReview.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Reviews
{
    public class Review : Entity<int>
    {
        [Required]
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime FechaCalificacion { get; set; }

        public string Opinion { get; set; }

        [Required]
        public LibroClasificacion Calificacion { get; set; }

        [Required]
        public Libro Libro { get; set; }

    }
}
