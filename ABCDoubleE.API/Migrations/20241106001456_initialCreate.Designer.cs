﻿// <auto-generated />
using ABCDoubleE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ABCDoubleE.API.Migrations
{
    [DbContext(typeof(ABCDoubleEContext))]
    [Migration("20241106001456_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ABCDoubleE.Models.Author", b =>
                {
                    b.Property<int>("authorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("authorId"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("authorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Book", b =>
                {
                    b.Property<int>("bookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("bookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ABCDoubleE.Models.BookAuthor", b =>
                {
                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<int>("authorId")
                        .HasColumnType("int");

                    b.HasKey("bookId", "authorId");

                    b.HasIndex("authorId");

                    b.ToTable("BookAuthor");
                });

            modelBuilder.Entity("ABCDoubleE.Models.BookGenre", b =>
                {
                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<int>("genreId")
                        .HasColumnType("int");

                    b.HasKey("bookId", "genreId");

                    b.HasIndex("genreId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Bookshelf", b =>
                {
                    b.Property<int>("bookshelfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookshelfId"));

                    b.Property<int>("libraryId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("bookshelfId");

                    b.HasIndex("libraryId");

                    b.ToTable("Bookshelves");
                });

            modelBuilder.Entity("ABCDoubleE.Models.BookshelfBook", b =>
                {
                    b.Property<int>("bookshelfId")
                        .HasColumnType("int");

                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.HasKey("bookshelfId", "bookId");

                    b.HasIndex("bookId");

                    b.ToTable("BookshelfBooks");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Genre", b =>
                {
                    b.Property<int>("genreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("genreId"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("genreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Library", b =>
                {
                    b.Property<int>("libraryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("libraryId"));

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("libraryId");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Preference", b =>
                {
                    b.Property<int>("preferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("preferenceId"));

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("preferenceId");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("ABCDoubleE.Models.PreferenceAuthor", b =>
                {
                    b.Property<int>("preferenceId")
                        .HasColumnType("int");

                    b.Property<int>("authorId")
                        .HasColumnType("int");

                    b.HasKey("preferenceId", "authorId");

                    b.HasIndex("authorId");

                    b.ToTable("PreferenceAuthors");
                });

            modelBuilder.Entity("ABCDoubleE.Models.PreferenceBook", b =>
                {
                    b.Property<int>("preferenceId")
                        .HasColumnType("int");

                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.HasKey("preferenceId", "bookId");

                    b.HasIndex("bookId");

                    b.ToTable("PreferenceBooks");
                });

            modelBuilder.Entity("ABCDoubleE.Models.PreferenceGenre", b =>
                {
                    b.Property<int>("preferenceId")
                        .HasColumnType("int");

                    b.Property<int>("genreId")
                        .HasColumnType("int");

                    b.HasKey("preferenceId", "genreId");

                    b.HasIndex("genreId");

                    b.ToTable("PreferenceGenres");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Review", b =>
                {
                    b.Property<int>("reviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reviewId"));

                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<string>("reviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("reviewId");

                    b.HasIndex("bookId");

                    b.HasIndex("userId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ABCDoubleE.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("libraryId")
                        .HasColumnType("int");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("preferenceId")
                        .HasColumnType("int");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ABCDoubleE.Models.BookAuthor", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Author", "author")
                        .WithMany("bookAuthors")
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.Book", "book")
                        .WithMany("bookAuthors")
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");

                    b.Navigation("book");
                });

            modelBuilder.Entity("ABCDoubleE.Models.BookGenre", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Book", "book")
                        .WithMany("bookGenres")
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.Genre", "genre")
                        .WithMany("bookGenres")
                        .HasForeignKey("genreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("genre");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Bookshelf", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Library", "library")
                        .WithMany("bookshelfList")
                        .HasForeignKey("libraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("library");
                });

            modelBuilder.Entity("ABCDoubleE.Models.BookshelfBook", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Book", "book")
                        .WithMany("bookshelfBooks")
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.Bookshelf", "bookshelf")
                        .WithMany("bookshelfBooks")
                        .HasForeignKey("bookshelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("bookshelf");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Library", b =>
                {
                    b.HasOne("ABCDoubleE.Models.User", "user")
                        .WithOne("library")
                        .HasForeignKey("ABCDoubleE.Models.Library", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Preference", b =>
                {
                    b.HasOne("ABCDoubleE.Models.User", "user")
                        .WithOne("preference")
                        .HasForeignKey("ABCDoubleE.Models.Preference", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("ABCDoubleE.Models.PreferenceAuthor", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Author", "author")
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.Preference", "preference")
                        .WithMany("preferenceAuthors")
                        .HasForeignKey("preferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");

                    b.Navigation("preference");
                });

            modelBuilder.Entity("ABCDoubleE.Models.PreferenceBook", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Book", "book")
                        .WithMany()
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.Preference", "preference")
                        .WithMany("preferenceBooks")
                        .HasForeignKey("preferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("preference");
                });

            modelBuilder.Entity("ABCDoubleE.Models.PreferenceGenre", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Genre", "genre")
                        .WithMany()
                        .HasForeignKey("genreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.Preference", "preference")
                        .WithMany("preferenceGenres")
                        .HasForeignKey("preferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("genre");

                    b.Navigation("preference");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Review", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Book", "book")
                        .WithMany("reviewList")
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABCDoubleE.Models.User", "user")
                        .WithMany("reviewList")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Author", b =>
                {
                    b.Navigation("bookAuthors");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Book", b =>
                {
                    b.Navigation("bookAuthors");

                    b.Navigation("bookGenres");

                    b.Navigation("bookshelfBooks");

                    b.Navigation("reviewList");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Bookshelf", b =>
                {
                    b.Navigation("bookshelfBooks");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Genre", b =>
                {
                    b.Navigation("bookGenres");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Library", b =>
                {
                    b.Navigation("bookshelfList");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Preference", b =>
                {
                    b.Navigation("preferenceAuthors");

                    b.Navigation("preferenceBooks");

                    b.Navigation("preferenceGenres");
                });

            modelBuilder.Entity("ABCDoubleE.Models.User", b =>
                {
                    b.Navigation("library")
                        .IsRequired();

                    b.Navigation("preference")
                        .IsRequired();

                    b.Navigation("reviewList");
                });
#pragma warning restore 612, 618
        }
    }
}
