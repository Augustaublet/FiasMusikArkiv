using FiasMusikArkiv.Server.Data;
using FiasMusikArkiv.Server.Data.DTOs;
using FiasMusikArkiv.Server.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace FiasMusikArkiv.Server.Services;
public interface ISongService
{
    Task<IEnumerable<SongDto>> GetAllSongsAsync();
    Task<Song> GetSongByIdAsync(int id);
    Task<Song> AddSongAsync(Song song);
    Task UpdateSongAsync(Song song);
    Task DeleteSongAsync(int id);
}

public class SongService : ISongService
{
    private readonly FiasMusikArkivDbContext _dbContext;
    private readonly IMapper _mapper;

    public SongService(
        FiasMusikArkivDbContext context, IMapper mapper
        )
    {
        _dbContext = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SongDto>> GetAllSongsAsync()
    {
        return (await _dbContext.Songs
            .ToListAsync())
            .Select(song => _mapper.Map<SongDto>(song))
            .ToList();
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