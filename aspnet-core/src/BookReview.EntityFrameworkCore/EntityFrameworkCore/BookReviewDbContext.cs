using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BookReview.Authorization.Roles;
using BookReview.Authorization.Users;
using BookReview.MultiTenancy;
using BookReview.Autores;
using BookReview.Usuarios;
using BookReview.Suscripciones;
using BookReview.Libros;
using BookReview.Reviews;

namespace BookReview.EntityFrameworkCore
{
    public class BookReviewDbContext : AbpZeroDbContext<Tenant, Role, User, BookReviewDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public BookReviewDbContext(DbContextOptions<BookReviewDbContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Suscripcion> Suscripciones { get; set; }

        public DbSet<Libro> Libros { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
