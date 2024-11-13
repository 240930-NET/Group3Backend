using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using ABCDoubleE.Services;

namespace ABCDoubleE.Tests.Services
{
    public class PreferenceServiceTests
    {
        private readonly Mock<IPreferenceRepository> _preferenceRepositoryMock;
        private readonly PreferenceService _preferenceService;

        public PreferenceServiceTests()
        {
            _preferenceRepositoryMock = new Mock<IPreferenceRepository>();
            _preferenceService = new PreferenceService(_preferenceRepositoryMock.Object);
        }

        [Fact]
        public async Task AddGenreToPreferenceAsync_Successful()
        {
            // Arrange
            int userId = 1, genreId = 1;
            var preference = new Preference
            {
                userId = userId,
                preferenceGenres = new List<PreferenceGenre>()
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceByUserIdAsync(userId)).ReturnsAsync(preference);
            _preferenceRepositoryMock.Setup(repo => repo.AddGenreToPreferenceAsync(preference, genreId)).ReturnsAsync(true);

            // Act
            var result = await _preferenceService.AddGenreToPreferenceAsync(userId, genreId);

            // Assert
            Assert.True(result);
            _preferenceRepositoryMock.Verify(repo => repo.AddGenreToPreferenceAsync(preference, genreId), Times.Once);
        }

        [Fact]
        public async Task AddGenreToPreferenceAsync_AlreadyExists()
        {
            // Arrange
            int userId = 1, genreId = 1;
            var preference = new Preference
            {
                userId = userId,
                preferenceGenres = new List<PreferenceGenre> { new PreferenceGenre { genreId = genreId } }
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceByUserIdAsync(userId)).ReturnsAsync(preference);

            // Act
            var result = await _preferenceService.AddGenreToPreferenceAsync(userId, genreId);

            // Assert
            Assert.False(result);
            _preferenceRepositoryMock.Verify(repo => repo.AddGenreToPreferenceAsync(It.IsAny<Preference>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task RemoveGenreFromPreferenceAsync_Successful()
        {
            // Arrange
            int preferenceId = 1, genreId = 1;
            var preference = new Preference
            {
                preferenceId = preferenceId,
                preferenceGenres = new List<PreferenceGenre> { new PreferenceGenre { genreId = genreId, preferenceId = preferenceId } }
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithGenresByUserIdAsync(preferenceId)).ReturnsAsync(preference);
            _preferenceRepositoryMock.Setup(repo => repo.RemoveGenreFromPreferenceAsync(It.IsAny<PreferenceGenre>())).Returns(Task.CompletedTask);

            // Act
            var result = await _preferenceService.RemoveGenreFromPreferenceAsync(preferenceId, genreId);

            // Assert
            Assert.True(result);
            _preferenceRepositoryMock.Verify(repo => repo.RemoveGenreFromPreferenceAsync(It.IsAny<PreferenceGenre>()), Times.Once);
        }

        [Fact]
        public async Task RemoveGenreFromPreferenceAsync_NotFound()
        {
            // Arrange
            int preferenceId = 1, genreId = 2;
            var preference = new Preference
            {
                preferenceId = preferenceId,
                preferenceGenres = new List<PreferenceGenre> { new PreferenceGenre { genreId = 1, preferenceId = preferenceId } }
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithGenresByUserIdAsync(preferenceId)).ReturnsAsync(preference);

            // Act
            var result = await _preferenceService.RemoveGenreFromPreferenceAsync(preferenceId, genreId);

            // Assert
            Assert.False(result);
            _preferenceRepositoryMock.Verify(repo => repo.RemoveGenreFromPreferenceAsync(It.IsAny<PreferenceGenre>()), Times.Never);
        }

        [Fact]
        public async Task AddAuthorToPreferenceAsync_Successful()
        {
            // Arrange
            int userId = 1, authorId = 1;
            var preference = new Preference
            {
                userId = userId,
                preferenceAuthors = new List<PreferenceAuthor>()
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceByUserIdAsync(userId)).ReturnsAsync(preference);
            _preferenceRepositoryMock.Setup(repo => repo.AddAuthorToPreferenceAsync(preference, authorId)).ReturnsAsync(true);

            // Act
            var result = await _preferenceService.AddAuthorToPreferenceAsync(userId, authorId);

            // Assert
            Assert.True(result);
            _preferenceRepositoryMock.Verify(repo => repo.AddAuthorToPreferenceAsync(preference, authorId), Times.Once);
        }

        [Fact]
        public async Task RemoveAuthorFromPreferenceAsync_Successful()
        {
            // Arrange
            int preferenceId = 1, authorId = 1;
            var preference = new Preference
            {
                preferenceId = preferenceId,
                preferenceAuthors = new List<PreferenceAuthor> { new PreferenceAuthor { authorId = authorId, preferenceId = preferenceId } }
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithAuthorsByUserIdAsync(preferenceId)).ReturnsAsync(preference);
            _preferenceRepositoryMock.Setup(repo => repo.RemoveAuthorFromPreferenceAsync(It.IsAny<PreferenceAuthor>())).Returns(Task.CompletedTask);

            // Act
            var result = await _preferenceService.RemoveAuthorFromPreferenceAsync(preferenceId, authorId);

            // Assert
            Assert.True(result);
            _preferenceRepositoryMock.Verify(repo => repo.RemoveAuthorFromPreferenceAsync(It.IsAny<PreferenceAuthor>()), Times.Once);
        }

        [Fact]
        public async Task AddBookToPreferenceAsync_Successful()
        {
            // Arrange
            int userId = 1, bookId = 1;
            var preference = new Preference
            {
                userId = userId,
                preferenceBooks = new List<PreferenceBook>()
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceByUserIdAsync(userId)).ReturnsAsync(preference);
            _preferenceRepositoryMock.Setup(repo => repo.AddBookToPreferenceAsync(preference, bookId)).ReturnsAsync(true);

            // Act
            var result = await _preferenceService.AddBookToPreferenceAsync(userId, bookId);

            // Assert
            Assert.True(result);
            _preferenceRepositoryMock.Verify(repo => repo.AddBookToPreferenceAsync(preference, bookId), Times.Once);
        }

        [Fact]
        public async Task RemoveBookFromPreferenceAsync_Successful()
        {
            // Arrange
            int preferenceId = 1, bookId = 1;
            var preference = new Preference
            {
                preferenceId = preferenceId,
                preferenceBooks = new List<PreferenceBook> { new PreferenceBook { bookId = bookId, preferenceId = preferenceId } }
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithBooksByUserIdAsync(preferenceId)).ReturnsAsync(preference);
            _preferenceRepositoryMock.Setup(repo => repo.RemoveBookFromPreferenceAsync(It.IsAny<PreferenceBook>())).Returns(Task.CompletedTask);

            // Act
            var result = await _preferenceService.RemoveBookFromPreferenceAsync(preferenceId, bookId);

            // Assert
            Assert.True(result);
            _preferenceRepositoryMock.Verify(repo => repo.RemoveBookFromPreferenceAsync(It.IsAny<PreferenceBook>()), Times.Once);
        }

        [Fact]
        public async Task RemoveBookFromPreferenceAsync_NotFound()
        {
            // Arrange
            int preferenceId = 1, bookId = 2;
            var preference = new Preference
            {
                preferenceId = preferenceId,
                preferenceBooks = new List<PreferenceBook> { new PreferenceBook { bookId = 1, preferenceId = preferenceId } }
            };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithBooksByUserIdAsync(preferenceId)).ReturnsAsync(preference);

            // Act
            var result = await _preferenceService.RemoveBookFromPreferenceAsync(preferenceId, bookId);

            // Assert
            Assert.False(result);
            _preferenceRepositoryMock.Verify(repo => repo.RemoveBookFromPreferenceAsync(It.IsAny<PreferenceBook>()), Times.Never);
        }

        [Fact]
        public async Task CreatePreferenceAsync_Success()
        {
            int userId = 1;
            var preference = new Preference { userId = userId };
            

            _preferenceRepositoryMock.Setup(repo => repo.CreatePreferenceAsync(It.IsAny<Preference>()))
                                    .ReturnsAsync(preference);

            // Act
            var result = await _preferenceService.CreatePreferenceAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.userId);
        }

        [Fact]
        public async Task GetPreferenceByUserIdAsync_Success()
        {
            // Arrange
            int userId = 1;
            var preference = new Preference { userId = userId };
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceByUserIdAsync(userId)).ReturnsAsync(preference);

            // Act
            var result = await _preferenceService.GetPreferenceByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.userId);
        }

