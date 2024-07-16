using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BookReview.EntityFrameworkCore
{
    public static class BookReviewDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BookReviewDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BookReviewDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
