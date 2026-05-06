using RecordShop.Data;
using RecordShop.Models;

namespace RecordShop.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly RecordShopDbContext _recordShopDbContext;

        public AlbumRepository(RecordShopDbContext recordShopDbContext)
        {
            _recordShopDbContext = recordShopDbContext;
        }
        public IEnumerable<Album> GetAllAlbums()
        {
            return _recordShopDbContext.Albums; 
        }

        public Album GetAlbumById(int id)
        {
            var album = _recordShopDbContext.Albums;

            return album.FirstOrDefault(a => a.Id == id);
        }
    }
}
