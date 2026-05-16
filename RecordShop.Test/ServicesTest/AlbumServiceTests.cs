using Moq;
using RecordShop.Models;
using RecordShop.Repositories;
using RecordShop.Services;
using Shouldly;

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

    [Test]
    public void UpdateAlbum_ReturnsUpdatedAlbum_WhenAlbumExists()
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

            _albumRepositoryMock.Setup(repo => repo.UpdateAlbum(1, updatedAlbum)).Returns(testAlbums[0]);

            var result = _albumService.UpdateAlbum(1, updatedAlbum);


            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        
    }

    [Test]
    public void UpdateAlbum_CallsRepositoryUpdateAlbum_Once()
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

            var result = _albumService.UpdateAlbum(1, updatedAlbum);

            _albumRepositoryMock.Verify(repo => repo.UpdateAlbum(1, updatedAlbum), Times.Once());
    }

    [Test]
    public void UpdateAlbum_PassesCorrectIdAndAlbumToRepository()
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
        }};

            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 15
            };

            var result = _albumService.UpdateAlbum(1, updatedAlbum);

        _albumRepositoryMock.Verify(repo => repo.UpdateAlbum(1,
        It.Is<Album>(a =>
        a.Title == "Thriller1" &&
        a.Artist == "Michael Jackson" &&
        a.Genre == "Pop" &&
        a.ReleaseYear == 1982 &&
        a.Price == 9.99m &&
        a.StockQuantity == 15
        )), Times.Once());
    }

    [Test]
    public void UpdateAlbum_ReturnsNull_WhenAlbumDoesNotExist()
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

        _albumRepositoryMock.Setup(repo => repo.UpdateAlbum(1, updatedAlbum)).Returns((Album) null);

        var result = _albumService.UpdateAlbum(1, updatedAlbum);

        result.ShouldBeNull();
    }

    [Test]
    public void DeleteAlbum_ReturnsDeletedAlbum_WhenAlbumExists()
    {
        List<Album> newAlbum = new List<Album>
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
        }};


        _albumRepositoryMock.Setup(repo => repo.DeleteAlbum(2)).Returns(newAlbum[1]);

        var result = _albumService.DeleteAlbum(2);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(2);
    }

    [Test]
    public void DeleteAlbum_CallsRepositoryDeleteAlbum_Once()
    {
        var deletedAlbum = _albumService.DeleteAlbum(1);

        _albumRepositoryMock.Verify(repo => repo.DeleteAlbum(1), Times.Once());
    }

    [Test]
    public void DeleteAlbum_PassesCorrectIdToRepository()
    {

        var result = _albumService.DeleteAlbum(1);

        _albumRepositoryMock.Verify(repo => repo.DeleteAlbum(1), Times.Once());
    }

    [Test]
    public void DeleteAlbum_ReturnsNull_WhenAlbumDoesNotExist()
    {
        _albumRepositoryMock.Setup(repo => repo.DeleteAlbum(1)).Returns((Album?)null);

        var result = _albumService.DeleteAlbum(1);

        result.ShouldBeNull();
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsAlbum_WhenAlbumExists()
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

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumByAlbumName("Thriller"))
            .Returns(album);

        var result = _albumService.GetAlbumByAlbumName("Thriller");

        result.ShouldNotBeNull();
        result.Title.ShouldBe("Thriller");
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsNull_WhenAlbumDoesNotExist()
    {
        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumByAlbumName("Unknown"))
            .Returns((Album)null);

        var result = _albumService.GetAlbumByAlbumName("Unknown");

        result.ShouldBeNull();
    }

    [Test]
    public void GetAlbumByAlbumName_CallsRepository_Once()
    {
        _albumService.GetAlbumByAlbumName("Thriller");

        _albumRepositoryMock.Verify(
            repo => repo.GetAlbumByAlbumName("Thriller"),
            Times.Once());
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsAlbums_WhenArtistExists()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "21", Artist = "Adele" },
        new Album { Id = 2, Title = "25", Artist = "Adele" }
    };

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumsByArtist("Adele"))
            .Returns(albums);

        var result = _albumService.GetAlbumsByArtist("Adele");

        result.Count().ShouldBe(2);
        result.All(a => a.Artist == "Adele").ShouldBeTrue();
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsEmptyList_WhenArtistDoesNotExist()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumsByArtist("Unknown"))
            .Returns(emptyAlbums);

        var result = _albumService.GetAlbumsByArtist("Unknown");

        result.ShouldBeEmpty();
    }

    [Test]
    public void GetAlbumsByArtist_CallsRepository_Once()
    {
        _albumService.GetAlbumsByArtist("Adele");

        _albumRepositoryMock.Verify(
            repo => repo.GetAlbumsByArtist("Adele"),
            Times.Once());
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsAlbums_WhenGenreExists()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "Thriller", Genre = "Pop" },
        new Album { Id = 2, Title = "Bad", Genre = "Pop" }
    };

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumsByGenre("Pop"))
            .Returns(albums);

        var result = _albumService.GetAlbumsByGenre("Pop");

        result.Count().ShouldBe(2);
        result.All(a => a.Genre == "Pop").ShouldBeTrue();
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsEmptyList_WhenGenreDoesNotExist()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumsByGenre("Jazz"))
            .Returns(emptyAlbums);

        var result = _albumService.GetAlbumsByGenre("Jazz");

        result.ShouldBeEmpty();
    }

    [Test]
    public void GetAlbumsByGenre_CallsRepository_Once()
    {
        _albumService.GetAlbumsByGenre("Pop");

        _albumRepositoryMock.Verify(
            repo => repo.GetAlbumsByGenre("Pop"),
            Times.Once());
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsAlbums_WhenReleaseYearExists()
    {
        List<Album> albums = new List<Album>
    {
        new Album { Id = 1, Title = "Thriller", ReleaseYear = 1982 },
        new Album { Id = 2, Title = "Album Two", ReleaseYear = 1982 }
    };

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumsByReleaseYear(1982))
            .Returns(albums);

        var result = _albumService.GetAlbumsByReleaseYear(1982);

        result.Count().ShouldBe(2);
        result.All(a => a.ReleaseYear == 1982).ShouldBeTrue();
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsEmptyList_WhenReleaseYearDoesNotExist()
    {
        List<Album> emptyAlbums = new List<Album>();

        _albumRepositoryMock
            .Setup(repo => repo.GetAlbumsByReleaseYear(2020))
            .Returns(emptyAlbums);

        var result = _albumService.GetAlbumsByReleaseYear(2020);

        result.ShouldBeEmpty();
    }

    [Test]
    public void GetAlbumsByReleaseYear_CallsRepository_Once()
    {
        _albumService.GetAlbumsByReleaseYear(1982);

        _albumRepositoryMock.Verify(
            repo => repo.GetAlbumsByReleaseYear(1982),
            Times.Once());
    }
}
