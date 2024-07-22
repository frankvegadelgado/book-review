using Abp.Domain.Entities;
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
        public Autor Author { get; set; }

        [Required]
        public string Editorial { get; set; }
        
        [Range(1, double.MaxValue)]
        public int PaginasTotal { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [Url]
        public string EnlaceDescarga { get; set; }

        [RegularExpression(("^(?=(?:\\D*\\d){10}(?:(?:\\D*\\d){3})?$)[\\d-]+$"))]
        public string ISBN { get; set; }

        public List<Review> Reviews { get; }

        [Required]
        public LibroClasificacion Calificacion { get; set; }
        /*
        public LibroClasificacion CalculateCalificacion()
        {
            var rankings = Reviews.Select(x => (int)x.Calificacion).ToList();
            var average = rankings.Average();
            var integerValue = Math.Round(average);
            return (LibroClasificacion)integerValue;

        }
        */
        
    }
}
