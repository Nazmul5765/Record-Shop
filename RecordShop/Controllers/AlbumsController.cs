using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Services;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var albums = _albumService.GetAllAlbums();

            if (albums == null || !albums.Any())
            {
                return NotFound("No albums were found.");
            }

            return Ok(albums);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Album id must be greater than zero");
            }

            var album = _albumService.GetAlbumById(id);

            if (album == null)
            {
                return NotFound($"Album with id {id} was not found");
            }

            return Ok(album);
        }

        [HttpPost]
        public IActionResult AddAlbum(Album album)
        {
            if (album == null)
            {
                return BadRequest("Album data is required.");
            }
            if (string.IsNullOrWhiteSpace(album.Title) || string.IsNullOrWhiteSpace(album.Artist) ||
            String.IsNullOrWhiteSpace(album.Genre))
            {
                return BadRequest("Title, Artist and Genre is required");
            }
            else
            {
                var addAlbum = _albumService.AddAlbum(album);
                return Created($"/api/Album/{album.Id}", addAlbum);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAlbum(int id, Album updateAlbum)
        {
            if (id <= 0)
            {
                return BadRequest("Album id must be greater than zero.");
            }
            if (updateAlbum == null)
            {
                return BadRequest("Updated album data is required.");
            }
            if (string.IsNullOrWhiteSpace(updateAlbum.Title) || string.IsNullOrWhiteSpace(updateAlbum.Artist) ||
            String.IsNullOrWhiteSpace(updateAlbum.Genre))
            {
                return BadRequest("Updated Album: Title, Artist and Genre is required");
            }


            var updatedAlbum = _albumService.UpdateAlbum(id, updateAlbum);

            if (updatedAlbum == null)
            {
                return NotFound($"Album with id {id} was not found.");
            }
            return Ok(updatedAlbum);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Album id must be greater than zero.");
            }


            var deletedAlbum = _albumService.DeleteAlbum(id);

            if (deletedAlbum == null)
            {
                return NotFound($"Album with id {id} was not found.");
            }
            return NoContent();

        }

        [HttpGet("title/{title}")]
        public IActionResult GetAlbumByAlbumName(string title)
        {

            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Album title is required");
            }
            var album = _albumService.GetAlbumByAlbumName(title);

            if (album == null)
            {
                return NotFound($"Album with title {title} was not found.");
            }
            return Ok(album); 

        }

        [HttpGet("artist/{artistName}")]
        public IActionResult GetAlbumsByArtist(string artistName)
        {
            if (string.IsNullOrWhiteSpace(artistName))
            {
                return BadRequest("Artist Name is required");
            }
            var album = _albumService.GetAlbumsByArtist(artistName);

            if (!album.Any())
            {
                return NotFound($"No albums found for artist {artistName}.");
            }
            return Ok(album);
        }

        [HttpGet("genre/{genre}")]
        public IActionResult GetAlbumsByGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                return BadRequest("Genre is required");
            }
            var album = _albumService.GetAlbumsByGenre(genre);

            if (!album.Any())
            {
                return NotFound($"No albums found for genre {genre}.");
            }
            return Ok(album);
        }

        [HttpGet("releaseYear/{releaseYear}")]
        public IActionResult GetAlbumsByReleaseYear(int releaseYear)
        {
            if (releaseYear <= 0)
            {
                return BadRequest("release year must be greater than zero.");
            }
            var album = _albumService.GetAlbumsByReleaseYear(releaseYear);

            if (!album.Any())
            {
                return NotFound($"No albums found for with the release year {releaseYear}.");
            }
            return Ok(album);
        }
    }
}
