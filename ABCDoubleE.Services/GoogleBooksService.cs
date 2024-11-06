using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ABCDoubleE.Models;
using Microsoft.EntityFrameworkCore;
namespace ABCDoubleE.Services;
public class GoogleBooksService
{
    private readonly HttpClient _httpClient;
    private readonly DatabaseLookupService _lookupService;
    private const string GoogleBooksApiUrl = "https://www.googleapis.com/books/v1/volumes?q=";

    public GoogleBooksService(HttpClient httpClient, DatabaseLookupService lookupService)
    {
        _httpClient = httpClient;
        _lookupService = lookupService;
    }

    // Method to search for books by a specific author and check for existing records
    public async Task<List<Book>> SearchBooksByAuthorAsync(string authorName)
    {
        var response = await _httpClient.GetStringAsync(GoogleBooksApiUrl + $"inauthor:{authorName}");
        var json = JObject.Parse(response);

        var books = new List<Book>();

        foreach (var item in json["items"])
        {
            var volumeInfo = item["volumeInfo"];
            var title = volumeInfo["title"]?.ToString() ?? "";
            var isbn = volumeInfo["industryIdentifiers"]?.FirstOrDefault(i => i["type"]?.ToString() == "ISBN_13")?["identifier"]?.ToString() ?? "";
            var description = volumeInfo["description"]?.ToString() ?? "";
            var imageLink = volumeInfo["imageLinks"]?["thumbnail"]?.ToString() ?? "";
            var authors = volumeInfo["authors"]?.ToObject<List<string>>() ?? new List<string>();
            var categories = volumeInfo["categories"]?.ToObject<List<string>>() ?? new List<string>();

            var existingBook = await _lookupService.GetExistingBookAsync(isbn, title);
            if (existingBook != null)
            {
                books.Add(existingBook);
                continue; 
            }


            var book = new Book
            {
                title = title,
                isbn = isbn,
                description = description,
                image = imageLink,
                bookAuthors = new List<BookAuthor>(),
                bookGenres = new List<BookGenre>()
            };

            foreach (var anAuthor in authors) 
            {
                var existingAuthor = await _lookupService.GetExistingAuthorAsync(anAuthor);
                var authorEntity = existingAuthor ?? new Author { name = anAuthor };
                book.bookAuthors.Add(new BookAuthor { book = book, author = authorEntity });
            }


              foreach (var category in categories)
                {
                    Genre genreEntity;
                    try
                    {
                        genreEntity = await _lookupService.GetOrCreateGenreAsync(category);
                    }
                    catch (DbUpdateException)
                    {
                        genreEntity = await _lookupService.GetExistingGenreAsync(category);
                    }

                    if (!book.bookGenres.Any(bg => bg.genre.name == genreEntity.name))
                    {
                        book.bookGenres.Add(new BookGenre { book = book, genre = genreEntity });
                    }
                }


            books.Add(book);
        }

        return books;
    }
}
