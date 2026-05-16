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

    [Test]
    public void UpdateAlbum_ReturnsUpdatedAlbum_WhenAlbumExists()
    {
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

            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 9.99m,
                StockQuantity = 15
            };

            int id = 1;
            var result = repo.UpdateAlbum(id, updatedAlbum);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }
    }

    [Test]
    public void UpdateAlbum_UpdatesAlbumDetails_WhenAlbumExists()
    {
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

            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 10.00m,
                StockQuantity = 15
            };

            int id = 1;
            var result = repo.UpdateAlbum(id, updatedAlbum);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
            result.Title.ShouldBe("Thriller1");
            result.Artist.ShouldBe("Michael Jackson");
            result.Genre.ShouldBe("Pop");
            result.ReleaseYear.ShouldBe(1982);
            result.Price.ShouldBe(10.00m);
            result.StockQuantity.ShouldBe(15);
        }
    }

    [Test]
    public void UpdateAlbum_ReturnsNull_WhenAlbumDoesNotExist()
    {
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

            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 10.00m,
                StockQuantity = 15
            };

            int id = 2;
            var result = repo.UpdateAlbum(id, updatedAlbum);

            result.ShouldBeNull();
        }
    }

    [Test]
    public void UpdateAlbum_DoesNotChangeAlbumCount_WhenAlbumIsUpdated()
    {
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

            var updatedAlbum = new Album
            {
                Title = "Thriller1",
                Artist = "Michael Jackson",
                Genre = "Pop",
                ReleaseYear = 1982,
                Price = 10.00m,
                StockQuantity = 15
            };

            int id = 1;
            var result = repo.UpdateAlbum(id, updatedAlbum);

            result.ShouldNotBeNull();
            context.Albums.Count().ShouldBe(1);
        }
    }

    [Test]
    public void DeleteAlbum_ReturnsDeletedAlbum_WhenAlbumExists()
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
        var result = repo.DeleteAlbum(2);

        result.Id.ShouldBe(2);
    }

    [Test]
    public void DeleteAlbum_RemovesAlbumFromDatabase_WhenAlbumExists()
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
        var result = repo.DeleteAlbum(2);

        context.Albums.Count().ShouldBe(1);
    }

    [Test]
    public void DeleteAlbum_ReturnsNull_WhenAlbumDoesNotExist()
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
        var result = repo.DeleteAlbum(3);

        result.ShouldBeNull();
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsAlbum_WhenAlbumNameExists()
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
                Title = "21", 
                Artist = "Adele", 
                Genre = "Soul", 
                ReleaseYear = 2011, 
                Price = 10.99m, 
                StockQuantity = 7 
            }
        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

        var result = repo.GetAlbumByAlbumName("21");

        result.ShouldNotBeNull();
        result.Title.ShouldBe("21");
    }

    [Test]
    public void GetAlbumByAlbumName_ReturnsNull_WhenAlbumNameDoesNotExist()
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

        var result = repo.GetAlbumByAlbumName("Unknown Album");

        result.ShouldBeNull();
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsMatchingAlbums_WhenArtistExists()
    {
        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RecordShopDbContext(options);

        context.Albums.AddRange(
            new Album 
            { 
                Id = 1, 
                Title = "21", 
                Artist = "Adele", 
                Genre = "Soul", 
                ReleaseYear = 2011, 
                Price = 10.99m, 
                StockQuantity = 7 
            },

            new Album 
            { 
                Id = 2, 
                Title = "25", 
                Artist = "Adele", 
                Genre = "Soul", 
                ReleaseYear = 2015, 
                Price = 11.99m, 
                StockQuantity = 5 
            },

            new Album 
            { 
                Id = 3, 
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

        var result = repo.GetAlbumsByArtist("Adele");

        result.Count().ShouldBe(2);
        result.All(a => a.Artist == "Adele").ShouldBeTrue();
    }

    [Test]
    public void GetAlbumsByArtist_ReturnsEmptyList_WhenArtistDoesNotExist()
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

        var result = repo.GetAlbumsByArtist("Adele");

        result.ShouldBeEmpty();
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsMatchingAlbums_WhenGenreExists()
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
                Title = "Bad", 
                Artist = "Michael Jackson", 
                Genre = "Pop", 
                ReleaseYear = 1987, 
                Price = 8.99m, 
                StockQuantity = 4 
            },

            new Album 
            { 
                Id = 3, 
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

        var result = repo.GetAlbumsByGenre("Pop");

        result.Count().ShouldBe(2);
        result.All(a => a.Genre == "Pop").ShouldBeTrue();
    }

    [Test]
    public void GetAlbumsByGenre_ReturnsEmptyList_WhenGenreDoesNotExist()
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

        var result = repo.GetAlbumsByGenre("Jazz");

        result.ShouldBeEmpty();
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsMatchingAlbums_WhenReleaseYearExists()
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
                Title = "Album Two", 
                Artist = "Artist Two", 
                Genre = "Pop", 
                ReleaseYear = 1982, 
                Price = 7.99m, 
                StockQuantity = 3 
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
        );

        context.SaveChanges();

        var repo = new AlbumRepository(context);

        var result = repo.GetAlbumsByReleaseYear(1982);

        result.Count().ShouldBe(2);
        result.All(a => a.ReleaseYear == 1982).ShouldBeTrue();
    }

    [Test]
    public void GetAlbumsByReleaseYear_ReturnsEmptyList_WhenReleaseYearDoesNotExist()
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

        var result = repo.GetAlbumsByReleaseYear(2020);

        result.ShouldBeEmpty();
    }
}

