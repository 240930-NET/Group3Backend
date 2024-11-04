using Microsoft.EntityFrameworkCore;
namespace ABCDoubleE.Data;
using  ABCDoubleE.Models;
public partial class ABCDoubleEContext : DbContext{
    public ABCDoubleEContext(){}
    public ABCDoubleEContext(DbContextOptions<ABCDoubleEContext> options) : base(options){}

    public virtual DbSet<Book> Books {get; set; }
    public virtual DbSet<Bookshelf> Bookshelves {get; set; }
    public virtual DbSet<Library> Libraries {get; set; }
    public virtual DbSet<Preference> Preferences {get; set; }
    public virtual DbSet<Review> Reviews {get; set; }
    public virtual DbSet<User> Users {get; set; }
    public virtual DbSet<BookshelfBook> BookshelfBooks {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){

        //One User - Many Review
        modelBuilder.Entity<Review>()
            .HasOne(review => review.user)
            .WithMany(user => user.reviewList)
            .HasForeignKey(r => r.userId);
        
        //One User - One Libray
        modelBuilder.Entity<User>()
            .HasOne(user => user.library)
            .WithOne(library => library.user)
            .HasForeignKey<Library>(library => library.userId)
            .OnDelete(DeleteBehavior.Cascade);

        //One User - One Prefence *
        modelBuilder.Entity<User>()
            .HasOne(user => user.preference)
            .WithOne(preference => preference.user)
            .HasForeignKey<Preference>(preference => preference.userId);

        //One Libray to many Bookshelves
        modelBuilder.Entity<Library>()
            .HasMany(library => library.bookshelfList)
            .WithOne(book => book.library)
            .HasForeignKey(book => book.libraryId);
        //========
        //Many Book - many bookshelves
        modelBuilder.Entity<BookshelfBook>()
            .HasKey(bb => new{bb.bookshelfId,bb.bookId} );
        //1 book -> many bookshelves
        modelBuilder.Entity<BookshelfBook>()
            .HasOne(bb => bb.book)
            .WithMany(b => b.bookshelfBooks )
            .HasForeignKey(bb => bb.bookId);
        //1 bookshelf -> many books
        modelBuilder.Entity<BookshelfBook>()
            .HasOne(bb => bb.bookshelf)
            .WithMany(b => b.bookshelfBooks)
            .HasForeignKey(bb => bb.bookshelfId);
        //=========
        //One book to many review
        modelBuilder.Entity<Review>()
            .HasOne(review => review.book)
            .WithMany(book => book.reviewList)
            .HasForeignKey(review => review.bookId);

    }
}
