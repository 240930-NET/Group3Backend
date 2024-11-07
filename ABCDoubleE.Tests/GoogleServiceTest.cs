using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using ABCDoubleE.Data;
using Newtonsoft.Json.Linq;

namespace ABCDoubleE.Tests.Services
{
    public class GoogleBooksServiceTests : IDisposable
    {
        private readonly DatabaseLookupService _lookupService;
        private readonly GoogleBooksService _googleBooksService;
        private readonly ABCDoubleEContext _context;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public GoogleBooksServiceTests()
        {
            var options = new DbContextOptionsBuilder<ABCDoubleEContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ABCDoubleEContext(options);
            _lookupService = new DatabaseLookupService(_context);

            // Set up the mocked HttpMessageHandler to handle HTTP requests
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _googleBooksService = new GoogleBooksService(httpClient, _lookupService);
        }

        [Fact]
        public async Task PopulateDatabaseWithAuthorAsync_ShouldAddBooks()
        {
            // Arrange
            string authorName = "J.K. Rowling";
            var jsonResponse = @"{
                'items': [
                    {
                        'volumeInfo': {
                            'title': 'Harry Potter and the Philosopher\'s Stone',
                            'industryIdentifiers': [{'type': 'ISBN_13', 'identifier': '1234567890123'}],
                            'description': 'A description',
                            'imageLinks': {'thumbnail': 'http://example.com/image.jpg'},
                            'authors': ['J.K. Rowling'],
                            'categories': ['Fantasy']
                        }
                    }
                ]
            }";

            // Setup the response for the HttpClient
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse),
                });

            // Act
            var books = await _googleBooksService.PopulateDatabaseWithAuthorAsync(authorName);

            // Assert
            Assert.Single(books);
            Assert.Equal("Harry Potter and the Philosopher's Stone", books[0].title);

            var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.isbn == "1234567890123");
            Assert.NotNull(dbBook);
        }

        [Fact]
        public async Task PopulateDatabaseWithTitleAsync_ShouldAddBooks()
        {
            // Arrange
            string title = "Harry Potter";
            var jsonResponse = @"{
                'items': [
                    {
                        'volumeInfo': {
                            'title': 'Harry Potter and the Chamber of Secrets',
                            'industryIdentifiers': [{'type': 'ISBN_13', 'identifier': '1234567890124'}],
                            'description': 'Another description',
                            'imageLinks': {'thumbnail': 'http://example.com/image2.jpg'},
                            'authors': ['J.K. Rowling'],
                            'categories': ['Fantasy']
                        }
                    }
                ]
            }";

            // Setup the response for the HttpClient
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse),
                });

            // Act
            var books = await _googleBooksService.PopulateDatabaseWithTitleAsync(title);

            // Assert
            Assert.Single(books);
            Assert.Equal("Harry Potter and the Chamber of Secrets", books[0].title);

            var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.isbn == "1234567890124");
            Assert.NotNull(dbBook);
        }

        [Fact]
        public async Task PopulateDatabaseWithTitleAsync_ShouldHandleEmptyResponse()
        {
            // Arrange
            string title = "Nonexistent Book";
            var jsonResponse = @"{ 'items': [] }"; // Simulating empty result

            // Setup the response for the HttpClient
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse),
                });

            // Act
            var books = await _googleBooksService.PopulateDatabaseWithTitleAsync(title);

            // Assert
            Assert.Empty(books);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
