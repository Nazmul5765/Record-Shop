using RecordShop.Models;
using RecordShop.Repositories;
namespace RecordShop.Services
{
    public interface IAlbumService
    {
        IEnumerable<Album> GetAllAlbums();
        Album GetAlbumById(int id);
    }
}
