using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Models;
using RecordShop.Repositories;
using Shouldly;


namespace RecordShop.Test;

public class AlbumRepositoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetAllAlbums_ReturnsAllAlbums_WhenAlbumsExist()
    {
        
        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.Add(
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            }

        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

        
        var result = repo.GetAllAlbums();

        
        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public void GetAllAlbums_ReturnsEmptyList_WhenNoAlbumsExist()
    {
        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase (databaseName: Guid.NewGuid().ToString()).Options;

        var context = new RecordShopDbContext(options);

        var repo = new AlbumRepository(context);

        var result = repo.GetAllAlbums();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetAlbumById_ReturnsAlbumWithCorrectId_WhenAlbumIdExist()
    {

        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.Add(
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            }

        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

        int id = 1;
        var result = repo.GetAlbumById(id);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);

    }

    [Test]
    public void GetAlbumById_ReturnsNullWithInvalidId_WhenAlbumIdDoesNotExist()
    {

        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.Add(
            new Album
            {
                Id = 1,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 10
            }

        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

        int id = 5;
        var result = repo.GetAlbumById(id);

        result.ShouldBeNull();
    }

    [Test]
    public void AddAlbum_AddsNewAlbum_WhenAlbumIsValid()
    {

        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.AddRange(
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

        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

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

        var result = repo.AddAlbum(newAlbum);


        result.Id.ShouldBe(3);
    }

    [Test]
    public void AddAlbum_ReturnsAddedAlbum_WhenAlbumIsValid()
    {

        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.AddRange(
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

        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

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

        var result = repo.AddAlbum(newAlbum);


        result.Title.ShouldBe("21");
        result.Artist.ShouldBe("Adele");
        result.Genre.ShouldBe("Soul");
        result.ReleaseYear.ShouldBe(2011);

    }

    [Test]
    public void AddAlbum_IncreasesAlbumCount_WhenAlbumIsValid()
    {

        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.AddRange(
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

        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

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

        var result = repo.AddAlbum(newAlbum);


        context.Albums.Count().ShouldBe(3);

    }

}

