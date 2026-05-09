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

        public Album AddAlbum(Album album)
        {
            var addAlbum = _recordShopDbContext.Albums;

            addAlbum.Add(album);

            _recordShopDbContext.SaveChanges();
            return album;
        }

        public Album UpdateAlbum(int id, Album updateAlbum)
        {
            var album = _recordShopDbContext.Albums;

            var existingAlbum = album.Find(id);

            if(existingAlbum == null)
            {
                return null;
            }

            existingAlbum.Title = updateAlbum.Title;
            existingAlbum.Artist = updateAlbum.Artist;
            existingAlbum.Genre = updateAlbum.Genre;
            existingAlbum.ReleaseYear = updateAlbum.ReleaseYear;
            existingAlbum.Price = updateAlbum.Price;
            existingAlbum.StockQuantity = updateAlbum.StockQuantity;

            _recordShopDbContext.SaveChanges();

            return existingAlbum;
        }

        public Album DeleteAlbum(int id)
        {
            var deleteAlbum = _recordShopDbContext.Albums.Find(id);
            
            if (deleteAlbum == null)
            {
                return null;
            }

            _recordShopDbContext.Albums.Remove(deleteAlbum);

            _recordShopDbContext.SaveChanges();

            return deleteAlbum;
        }
    }
}
