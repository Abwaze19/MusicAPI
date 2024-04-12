using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public SongsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var songs = await _context.Songs.ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public IActionResult SongsFeatured()
        {
            var songs = _context.Songs.Where(x => x.IsFeatured).ToList();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public IActionResult NewSongs()
        {
            var songs = _context.Songs.OrderBy(x => x.UploadedDate).Take(3).ToList();
            return Ok(songs);
        }

       // GET api/<SongsController>/5
       [HttpGet("[action]/{query}")]
       public IActionResult SearchSong(string query)
{
         if (query == null)
         {
            return BadRequest("Query cannot be null");
         }

    var songs = _context.Songs.Where(x => x.Title.StartsWith(query)).ToList();
    return Ok(songs);
}


        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            song.UploadedDate = DateTime.Now;
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
