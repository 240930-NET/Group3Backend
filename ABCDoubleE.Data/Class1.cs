using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace ABCDoubleE.Data;

public partial class ABCDoubleEContext : DbContext{
    public ABCDoubleEContext(){}
    public ABCDoubleEContext(DbContextOptions<ABCDoubleEContext> options) : base(options){}

    public virtual DbSet<Book> Books {get; set; }
    public virtual DbSet<Bookshelf> Bookshelves {get; set; }
    public virtual DbSet<Library> Libraries {get; set; }
    public virtual DbSet<Preferences> Preferences {get; set; }
    public virtual DbSet<Review> Reviews {get; set; }
    public virtual DbSet<User> Users {get; set; }
    public virtual DbSet<BookshelfBook> BookshelfBooks {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<User>()
        .HasMany(user => user.Reviews)
        .WithOne(review => review.User)

    }
}
