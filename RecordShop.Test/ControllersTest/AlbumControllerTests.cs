using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShop.Controllers;
using RecordShop.Models;
using RecordShop.Services;
using Shouldly;
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
}





