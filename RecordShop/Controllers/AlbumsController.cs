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
                return NotFound();
            }

            return Ok(albums);
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var album = _albumService.GetAlbumById(id);

            if (album == null)
            {
                return NotFound();
            }
            
                return Ok(album);         
        }

        [HttpPost]
        public IActionResult AddAlbum(Album album)
        {
            if (album == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(album.Title) || string.IsNullOrWhiteSpace(album.Artist) ||
            String.IsNullOrWhiteSpace(album.Genre))
            {
                return BadRequest();
            }
            else
            {
                var addAlbum = _albumService.AddAlbum(album);
                return Created($"/api/Album/{album.Id}", addAlbum);
            }
        }
    }
}
