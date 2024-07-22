using Abp.Domain.Entities;
using BookReview.Autores;
using BookReview.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Suscripciones
{
    public class Suscripcion: Entity<int>
    {
        public Autor Autor { get; set; }
        public Usuario Usuario { get; set; }

    }
}
