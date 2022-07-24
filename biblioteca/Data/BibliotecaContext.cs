using biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext (DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<biblioteca.Models.User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Loan> Loan { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(x =>
            {
                x.HasKey(s => s.Id);
            });

            builder.Entity<Book>(x =>
            {
                x.HasKey(s => s.Id);
            });

            builder.Entity<Loan>(x =>
            {
                x.HasKey(s => s.Id);

                x.HasOne(s => s.User)
                    .WithMany(s => s.Loans)
                    .HasForeignKey(s => s.UserId);

                x.HasOne(s => s.Book)
                    .WithMany(s => s.Loans)
                    .HasForeignKey(s => s.BookId);
            });
        }
    }
}
