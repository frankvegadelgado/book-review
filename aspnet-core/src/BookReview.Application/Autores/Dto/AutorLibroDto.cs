using Abp.AutoMapper;
using BookReview.Libros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Autores.Dto
{
    [AutoMapFrom(typeof(Libro))]
    public class AutorLibroDto
    {
        public string Titulo { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string ISBN { get; set; }
    }
}
