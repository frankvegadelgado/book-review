using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using BookReview.Autores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Usuarios
{
    public class Usuario : AuditedEntity<Guid>
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Url]
        public string ImagenEnlace { get; set; }

        public List<Autor> Autores { get; }

        [NotMapped]
        public int TotalAutores
        {
            get
            {
                return Autores.Count;
            }
        }

    }

}