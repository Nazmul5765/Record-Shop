using Microsoft.AspNetCore.Mvc;
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
    }
}
