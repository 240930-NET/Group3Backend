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
    //note:
    // AuthorId and Genid need to be saved first then book, as bookgenre and bookauthor relied on them.
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

        // HashSets to cache unique genres and authors during this session
        var genreCache = new HashSet<string>();
        var authorCache = new HashSet<string>();
        var bookCache = new HashSet<string>();

        foreach (var item in json["items"])
        {
            var volumeInfo = item["volumeInfo"];
            var title = volumeInfo["title"]?.ToString() ?? "";
            var isbn = volumeInfo["industryIdentifiers"]?.FirstOrDefault(i => i["type"]?.ToString() == "ISBN_13")?["identifier"]?.ToString() ?? "";
            var description = volumeInfo["description"]?.ToString() ?? "";
            var imageLink = volumeInfo["imageLinks"]?["thumbnail"]?.ToString() ?? "";
            var authors = volumeInfo["authors"]?.ToObject<List<string>>() ?? new List<string>();
            var categories = volumeInfo["categories"]?.ToObject<List<string>>() ?? new List<string>();

            // Check if book already exists in the cache or database
            if (bookCache.Contains(isbn) || (await _lookupService.GetExistingBookAsync(isbn, title)) != null)
            {
                continue;
            }

            // Create a new book if not found in cache or database
            var book = new Book
            {
                title = title,
                isbn = isbn,
                description = description,
                image = imageLink,
                bookAuthors = new List<BookAuthor>(),
                bookGenres = new List<BookGenre>()
            };
            bookCache.Add(isbn);

            // Add authors to the book
            foreach (var anAuthor in authors)
            {
                if (!authorCache.Contains(anAuthor))
                {
                    var authorEntity = await _lookupService.GetOrAddAuthorAsync(anAuthor); // Save if new
                    authorCache.Add(anAuthor);
                }
                book.bookAuthors.Add(new BookAuthor { book = book, author = await _lookupService.GetExistingAuthorAsync(anAuthor) });
            }

            // Add genres to the book
            foreach (var category in categories)
            {
                if (!genreCache.Contains(category))
                {
                    var genreEntity = await _lookupService.GetOrAddGenreAsync(category); // Save if new
                    genreCache.Add(category);
                }
                book.bookGenres.Add(new BookGenre { book = book, genre = await _lookupService.GetExistingGenreAsync(category) });
            }

            // Track the new book without saving immediately
            _lookupService.TrackNewBook(book);
            books.Add(book);
        }

        // Save all changes in one transaction at the end
        await _lookupService.SaveChangesAsync();

        return books;
    }


    public async Task<List<Book>> SearchBooksByTitleAsync(string title)
    {
        var books = new List<Book>();

        // Retrieve existing ISBNs using _lookupService to filter duplicates
        var existingISBNs = await _lookupService.GetExistingISBNsAsync();

        try
        {
            var response = await _httpClient.GetStringAsync(GoogleBooksApiUrl + title);
            var json = JObject.Parse(response);

            if (json["items"] != null)
            {
                foreach (var item in json["items"])
                {
                    var volumeInfo = item["volumeInfo"];
                    var bookIsbn = volumeInfo["industryIdentifiers"]?.FirstOrDefault(i => i["type"]?.ToString() == "ISBN_13")?["identifier"]?.ToString() ?? "";

                    // Skip this book if the ISBN already exists in the database
                    if (existingISBNs.Contains(bookIsbn)) continue;

                    var book = new Book
                    {
                        title = volumeInfo["title"]?.ToString() ?? "",
                        isbn = bookIsbn,
                        description = volumeInfo["description"]?.ToString() ?? "",
                        image = volumeInfo["imageLinks"]?["thumbnail"]?.ToString() ?? "",
                        bookAuthors = new List<BookAuthor>(),
                        bookGenres = new List<BookGenre>()
                    };

                    // Add author names as BookAuthor objects
                    if (volumeInfo["authors"] != null)
                    {
                        foreach (var authorName in volumeInfo["authors"])
                        {
                            var bookAuthor = new BookAuthor
                            {
                                author = new Author { name = authorName.ToString() }
                            };
                            book.bookAuthors.Add(bookAuthor);
                        }
                    }

                    // Add genre names as BookGenre objects
                    if (volumeInfo["categories"] != null)
                    {
                        foreach (var categoryName in volumeInfo["categories"])
                        {
                            var bookGenre = new BookGenre
                            {
                                genre = new Genre { name = categoryName.ToString() }
                            };
                            book.bookGenres.Add(bookGenre);
                        }
                    }

                    books.Add(book); // Add the book to the result list
                }
            }
            else
            {
                Console.WriteLine("No items found in the response.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching books: {ex.Message}");
        }

        return books;
    }

}
