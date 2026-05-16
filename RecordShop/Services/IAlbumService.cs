using RecordShop.Models;
using RecordShop.Repositories;
namespace RecordShop.Services
{
    public interface IAlbumService
    {
        IEnumerable<Album> GetAllAlbums();
        Album GetAlbumById(int id);
        Album AddAlbum(Album album);
        Album UpdateAlbum(int id, Album updateAlbum);
        Album DeleteAlbum(int id);
        Album GetAlbumByAlbumName(string albumName);
        IEnumerable<Album> GetAlbumsByArtist(string artistName);
        IEnumerable<Album> GetAlbumsByGenre(string genre);
        IEnumerable<Album> GetAlbumsByReleaseYear(int releaseYear);
    }
}
