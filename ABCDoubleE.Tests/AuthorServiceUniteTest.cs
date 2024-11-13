using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using ABCDoubleE.Services;

namespace ABCDoubleE.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_authorRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAuthorAsync_Success()
        {
            // Arrange
            var author = new Author { authorId = 1, name = "J.K. Rowling" };
            _authorRepositoryMock.Setup(repo => repo.AddAuthorAsync(author)).ReturnsAsync(author);

            // Act
            var result = await _authorService.AddAuthorAsync(author);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(author.authorId, result.authorId);
            Assert.Equal(author.name, result.name);
            _authorRepositoryMock.Verify(repo => repo.AddAuthorAsync(author), Times.Once);
        }

        [Fact]
        public async Task AddAuthorAsync_Fails()
        {
            // Arrange
            var author = new Author { authorId = 1, name = "George Orwell" };
            _authorRepositoryMock.Setup(repo => repo.AddAuthorAsync(author)).ThrowsAsync(new Exception("Failed to add author"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _authorService.AddAuthorAsync(author));
            Assert.Equal("Failed to add author", exception.Message);
            _authorRepositoryMock.Verify(repo => repo.AddAuthorAsync(author), Times.Once);
        }

        [Fact]
        public async Task SearchAuthorsAsync_Success()
        {
            // Arrange
            var searchQuery = "Row";
            var authors = new List<Author>
            {
                new Author { authorId = 1, name = "J.K. Rowling" },
                new Author { authorId = 2, name = "Rowan Coleman" }
            };
            _authorRepositoryMock.Setup(repo => repo.SearchAuthorsAsync(searchQuery)).ReturnsAsync(authors);

            // Act
            var result = await _authorService.SearchAuthorsAsync(searchQuery);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, a => a.name == "J.K. Rowling");
            Assert.Contains(result, a => a.name == "Rowan Coleman");
            _authorRepositoryMock.Verify(repo => repo.SearchAuthorsAsync(searchQuery), Times.Once);
        }

        [Fact]
        public async Task SearchAuthorsAsync_NotFound()
        {
            // Arrange
            var searchQuery = "Nonexistent Author";
            _authorRepositoryMock.Setup(repo => repo.SearchAuthorsAsync(searchQuery)).ReturnsAsync(new List<Author>());

            // Act
            var result = await _authorService.SearchAuthorsAsync(searchQuery);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _authorRepositoryMock.Verify(repo => repo.SearchAuthorsAsync(searchQuery), Times.Once);
        }
    }
}
