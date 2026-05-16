using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using RecordShop.Controllers;
using RecordShop.Models;
using RecordShop.Services;
using Shouldly;
using System.Runtime.CompilerServices;
namespace RecordShop.Test;

public class AlbumControllerTests
{
    private Mock<IAlbumService> _albumServiceMock;
    private AlbumsController _albumController;

    [SetUp]
    public void Setup()
    {
        _albumServiceMock = new Mock<IAlbumService>();
        _albumController = new AlbumsController(_albumServiceMock.Object);
    }

    [Test]
    public void GetAllAlbums_ReturnsAllAlbums_WhenAlbumsExists()
    {
        List<Album> testAlbums = new List<Album>
        {
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            },

            new Album
            {
                Id = 2,
                Title = "Back in Black",
                Artist = "AC/DC",
                Genre = "Rock",
                ReleaseYear = 1980,
                Price = 8.99m,
                StockQuantity = 5
            },

            new Album
            {
                Id = 3,
                Title = "21",
                Artist = "Adele",
                Genre = "Soul",
                ReleaseYear = 2011,
                Price = 10.99m,
                StockQuantity = 7
            }
        };

        _albumServiceMock.Setup(service => service.GetAllAlbums()).Returns(testAlbums);

        IActionResult result = _albumController.GetAllAlbums();

