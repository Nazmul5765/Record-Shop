using RecordShop.Models;

namespace RecordShop.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAllAlbums();
        Album GetAlbumById(int id);
    }
}
