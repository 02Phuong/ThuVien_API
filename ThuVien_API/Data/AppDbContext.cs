using Microsoft.EntityFrameworkCore;
using ThuVien_API.Models;

namespace ThuVien_API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Books> Book { get; set; }
		public DbSet<Authors> Author { get; set; }
		public DbSet<Publishers> Publisher { get; set; }
		public DbSet<Book_Author> Book_Author { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Book_Author>()
				.HasOne(b => b.Book)
				.WithMany(ba => ba.Book_Authors)
				.HasForeignKey(bi => bi.BookID);
			builder.Entity<Book_Author>()
				.HasOne(a => a.Author)
				.WithMany(ba => ba.Book_Authors)
				.HasForeignKey(bi => bi.AuthorID);
		}

	}
}
