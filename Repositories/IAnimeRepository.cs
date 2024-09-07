using anime.Models;
using Microsoft.AspNetCore.Mvc;

namespace anime.Repositories;

public interface IAnimeRepository
{
    Task<List<Anime>> GetAllAnimesAsync();
    Task<Anime?>  AddAnimesAsync(Anime anime);
    
    Task<Anime?> GetAnimeByIdAsync(string id);
    
    Task<Anime?> updateAnime(string id, Anime anime);
    Task<IActionResult> DeleteAnime(string id);
}