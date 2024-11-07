using ABCDoubleE.API;
using ABCDoubleE.Repositories;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using ABCDoubleE.Data;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Moq;


using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ABCDoubleE.Models;
using ABCDoubleE.Services;
using System.Collections.Generic;
using ABCDoubleE.DTOs;


namespace ABCDoubleE.Tests;

public class BookshelfServiceTests
{
    [Fact]
    public void GetAllBookshelfReturnsNullonEmpty()
    {
        // Arrange
        Mock<IBookshelfRepo> mockRepo = new();
        BookshelfService bookshelfService = new(mockRepo.Object);

        List<Bookshelf> exList = [];

        mockRepo.Setup(repo => repo.GetAllBookshelfRecords())
            .Returns(exList);

    
        // Act

        var result = bookshelfService.GetAllBookshelfRecords();
    
        // Assert
        Assert.Null(result);
    }


    [Fact]

    public void GetAllBookshelfReturnsProperList()
    {
        //Arrange
        Mock<IBookshelfRepo> mockRepo = new();
        BookshelfService bookshelfService = new(mockRepo.Object);

          List<Bookshelf> exList = [
            new Bookshelf{ name = "SunBook"},
            new Bookshelf{},
            new Bookshelf{}
          ];

        mockRepo.Setup(repo => repo.GetAllBookshelfRecords())
            .Returns(exList);

             // Act

        var result = bookshelfService.GetAllBookshelfRecords();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Contains(result, e => e.name!.Equals("SunBook"));


        
    }

    [Fact]
    public async Task AddNewBookshelfAddsToList()
    {
        //Arrange
        Mock<IBookshelfRepo> mockRepo = new();
        BookshelfService bookshelfService= new(mockRepo.Object);

        List<Bookshelf> bList = [
            new Bookshelf {name = "Coffee", bookshelfId = 1},
            new Bookshelf {name = "Tea", bookshelfId = 2},
            new Bookshelf {name = "Pepsi", bookshelfId = 3}
        ];

            newBookshelfDTO newbookshelfDTO1 = new newBookshelfDTO{
                name = "Sprite"
            };

        Bookshelf newBookshelf = new(){ name = newbookshelfDTO1.name, bookshelfId = 4};



        mockRepo.Setup(repo => repo.AddBookshelf(It.IsAny<Bookshelf>()))
        .Callback(() => bList.Add(newBookshelf));

        //Act
         bookshelfService.AddBookshelf(newbookshelfDTO1);

        //Assert
        
        Assert.Contains(bList, b => b.name!.Equals("Sprite"));
        mockRepo.Verify(r => r.AddBookshelf(It.IsAny<Bookshelf>()), Times.Exactly(1));
    }
}

