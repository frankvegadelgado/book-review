using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BookReview.Authorization.Roles;
using BookReview.Authorization.Users;
using BookReview.MultiTenancy;

namespace BookReview.EntityFrameworkCore
{
    public class BookReviewDbContext : AbpZeroDbContext<Tenant, Role, User, BookReviewDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public BookReviewDbContext(DbContextOptions<BookReviewDbContext> options)
            : base(options)
        {
        }
    }
}
