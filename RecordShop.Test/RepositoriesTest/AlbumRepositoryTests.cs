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


}