        Assert.That(result, Is.TypeOf<OkObjectResult>());


    }

    [Test]
    public void GetAllAlbums_ReturnsNotFound_WhenNoAlbumsExists()
    {
        List<Album> emptyAlbums = new List<Album>();


        _albumServiceMock.Setup(service => service.GetAllAlbums()).Returns(emptyAlbums);

        IActionResult result = _albumController.GetAllAlbums();

        Assert.That(result, Is.TypeOf<NotFoundResult>());

    }

    [Test]
    public void GetAlbumById_ReturnsAlbumWithInputtedId_WhenAlbumIdExists()
    {
        List<Album> testAlbums = new List<Album>
        {
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            },

            new Album
            {
                Id = 2,
                Title = "Back in Black",
                Artist = "AC/DC",
                Genre = "Rock",
                ReleaseYear = 1980,
                Price = 8.99m,
                StockQuantity = 5
            },

        };

        _albumServiceMock.Setup(service => service.GetAlbumById(2)).Returns(testAlbums[1]);

        IActionResult result = _albumController.GetAlbumById(2);

        _albumServiceMock.Verify(service => service.GetAlbumById(2), Times.Once());

        Assert.That(result, Is.TypeOf<OkObjectResult>());
    }

    [Test]
    public void GetAlbumById_ReturnsNotFound_WhenAlbumIdDoesNotExist()
    {
        _albumServiceMock.Setup(service => service.GetAlbumById(3)).Returns((Album)null);

        IActionResult result = _albumController.GetAlbumById(3);

        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void GetAlbumById_ReturnsBadRequestWithNegativeId_WhenAlbumIdIsNegative()
    {

        IActionResult result = _albumController.GetAlbumById(-1);

        Assert.That(result, Is.TypeOf<BadRequestResult>());

        _albumServiceMock.Verify(service => service.GetAlbumById(It.IsAny<int>()), Times.Never());

    }

    [Test]
    public void AddAlbum_ReturnsCreated_WhenAlbumIsValid()
    {

        List<Album> testAlbums = new List<Album>
        {
        new Album
        {
            Id = 1,
            Title = "Thriller",
            Artist = "Michael Jackson",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 10
        },

        new Album
        {
            Id = 2,
            Title = "Back in Black",
            Artist = "AC/DC",
            Genre = "Rock",
            ReleaseYear = 1980,
            Price = 8.99m,
            StockQuantity = 5
        }

        };

        var newAlbum = new Album
        {
            Id = 3,
            Title = "21",
            Artist = "Adele",
            Genre = "Soul",
            ReleaseYear = 2011,
            Price = 10.99m,
            StockQuantity = 7
        };

        testAlbums.Add(newAlbum);

        _albumServiceMock.Setup(repo => repo.AddAlbum(newAlbum)).Returns(testAlbums[2]);

        IActionResult result = _albumController.AddAlbum(newAlbum);

        _albumServiceMock.Verify(repo => repo.AddAlbum(newAlbum), Times.Once());

        Assert.That(result, Is.TypeOf<CreatedResult>());

    }

    [Test]
    public void AddAlbum_ReturnsCreatedAlbum_WhenAlbumIsValid()
    {

        List<Album> testAlbums = new List<Album>();


        var newAlbum = new Album
        {
            Id = 1,
            Title = "21",
            Artist = "Adele",
            Genre = "Soul",
            ReleaseYear = 2011,
            Price = 10.99m,
            StockQuantity = 7
        };

        testAlbums.Add(newAlbum);

        _albumServiceMock.Setup(repo => repo.AddAlbum(newAlbum)).Returns(testAlbums[0]);

        var result = _albumController.AddAlbum(newAlbum);

        _albumServiceMock.Verify(repo => repo.AddAlbum(newAlbum), Times.Once());

        var createdResult = result as CreatedResult;

        var album = createdResult.Value as Album;

        newAlbum.Id.ShouldBe(1);
        newAlbum.Title.ShouldBe("21");
        newAlbum.Artist.ShouldBe("Adele");
        newAlbum.Genre.ShouldBe("Soul");
    }

    [Test]
    public void AddAlbum_ReturnsBadRequest_WhenAlbumIsNull()
    {
        Album testAlbum = null;

        IActionResult result = _albumController.AddAlbum(testAlbum);

        Assert.That(result, Is.TypeOf<BadRequestResult>());

    }
    [Test]
    public void AddAlbum_ReturnsBadRequest_WhenTitleIsMissing()
    {

        var newAlbum = new Album
        {
            Id = 1,
            Title = "",
            Artist = "Adele",
            Genre = "Soul",
            ReleaseYear = 2011,
            Price = 10.99m,
            StockQuantity = 7
        };

        IActionResult result = _albumController.AddAlbum(newAlbum);

        Assert.That(result, Is.TypeOf<BadRequestResult>());

        _albumServiceMock.Verify(repo => repo.AddAlbum(newAlbum), Times.Never());

    }

    [Test]
    public void AddAlbum_ReturnsBadRequest_WhenArtistIsMissing()
    {

        var newAlbum = new Album
        {
            Id = 1,
            Title = "21",
            Artist = "",
            Genre = "Soul",
            ReleaseYear = 2011,
            Price = 10.99m,
            StockQuantity = 7
        };

        IActionResult result = _albumController.AddAlbum(newAlbum);

        Assert.That(result, Is.TypeOf<BadRequestResult>());

        _albumServiceMock.Verify(repo => repo.AddAlbum(newAlbum), Times.Never());

    }

    [Test]
    public void AddAlbum_ReturnsBadRequest_WhenGenreIsMissing()
    {

        var newAlbum = new Album
        {
            Id = 1,
            Title = "21",
            Artist = "Adele",
            Genre = "",
            ReleaseYear = 2011,
            Price = 10.99m,
            StockQuantity = 7
        };

        IActionResult result = _albumController.AddAlbum(newAlbum);

        Assert.That(result, Is.TypeOf<BadRequestResult>());

        _albumServiceMock.Verify(repo => repo.AddAlbum(newAlbum), Times.Never());

    }

    [Test]
    public void UpdateAlbum_ReturnsOk_WhenAlbumIsUpdated()
    {
        List<Album> testAlbums = new List<Album>
        {
        new Album
        {
            Id = 1,
            Title = "Thriller",
            Artist = "Michael Jackson",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 10
        },

        new Album
        {
            Id = 2,
            Title = "Back in Black",
            Artist = "AC/DC",
            Genre = "Rock",
            ReleaseYear = 1980,
            Price = 8.99m,
            StockQuantity = 5
        }

        };

        var updatedAlbum = new Album
        {
            Title = "Thriller1",
            Artist = "Michael Jackson",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 15
        };

        _albumServiceMock.Setup(repo => repo.UpdateAlbum(1, updatedAlbum)).Returns(testAlbums[0]);

        IActionResult result = _albumController.UpdateAlbum(1, updatedAlbum);


        result.ShouldNotBeNull();
        result.ShouldBeOfType<OkObjectResult>();
    }

    [Test]
    public void UpdateAlbum_ReturnsUpdatedAlbum_WhenAlbumIsUpdated()
    {


         List<Album> testAlbums = new List<Album>
            {
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            },

            new Album
            {
                Id = 2,
                Title = "Back in Black",
                Artist = "AC/DC",
                Genre = "Rock",
                ReleaseYear = 1980,
                Price = 8.99m,
                StockQuantity = 5
            }

            };

            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 15
            };

            _albumServiceMock.Setup(repo => repo.UpdateAlbum(1, updatedAlbum)).Returns(updatedAlbum);

            var result = _albumController.UpdateAlbum(1, updatedAlbum);

            var albumReturnOk = result as OkObjectResult;

            var album = albumReturnOk.Value as Album;


            album.Title.ShouldBe("Thriller1");
            album.Artist.ShouldBe("Michael Jackson");
            album.Genre.ShouldBe("Pop");
            album.ReleaseYear.ShouldBe(1982);
            album.Price.ShouldBe(9.99m);
            album.StockQuantity.ShouldBe(15);
        
    }

    [Test]
    public void UpdateAlbum_ReturnsBadRequest_WhenIdIsZero()
    {
            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 15
            };

            var result = _albumController.UpdateAlbum(0, updatedAlbum);

            result.ShouldBeOfType<BadRequestResult>();
        
    }

    [Test]
    public void UpdateAlbum_ReturnsBadRequest_WhenIdIsNegative()
    {
        var updatedAlbum = new Album
        {
            Title = "Thriller1",
            Artist = "Michael Jackson",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 15
        };

        var result = _albumController.UpdateAlbum(-2, updatedAlbum);

        result.ShouldBeOfType<BadRequestResult>();

    }

    [Test]
    public void UpdateAlbum_ReturnsBadRequest_WhenAlbumIsNull()
    {

        var result = _albumController.UpdateAlbum(1, (Album)null);

        result.ShouldBeOfType<BadRequestResult>();

    }

    [Test]
    public void UpdateAlbum_ReturnsBadRequest_WhenTitleIsMissing()
    {
            var updatedAlbum = new Album
            {
                
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 15
            };

            var result = _albumController.UpdateAlbum(1, updatedAlbum);

            result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void UpdateAlbum_ReturnsBadRequest_WhenArtistIsMissing()
    {
        var updatedAlbum = new Album
        {

            Title = "Thiller1",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 15
        };

        var result = _albumController.UpdateAlbum(1, updatedAlbum);

        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void UpdateAlbum_ReturnsBadRequest_WhenGenreIsMissing()
    {
        var updatedAlbum = new Album
        {

            Title = "Thiller1",
            Artist = "Michael Jackson",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 15
        };

        var result = _albumController.UpdateAlbum(1, updatedAlbum);

        result.ShouldBeOfType<BadRequestResult>();
    }


    [Test]
    public void UpdateAlbum_ReturnsNotFound_WhenAlbumDoesNotExist()
    {
        var updatedAlbum = new Album
        {

            Title = "Thiller1",
            Artist = "Michael Jackson",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 15
        };

        _albumServiceMock.Setup(service => service.UpdateAlbum(999, updatedAlbum)).Returns((Album)null);

        var result = _albumController.UpdateAlbum(999, updatedAlbum);

        result.ShouldBeOfType<NotFoundResult>();
    }

    [Test]
    public void DeleteAlbum_ReturnsNoContent_WhenAlbumIsDeleted()
    {
        List<Album> testAlbums = new List<Album>
            {
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            },

            new Album
            {
                Id = 2,
                Title = "Back in Black",
                Artist = "AC/DC",
                Genre = "Rock",
                ReleaseYear = 1980,
                Price = 8.99m,
                StockQuantity = 5
            } };

        _albumServiceMock.Setup(service => service.DeleteAlbum(1)).Returns(testAlbums[0]);

        var result = _albumController.DeleteAlbum(1);
        result.ShouldBeOfType<NoContentResult>();
    }

    [Test]
    public void DeleteAlbum_ReturnsBadRequest_WhenIdIsZero()
    {
        var result = _albumController.DeleteAlbum(0);
        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void DeleteAlbum_ReturnsBadRequest_WhenIdIsNegative()
    {
        var result = _albumController.DeleteAlbum(-5);
        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void DeleteAlbum_ReturnsNotFound_WhenAlbumDoesNotExist()
    {

        _albumServiceMock.Setup(service => service.DeleteAlbum(999)).Returns((Album)null);
        var result = _albumController.DeleteAlbum(999);
        result.ShouldBeOfType<NotFoundResult>();
    }

    [Test]
    public void DeleteAlbum_DoesNotCallService_WhenIdIsInvalid()
    {


        var result = _albumController.DeleteAlbum(-1);

        _albumServiceMock.Verify(service => service.DeleteAlbum(It.IsAny<int>()), Times.Never());
        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsOk_WhenAlbumExists()
    {
        var album = new Album
        {
            Id = 1,
            Title = "Thriller",
            Artist = "Michael Jackson",
            Genre = "Pop",
            ReleaseYear = 1982,
            Price = 9.99m,
            StockQuantity = 10
        };

        _albumServiceMock
            .Setup(service => service.GetAlbumByAlbumName("Thriller"))
            .Returns(album);

        var result = _albumController.GetAlbumByAlbumName("Thriller");

        result.ShouldBeOfType<OkObjectResult>();
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsAlbum_WhenAlbumExists()
    {
        var album = new Album
        {
            Id = 1,
            Title = "Thriller",
            Artist = "Michael Jackson"
        };

        _albumServiceMock
            .Setup(service => service.GetAlbumByAlbumName("Thriller"))
            .Returns(album);

        var result = _albumController.GetAlbumByAlbumName("Thriller");

        var okResult = result as OkObjectResult;
        var returnedAlbum = okResult.Value as Album;

        returnedAlbum.ShouldNotBeNull();
        returnedAlbum.Title.ShouldBe("Thriller");
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsBadRequest_WhenTitleIsEmpty()
    {
        var result = _albumController.GetAlbumByAlbumName("");

        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsNotFound_WhenAlbumDoesNotExist()
    {
        _albumServiceMock
            .Setup(service => service.GetAlbumByAlbumName("Unknown"))
            .Returns((Album)null);

        var result = _albumController.GetAlbumByAlbumName("Unknown");

        result.ShouldBeOfType<NotFoundResult>();
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsOk_WhenAlbumsExist()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "21", Artist = "Adele" },
        new Album { Id = 2, Title = "25", Artist = "Adele" }
    };

        _albumServiceMock
            .Setup(service => service.GetAlbumsByArtist("Adele"))
            .Returns(albums);

        var result = _albumController.GetAlbumsByArtist("Adele");

        result.ShouldBeOfType<OkObjectResult>();
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsAlbums_WhenAlbumsExist()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "21", Artist = "Adele" },
        new Album { Id = 2, Title = "25", Artist = "Adele" }
    };

        _albumServiceMock
            .Setup(service => service.GetAlbumsByArtist("Adele"))
            .Returns(albums);

        var result = _albumController.GetAlbumsByArtist("Adele");

        var okResult = result as OkObjectResult;
        var returnedAlbums = okResult.Value as IEnumerable<Album>;

        returnedAlbums.Count().ShouldBe(2);
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsBadRequest_WhenArtistNameIsEmpty()
    {
        var result = _albumController.GetAlbumsByArtist("");

        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsNotFound_WhenArtistDoesNotExist()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumServiceMock
            .Setup(service => service.GetAlbumsByArtist("Unknown"))
            .Returns(emptyAlbums);

        var result = _albumController.GetAlbumsByArtist("Unknown");

        result.ShouldBeOfType<NotFoundResult>();
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsOk_WhenAlbumsExist()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "Thriller", Genre = "Pop" },
        new Album { Id = 2, Title = "Bad", Genre = "Pop" }
    };

        _albumServiceMock
            .Setup(service => service.GetAlbumsByGenre("Pop"))
            .Returns(albums);

        var result = _albumController.GetAlbumsByGenre("Pop");

        result.ShouldBeOfType<OkObjectResult>();
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsAlbums_WhenAlbumsExist()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "Thriller", Genre = "Pop" },
        new Album { Id = 2, Title = "Bad", Genre = "Pop" }
    };

        _albumServiceMock
            .Setup(service => service.GetAlbumsByGenre("Pop"))
            .Returns(albums);

        var result = _albumController.GetAlbumsByGenre("Pop");

        var okResult = result as OkObjectResult;
        var returnedAlbums = okResult.Value as IEnumerable<Album>;

        returnedAlbums.Count().ShouldBe(2);
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsBadRequest_WhenGenreIsEmpty()
    {
        var result = _albumController.GetAlbumsByGenre("");

        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsNotFound_WhenGenreDoesNotExist()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumServiceMock
            .Setup(service => service.GetAlbumsByGenre("Jazz"))
            .Returns(emptyAlbums);

        var result = _albumController.GetAlbumsByGenre("Jazz");

        result.ShouldBeOfType<NotFoundResult>();
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsOk_WhenAlbumsExist()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "Thriller", ReleaseYear = 1982 },
        new Album { Id = 2, Title = "Album Two", ReleaseYear = 1982 }
    };

        _albumServiceMock
            .Setup(service => service.GetAlbumsByReleaseYear(1982))
            .Returns(albums);

        var result = _albumController.GetAlbumsByReleaseYear(1982);

        result.ShouldBeOfType<OkObjectResult>();
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsAlbums_WhenAlbumsExist()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "Thriller", ReleaseYear = 1982 },
        new Album { Id = 2, Title = "Album Two", ReleaseYear = 1982 }
    };

        _albumServiceMock
            .Setup(service => service.GetAlbumsByReleaseYear(1982))
            .Returns(albums);

        var result = _albumController.GetAlbumsByReleaseYear(1982);

        var okResult = result as OkObjectResult;
        var returnedAlbums = okResult.Value as IEnumerable<Album>;

        returnedAlbums.Count().ShouldBe(2);
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsBadRequest_WhenReleaseYearIsInvalid()
    {
        var result = _albumController.GetAlbumsByReleaseYear(0);

        result.ShouldBeOfType<BadRequestResult>();
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsNotFound_WhenReleaseYearDoesNotExist()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumServiceMock
            .Setup(service => service.GetAlbumsByReleaseYear(2020))
            .Returns(emptyAlbums);

        var result = _albumController.GetAlbumsByReleaseYear(2020);

        result.ShouldBeOfType<NotFoundResult>();
    }
}

