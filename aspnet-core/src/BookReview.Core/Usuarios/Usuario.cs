using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using BookReview.Autores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Usuarios
{
    public class Usuario : Entity<Guid>
    {
        [Required]
        public string Nombre { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        [Url]
        public string ImagenEnlace { get; set; }

        public List<Autor> Autores { get; }
    }

}