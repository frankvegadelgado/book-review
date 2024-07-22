using Abp.Domain.Entities;
using BookReview.Suscripciones;
using BookReview.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Autores
{
    public class Autor : Entity<int>
    {
        [Required]
        [MaxLength(BookReviewConsts.MaxNameLength)]
        public string Nombre { get; set; }

        [Required]
        public string Nacionalidad { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

    }
}
