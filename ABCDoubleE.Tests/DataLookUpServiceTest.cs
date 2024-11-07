using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using ABCDoubleE.Services;
using ABCDoubleE.Data;
using ABCDoubleE.Models;

namespace ABCDoubleE.Tests.Services
{
    public class DatabaseLookupServiceTests
    {
        private readonly ABCDoubleEContext _context;
        private readonly DatabaseLookupService _lookupService;

        public DatabaseLookupServiceTests()
        {
            // Set up in-memory database context
            var options = new DbContextOptionsBuilder<ABCDoubleEContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ABCDoubleEContext(options);
            _lookupService = new DatabaseLookupService(_context);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldSaveWithoutErrors()
        {
            // Arrange
            var genre = new Genre { name = "Fantasy" };
            _context.Genres.Add(genre);

            // Act
            await _lookupService.SaveChangesAsync();

            // Assert
            var savedGenre = await _context.Genres.FirstOrDefaultAsync(g => g.name == "Fantasy");
            Assert.NotNull(savedGenre);
        }

        [Fact]
        public async Task TrackNewBook_ShouldAddBookToContext()
        {
            // Arrange
            var book = new Book { title = "New Book" };

            // Act
            _lookupService.TrackNewBook(book);
            await _lookupService.SaveChangesAsync(); // Ensure changes are saved

            // Assert
            var savedBook = await _context.Books.FirstOrDefaultAsync(b => b.title == "New Book");
            Assert.NotNull(savedBook);
        }
    }
}
