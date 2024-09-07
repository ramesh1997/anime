using anime.Data;
using anime.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
namespace anime.Repositories;

public class AnimeRepository : IAnimeRepository
{
    private readonly MongoService _mongoService;
    
    public AnimeRepository(MongoService mongoService)
    {
        _mongoService = mongoService;
    }
    public async Task<List<Anime>> GetAllAnimesAsync()
    {
        return await _mongoService.GetAllAnimes();
    }
    public async Task<Anime?> GetAnimeByIdAsync(string id)
    {
        return await _mongoService.GetAnime(id);
    }
    public async Task<Anime?> AddAnimesAsync(Anime anime)
    {
        return await _mongoService.CreateAsyncAnimes(anime);
    }
    public async Task<Anime?> updateAnime(string id,Anime anime)
    {
        return await _mongoService.UpdateAsyncAnime(id, anime);
    }
    public async Task<IActionResult> DeleteAnime(string id)
    {
        return await _mongoService.DeleteAnime(id);
        
    }
}