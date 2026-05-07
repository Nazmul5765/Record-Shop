using Shouldly;
using Moq;
using RecordShop.Repositories;
using RecordShop.Services;
using RecordShop.Models;

namespace RecordShop.Test;

public class AlbumServiceTests
{
    private Mock<IAlbumRepository> _albumRepositoryMock;
    private IAlbumService _albumService;

    [SetUp]
    public void Setup()
    {
        _albumRepositoryMock = new Mock<IAlbumRepository>();
        _albumService = new AlbumService(_albumRepositoryMock.Object);
        
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

        _albumRepositoryMock.Setup(repo => repo.GetAllAlbums()).Returns(testAlbums);

        var result = _albumService.GetAllAlbums();

        result.ShouldBe(testAlbums);
    }

    [Test]
    public void GetAllAlbums_ReturnsEmptyList_WhenNoAlbumsExists()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumRepositoryMock.Setup(repo => repo.GetAllAlbums()).Returns(emptyAlbums);

        var result = _albumService.GetAllAlbums();

        result.ShouldBe(emptyAlbums);
    }

    [Test]
    public void GetAlbumById_ReturnsAlbumWithInputtedId_WhenAlbumDoesExistWithCorrectId()
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

        _albumRepositoryMock.Setup(repo => repo.GetAlbumById(2)).Returns(testAlbums[1]);

        var result = _albumService.GetAlbumById(2);

        _albumRepositoryMock.Verify(repo => repo.GetAlbumById(2),Times.Once());

        result.ShouldNotBeNull();
        result.Id.ShouldBe(2);
    }

    [Test]
    public void GetAlbumById_ReturnsNullWithInvalidId_WhenAlbumIdDoesNotExist()
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

        _albumRepositoryMock.Setup(repo => repo.GetAlbumById(3)).Returns((Album)null);

        var result = _albumService.GetAlbumById(3);


        result.ShouldBeNull();
    }

    [Test]
    public void AddAlbum_AddNewAlbum_WhenAlbumIsValid()
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

        _albumRepositoryMock.Setup(repo => repo.AddAlbum(newAlbum)).Returns(testAlbums[2]);

        var result = _albumService.AddAlbum(newAlbum);

        _albumRepositoryMock.Verify(repo => repo.AddAlbum(newAlbum), Times.Once());

        result.ShouldNotBeNull();
        result.Id.ShouldBe(3);
    }

    [Test]
    public void AddAlbum_PassesCorrectAlbumToRepository_WhenAlbumIsValid()
    {

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

        var result = _albumService.AddAlbum(newAlbum);

        _albumRepositoryMock.Verify(repo => repo.AddAlbum(
        It.Is<Album>(a =>
        a.Id == 3 &&
        a.Title == "21" &&
        a.Artist == "Adele" &&
        a.Genre == "Soul"
    )), Times.Once());
    }

}
