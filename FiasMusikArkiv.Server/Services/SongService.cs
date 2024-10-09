using FiasMusikArkiv.Server.Data;
using FiasMusikArkiv.Server.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FiasMusikArkiv.Server.Services;
public interface ISongService
{
    Task<IEnumerable<Song>> GetAllSongsAsync();
    Task<Song> GetSongByIdAsync(int id);
    Task<Song> AddSongAsync(Song song);
    Task UpdateSongAsync(Song song);
    Task DeleteSongAsync(int id);
}

public class SongService : ISongService
{
    private readonly FiasMusikArkivDbContext _dbContext;

    public SongService(
        FiasMusikArkivDbContext context
        )
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        return await _dbContext.Songs.ToListAsync();
    }
    public async Task<Song> GetSongByIdAsync(int id)
    {
        return await _dbContext.Songs.FirstOrDefaultAsync(song => song.Id == id);
    }

    public async Task<Song> AddSongAsync(Song song)
    {
        await _dbContext.Songs.AddAsync(song);
        await _dbContext.SaveChangesAsync();
        return song;
    }

    public async Task UpdateSongAsync(Song song)
    {
        _dbContext.Songs.Update(song);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSongAsync(int id)
    {
        var song = _dbContext.Songs.FirstOrDefault(song => song.Id == id);
        if (song != null)
        {
            _dbContext.Songs.Remove(song);
            await _dbContext.SaveChangesAsync();
        }
    }
}