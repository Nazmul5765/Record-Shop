using RecordShop.Models;

namespace RecordShop.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAllAlbums();
        Album GetAlbumById(int id);

        Album AddAlbum(Album album);

        Album UpdateAlbum(int id, Album updateAlbum);

        Album DeleteAlbum(int id);
    }
}
