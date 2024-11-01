﻿// <auto-generated />
using System;
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
    [Migration("20241101170801_UpdateUserPasswordFields")]
    partial class UpdateUserPasswordFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ABCDoubleE.Models.Book", b =>
                {
                    b.Property<int>("bookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookId"));

                    b.Property<int?>("bookshelfId")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("preferenceId")
                        .HasColumnType("int");

                    b.HasKey("bookId");

                    b.HasIndex("bookshelfId");

                    b.HasIndex("preferenceId");

                    b.ToTable("Books");
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

                    b.Property<string>("favAuthors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("favGenres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("preferenceId");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Preferences");
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

                    b.Property<string>("review")
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

            modelBuilder.Entity("ABCDoubleE.Models.Book", b =>
                {
                    b.HasOne("ABCDoubleE.Models.Bookshelf", null)
                        .WithMany("listOfBooks")
                        .HasForeignKey("bookshelfId");

                    b.HasOne("ABCDoubleE.Models.Preference", null)
                        .WithMany("favBooks")
                        .HasForeignKey("preferenceId");
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

            modelBuilder.Entity("ABCDoubleE.Models.Book", b =>
                {
                    b.Navigation("bookshelfBooks");

                    b.Navigation("reviewList");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Bookshelf", b =>
                {
                    b.Navigation("bookshelfBooks");

                    b.Navigation("listOfBooks");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Library", b =>
                {
                    b.Navigation("bookshelfList");
                });

            modelBuilder.Entity("ABCDoubleE.Models.Preference", b =>
                {
                    b.Navigation("favBooks");
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
