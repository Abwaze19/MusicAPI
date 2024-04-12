using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public AlbumsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<AlbumsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var albums = await _context.Albums.ToListAsync();
            return Ok(albums);
        }

        // GET api/<ArtistsController>/5
        [HttpGet("[action]/{id}")]
        public IActionResult AlbumDetails(int id)
        {
            var album = _context.Albums.Include(a => a.Songs).SingleOrDefault(a => a.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }

        // POST api/<AlbumsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<AlbumsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Album album)
        {
            if (id != album.Id)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<AlbumsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}
