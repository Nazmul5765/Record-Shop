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
        _albumController = new AlbumsController( _albumServiceMock.Object);
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
}
