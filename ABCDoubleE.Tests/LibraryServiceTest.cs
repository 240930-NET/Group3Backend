using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using ABCDoubleE.Services;
using ABCDoubleE.DTOs;

namespace ABCDoubleE.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly LibraryService _libraryService;

        public LibraryServiceTests()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _libraryService = new LibraryService(_libraryRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateLibraryAsync_Success()
        {
            // Arrange
            int userId = 1;
            var library = new Library { userId = userId };
            _libraryRepositoryMock.Setup(repo => repo.AddLibraryAsync(It.IsAny<Library>())).ReturnsAsync(library);

            // Act
            var result = await _libraryService.CreateLibraryAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.userId);
            _libraryRepositoryMock.Verify(repo => repo.AddLibraryAsync(It.IsAny<Library>()), Times.Once);
        }

        [Fact]
        public async Task GetLibraryAsync_Success()
        {
            // Arrange
            int libraryId = 1;
            var library = new Library
            {
                libraryId = libraryId,
                bookshelfList = new List<Bookshelf>
                {
                    new Bookshelf { bookshelfId = 1, name = "Fiction" },
                    new Bookshelf { bookshelfId = 2, name = "Non-Fiction" }
                }
            };
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync(library);

            // Act
            var result = await _libraryService.GetLibraryAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(libraryId, result.libraryId);
            Assert.Equal(2, result.bookshelfList.Count);
            _libraryRepositoryMock.Verify(repo => repo.GetLibraryByIdAsync(libraryId), Times.Once);
        }

        [Fact]
        public async Task GetLibraryAsync_NotExist()
        {
            // Arrange
            int libraryId = 1;
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync((Library)null);

            // Act
            var result = await _libraryService.GetLibraryAsync(libraryId);

            // Assert
            Assert.Null(result);
            _libraryRepositoryMock.Verify(repo => repo.GetLibraryByIdAsync(libraryId), Times.Once);
        }

        [Fact]
        public async Task AddBookshelfAsync_Success()
        {
            // Arrange
            int libraryId = 1;
            var library = new Library
            {
                libraryId = libraryId,
                bookshelfList = new List<Bookshelf>()
            };
            var bookshelfCreateDto = new BookshelfCreateDTO { name = "New Shelf" };

            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync(library);
            _libraryRepositoryMock.Setup(repo => repo.UpdateLibraryAsync(library)).Returns(Task.CompletedTask);

            // Act
            var result = await _libraryService.AddBookshelfAsync(libraryId, bookshelfCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookshelfCreateDto.name, result.name);
            _libraryRepositoryMock.Verify(repo => repo.UpdateLibraryAsync(library), Times.Once);
        }

        [Fact]
        public async Task AddBookshelfAsync_NotExist()
        {
            // Arrange
            int libraryId = 1;
            var bookshelfCreateDto = new BookshelfCreateDTO { name = "New Shelf" };
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync((Library)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _libraryService.AddBookshelfAsync(libraryId, bookshelfCreateDto));
        }

        [Fact]
        public async Task DeleteBookshelfAsync_Success()
        {
            // Arrange
            int libraryId = 1, bookshelfId = 1;
            var library = new Library
            {
                libraryId = libraryId,
                bookshelfList = new List<Bookshelf> { new Bookshelf { bookshelfId = bookshelfId, name = "Shelf to Delete" } }
            };

            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync(library);
            _libraryRepositoryMock.Setup(repo => repo.UpdateLibraryAsync(library)).Returns(Task.CompletedTask);

            // Act
            var result = await _libraryService.DeleteBookshelfAsync(libraryId, bookshelfId);

            // Assert
            Assert.True(result);
            _libraryRepositoryMock.Verify(repo => repo.UpdateLibraryAsync(library), Times.Once);
        }

        [Fact]
        public async Task DeleteBookshelfAsync_NotExist()
        {
            // Arrange
            int libraryId = 1, bookshelfId = 1;
            var library = new Library { libraryId = libraryId, bookshelfList = new List<Bookshelf>() };

            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync(library);

            // Act
            var result = await _libraryService.DeleteBookshelfAsync(libraryId, bookshelfId);

            // Assert
            Assert.False(result);
            _libraryRepositoryMock.Verify(repo => repo.UpdateLibraryAsync(It.IsAny<Library>()), Times.Never);
        }

        [Fact]
        public async Task DeleteLibraryAsync_Success()
        {
            // Arrange
            int libraryId = 1;
            var library = new Library { libraryId = libraryId };

            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync(library);
            _libraryRepositoryMock.Setup(repo => repo.DeleteLibraryAsync(libraryId)).Returns(Task.CompletedTask);

            // Act
            var result = await _libraryService.DeleteLibraryAsync(libraryId);

            // Assert
            Assert.True(result);
            _libraryRepositoryMock.Verify(repo => repo.DeleteLibraryAsync(libraryId), Times.Once);
        }

        [Fact]
        public async Task DeleteLibraryAsync_NotExist()
        {
            // Arrange
            int libraryId = 1;
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync((Library)null);

            // Act
            var result = await _libraryService.DeleteLibraryAsync(libraryId);

            // Assert
            Assert.False(result);
            _libraryRepositoryMock.Verify(repo => repo.DeleteLibraryAsync(libraryId), Times.Never);
        }

        [Fact]
        public async Task GetBookshelvesByLibraryIdAsync_Success()
        {
            // Arrange
            int libraryId = 1;
            var bookshelves = new List<Bookshelf>
            {
                new Bookshelf { bookshelfId = 1, name = "Shelf 1" },
                new Bookshelf { bookshelfId = 2, name = "Shelf 2" }
            };
            _libraryRepositoryMock.Setup(repo => repo.GetBookshelvesByLibraryIdAsync(libraryId)).ReturnsAsync(bookshelves);

            // Act
            var result = await _libraryService.GetBookshelvesByLibraryIdAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _libraryRepositoryMock.Verify(repo => repo.GetBookshelvesByLibraryIdAsync(libraryId), Times.Once);
        }

        [Fact]
        public async Task GetLibraryIdByUserIdAsync_Success()
        {
            // Arrange
            int userId = 1, libraryId = 1;
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryIdByUserIdAsync(userId)).ReturnsAsync(libraryId);

            // Act
            var result = await _libraryService.GetLibraryIdByUserIdAsync(userId);

            // Assert
            Assert.Equal(libraryId, result);
            _libraryRepositoryMock.Verify(repo => repo.GetLibraryIdByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetAllBookshelvesAsync_Success()
        {
            // Arrange
            int libraryId = 1;
            var library = new Library
            {
                libraryId = libraryId,
                bookshelfList = new List<Bookshelf>
                {
                    new Bookshelf { bookshelfId = 1, name = "Fiction" },
                    new Bookshelf { bookshelfId = 2, name = "Non-Fiction" }
                }
            };
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync(library);

            // Act
            var result = await _libraryService.GetAllBookshelvesAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _libraryRepositoryMock.Verify(repo => repo.GetLibraryByIdAsync(libraryId), Times.Once);
        }

        [Fact]
        public async Task GetAllBookshelvesAsync_NotExist()
        {
            // Arrange
            int libraryId = 1;
            _libraryRepositoryMock.Setup(repo => repo.GetLibraryByIdAsync(libraryId)).ReturnsAsync((Library)null);

            // Act
            var result = await _libraryService.GetAllBookshelvesAsync(libraryId);

            // Assert
            Assert.Empty(result);
            _libraryRepositoryMock.Verify(repo => repo.GetLibraryByIdAsync(libraryId), Times.Once);
        }
    }
}
