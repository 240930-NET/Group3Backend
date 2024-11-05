using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using  ABCDoubleE.Models;

namespace ABCDoubleE.Data;
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
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public DbSet<PreferenceGenre> PreferenceGenres { get; set; } 
    public DbSet<PreferenceAuthor> PreferenceAuthors { get; set; } 
    public DbSet<PreferenceBook> PreferenceBooks { get; set; } 

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
            .HasForeignKey<Preference>(preference => preference.userId)
            .OnDelete(DeleteBehavior.Cascade);

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

        //=======
        //Many Many Relationship between Preference and Author,Bool,Genre
        //Preference to Author

        modelBuilder.Entity<PreferenceGenre>()
            .HasKey(pg => new { pg.preferenceId, pg.genreId });

        modelBuilder.Entity<PreferenceGenre>()
            .HasOne(pg => pg.preference)
            .WithMany(preference => preference.preferenceGenres)
            .HasForeignKey(pg => pg.preferenceId);

        modelBuilder.Entity<PreferenceGenre>()
            .HasOne(pg => pg.genre)
            .WithMany()
            .HasForeignKey(pg => pg.genreId);

        modelBuilder.Entity<PreferenceAuthor>()
            .HasKey(pa => new { pa.preferenceId, pa.authorId });

        modelBuilder.Entity<PreferenceAuthor>()
            .HasOne(pa => pa.preference)
            .WithMany(preference => preference.preferenceAuthors)
            .HasForeignKey(pa => pa.preferenceId);

        modelBuilder.Entity<PreferenceAuthor>()
            .HasOne(pa => pa.author)
            .WithMany()
            .HasForeignKey(pa => pa.authorId);

        modelBuilder.Entity<PreferenceBook>()
            .HasKey(pb => new { pb.preferenceId, pb.bookId });

        modelBuilder.Entity<PreferenceBook>()
            .HasOne(pb => pb.preference)
            .WithMany(preference => preference.preferenceBooks)
            .HasForeignKey(pb => pb.preferenceId);

        modelBuilder.Entity<PreferenceBook>()
            .HasOne(pb => pb.book)
            .WithMany()
            .HasForeignKey(pb => pb.bookId);
        //book and genre
        modelBuilder.Entity<BookGenre>()
            .HasKey(bg => new { bg.BookId, bg.GenreId });

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId);

        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId);
        //book and author
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

    }
}
