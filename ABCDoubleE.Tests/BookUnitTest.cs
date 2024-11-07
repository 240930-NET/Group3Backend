using Moq;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using ABCDoubleE.Repositories;
using ABCDoubleE.DTOs;

public class BookServiceTests
{
    //private readonly IConfiguration _configuration;
    //private readonly BookService _bookService;

    //private readonly BookRepo _bookRepo;


    [Fact]
    public void GetAllBooksServiceIsNullOrEmptyExceptionThrown()
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [];

        mockRepo.Setup(repo => repo.GetAllBooks()).Returns(bookList);

        // Assert
        Assert.Throws<Exception>(()=> bookService.GetAllBooks());
    }

    [Fact]
    public void GetAllBooksServiceIsNotEmptyOrNull()
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [ new Book{}, ];

        mockRepo.Setup(repo => repo.GetAllBooks()).Returns(bookList);

        //Act
        var result = bookService.GetAllBooks();

        // Assert
        Assert.NotEmpty(result);
    }

    [Theory]
    [InlineData("1111111111")]
    public void GetAllBookByISBNIsNotNullOrEmpty(string isbn)
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);


        List<Book> bookList= [ 
            new Book{ isbn = "1111111111"},
            new Book{ isbn = "2222222222"}
        ];

        mockRepo.Setup(repo => repo.GetBookByISBN(It.IsAny<string>()))!
            .Returns(bookList.FirstOrDefault(book=> book.isbn == isbn));

        //Act
        var result = bookService.GetBookByISBN(isbn);

        // Assert
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("1111111111")]
    public void GetAllBookByISBN(string isbn)
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);


        List<Book> bookList= [ 
            new Book{ isbn = "1234567890"},
            new Book{ isbn = "2222222222"}
        ];

        mockRepo.Setup(repo => repo.GetBookByISBN(It.IsAny<string>()))!
            .Returns(bookList.FirstOrDefault(book=> book.isbn == isbn));

        //Act
        //var result = bookService.GetBookByISBN(isbn);

        // Assert
        Assert.Throws<Exception>(()=> bookService.GetBookByISBN(isbn));
    }

    [Fact]
    public void AddBookISBNNotNullOrEmptyOrDuplicate()
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [ new Book{ isbn = "1111111111"}, ];

        BookDTO bookDTO = new(){
            isbn="1234567890"
        };
        Book book = new(){
            isbn="1234567890"
        };

        mockRepo.Setup(repo => repo.AddBook(It.IsAny<Book>())).Callback(()=> bookList.Add(book));

        //Act
        var result = bookService.AddBook(bookDTO);

        // Assert
        Assert.Equal(result, "Book added");
        Assert.Contains(bookList, b => b.isbn.Equals("1234567890"));
        mockRepo.Verify(r => r.AddBook(It.IsAny<Book>()), Times.Exactly(1));
    }

    [Fact]
    public void AddBookISBNNullOrEmpty()
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [ new Book{ isbn = "1111111111"}, ];

        BookDTO bookDTO = new(){
            isbn=""
        };
        Book book = new(){
            isbn=""
        };

        mockRepo.Setup(repo => repo.AddBook(It.IsAny<Book>())).Callback(()=> bookList.Add(book));

        //Act
        //var result = bookService.AddBook(bookDTO);

        // Assert
        Assert.Throws<Exception>(()=> bookService.AddBook(bookDTO));
    }

    [Fact]
    public void AddBookISBNDuplicate()
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [ new Book{ isbn = "1111111111"}, ];

        BookDTO bookDTO = new(){
            isbn="1111111111"
        };
        Book book = new(){
            isbn="1111111111"
        };

        mockRepo.Setup(repo => repo.GetBookByISBN(It.IsAny<string>()))!
            .Returns(bookList.FirstOrDefault(book=> book.isbn == "1111111111"));

        mockRepo.Setup(repo => repo.AddBook(It.IsAny<Book>())).Callback(()=> bookList.Add(book));

        //Act
        //var result = bookService.AddBook(bookDTO);

        // Assert
        Assert.Throws<Exception>(()=> bookService.AddBook(bookDTO));
    }

    [Theory]
    [InlineData("1")]
    public void DeleteBookISBN(string isbn)
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [ new Book{ isbn ="1"}, ];

        mockRepo.Setup(repo => repo.DeleteBook(It.IsAny<Book>()))
        .Callback(()=> bookList.Remove(bookList.FirstOrDefault(book=> book.isbn == isbn)));

        mockRepo.Setup(repo => repo.GetBookByISBN(It.IsAny<string>()))!
            .Returns(bookList.FirstOrDefault(book=> book.isbn == isbn));

        //Act
        var result = bookService.DeleteBook(isbn);

        // Assert
        Assert.Equal(result, $"Book deleted succesfully with ISBN: {isbn}");
    }

    [Theory]
    [InlineData("2222222222")]
    public void DeleteBookISBNBookNotFound(string isbn)
    {
        // Arrange
        Mock<IBookRepo> mockRepo = new();
        BookService bookService = new(mockRepo.Object);

        List<Book> bookList= [ new Book{ isbn = "1111111111"}, ];

        mockRepo.Setup(repo => repo.DeleteBook(It.IsAny<Book>()))
        .Callback(()=> bookList.Remove(bookList.FirstOrDefault(book=> book.isbn == isbn)));

        mockRepo.Setup(repo => repo.GetBookByISBN(It.IsAny<string>()))!
            .Returns(bookList.FirstOrDefault(book=> book.isbn == isbn));

        //Act

        // Assert
        Assert.Throws<Exception>(()=> bookService.DeleteBook(isbn));
    }


}