        [Fact]
        public async Task UpdatePreferenceAsync_Success()
        {
            // Arrange
            var preference = new Preference { preferenceId = 1, userId = 1 };
            _preferenceRepositoryMock.Setup(repo => repo.UpdatePreferenceAsync(preference)).Returns(Task.CompletedTask);

            // Act
            await _preferenceService.UpdatePreferenceAsync(preference);

            // Assert
            _preferenceRepositoryMock.Verify(repo => repo.UpdatePreferenceAsync(preference), Times.Once);
        }

        [Fact]
        public async Task DeletePreferenceByUserIdAsync_Success()
        {
            // Arrange
            int userId = 1;

            // Act
            await _preferenceService.DeletePreferenceByUserIdAsync(userId);

            // Assert
            _preferenceRepositoryMock.Verify(repo => repo.DeletePreferenceByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task RemoveGenreFromPreferenceAsync_NonExist()
        {
            // Arrange
            int preferenceId = 1, genreId = 1;
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithGenresByUserIdAsync(preferenceId)).ReturnsAsync((Preference)null);

            // Act
            var result = await _preferenceService.RemoveGenreFromPreferenceAsync(preferenceId, genreId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveAuthorFromPreferenceAsync_NoExist()
        {
            // Arrange
            int preferenceId = 1, authorId = 1;
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithAuthorsByUserIdAsync(preferenceId)).ReturnsAsync((Preference)null);

            // Act
            var result = await _preferenceService.RemoveAuthorFromPreferenceAsync(preferenceId, authorId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveBookFromPreferenceAsync_Nonxist()
        {
            // Arrange
            int preferenceId = 1, bookId = 1;
            _preferenceRepositoryMock.Setup(repo => repo.GetPreferenceWithBooksByUserIdAsync(preferenceId)).ReturnsAsync((Preference)null);

            // Act
            var result = await _preferenceService.RemoveBookFromPreferenceAsync(preferenceId, bookId);

            // Assert
            Assert.False(result);
        }
    }
}
