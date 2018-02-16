using Microsoft.EntityFrameworkCore;
using RandomQuotes.Models;

namespace RandomQuotes.Data
{
    public class QuoteContext : DbContext
    {
        public QuoteContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Quote>().ToTable("Quote");
        }
    }
}
