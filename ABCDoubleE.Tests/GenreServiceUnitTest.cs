using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using ABCDoubleE.Services;

namespace ABCDoubleE.Tests.Services
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly GenreService _genreService;

        public GenreServiceTests()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _genreService = new GenreService(_genreRepositoryMock.Object);
        }

        [Fact]
        public async Task AddGenreAsync_Successful()
        {
            // Arrange
            var genre = new Genre { genreId = 1, name = "Fiction" };
            _genreRepositoryMock.Setup(repo => repo.AddGenreAsync(genre)).ReturnsAsync(genre);

            // Act
            var result = await _genreService.AddGenreAsync(genre);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(genre.genreId, result.genreId);
            Assert.Equal(genre.name, result.name);
            _genreRepositoryMock.Verify(repo => repo.AddGenreAsync(genre), Times.Once);
        }

        [Fact]
        public async Task AddGenreAsync_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var genre = new Genre { genreId = 1, name = "Fantasy" };
            _genreRepositoryMock.Setup(repo => repo.AddGenreAsync(genre)).ThrowsAsync(new Exception("Failed to add genre"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _genreService.AddGenreAsync(genre));
            Assert.Equal("Failed to add genre", exception.Message);
            _genreRepositoryMock.Verify(repo => repo.AddGenreAsync(genre), Times.Once);
        }

        [Fact]
        public async Task SearchGenresAsync_Successful()
        {
            // Arrange
            var searchQuery = "Fic";
            var genres = new List<Genre>
            {
                new Genre { genreId = 1, name = "Fiction" },
                new Genre { genreId = 2, name = "Science Fiction" }
            };
            _genreRepositoryMock.Setup(repo => repo.SearchGenresAsync(searchQuery)).ReturnsAsync(genres);

            // Act
            var result = await _genreService.SearchGenresAsync(searchQuery);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.name == "Fiction");
            Assert.Contains(result, g => g.name == "Science Fiction");
            _genreRepositoryMock.Verify(repo => repo.SearchGenresAsync(searchQuery), Times.Once);
        }

        [Fact]
        public async Task SearchGenresAsync_NothFound()
        {
            // Arrange
            var searchQuery = "Nonexistent Genre";
            _genreRepositoryMock.Setup(repo => repo.SearchGenresAsync(searchQuery)).ReturnsAsync(new List<Genre>());

            // Act
            var result = await _genreService.SearchGenresAsync(searchQuery);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _genreRepositoryMock.Verify(repo => repo.SearchGenresAsync(searchQuery), Times.Once);
        }
    }
}
