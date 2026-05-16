using RecordShop.Models;
using RecordShop.Repositories;

namespace RecordShop.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            return _albumRepository.GetAllAlbums();
        }

        public Album GetAlbumById(int id)
        {
            return _albumRepository.GetAlbumById(id);
        }

        public Album AddAlbum(Album album)
        {
            return _albumRepository.AddAlbum(album);
        }

        public Album UpdateAlbum(int id, Album updateAlbum)
        {
            return _albumRepository.UpdateAlbum(id, updateAlbum);
        }

        public Album DeleteAlbum(int id)
        {
            return _albumRepository.DeleteAlbum(id);
        }

        public Album GetAlbumByAlbumName(string albumName)
        {
            return _albumRepository.GetAlbumByAlbumName(albumName);
        }

        public IEnumerable<Album> GetAlbumsByArtist(string artistName)
        {
            return _albumRepository.GetAlbumsByArtist(artistName);
        }

        public IEnumerable<Album> GetAlbumsByGenre(string genre)
        {
            return _albumRepository.GetAlbumsByGenre(genre);
        }

        public IEnumerable<Album> GetAlbumsByReleaseYear(int releaseYear)
        {
            return _albumRepository.GetAlbumsByReleaseYear(releaseYear);
        }

    }
}
