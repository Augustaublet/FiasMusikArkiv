using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FiasMusikArkiv.Server.Services;
using FiasMusikArkiv.Server.Data.Models;
using FiasMusikArkiv.Server.Data.DTOs;

namespace FiasMusikArkiv.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songService.GetAllSongsAsync();
            if (songs == null)
            {
                return NotFound();
            }
            return Ok(songs);
        }

        [HttpGet("{id}", Name = "GetSong")]
        public async Task<IActionResult> GetSong(int id)
        {
            try
            {
                var song = await _songService.GetSongByIdAsync(id);
                if (song == null)
                {
                    return NotFound();
                }
                return Ok(song);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(Song song)
        {
            var newSong = await _songService.AddSongAsync(song);
            return CreatedAtRoute("GetSong", new { id = newSong.Id }, newSong);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest("Id in URL does not match Id in song object.");
            }

            try
            {
                await _songService.UpdateSongAsync(song);
                return NoContent();
            }      
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                await _songService.DeleteSongAsync(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
