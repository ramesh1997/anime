using anime.Models;
using anime.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace anime.Controllers;
[ApiController]
[Route("api/anime/")]
public class AnimeContoller : ControllerBase
{
    private readonly IAnimeRepository _animeRepository;
    // public static List<Anime> animeList = new List<Anime>
    // {
    //     new Anime {Id = 1, Title = "Naruto", Genre = "Action", ReleaseDate = new DateTime(2002, 10, 3), Studio = "Studio Pierrot", Rating = 8.5, Description = "A young ninja who seeks recognition...", LogoUrl = "http://example.com/naruto-logo.png"}
    // };
    public AnimeContoller(IAnimeRepository animeRepository)
    {
     _animeRepository = animeRepository;   
    }

    [HttpGet]
    // public ActionResult<List<Anime>> GetAnime()
    // {
    //     return animeList;
    // }
    public async Task<IActionResult> GetAnimes()
    {
        var animeList = await _animeRepository.GetAllAnimesAsync();
        return Ok(animeList);
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> GetAnime(string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }
        var anime = await _animeRepository.GetAnimeByIdAsync(id);
        if (anime is null)
        {
            return NotFound("Anime not found");
        }
        return Ok(anime);
    }
    
    [HttpPost]
    // public ActionResult<List<Anime>> GetAnime()
    // {
    //     return animeList;
    // }
    public async Task<IActionResult> AddAnime([FromBody] Anime? anime)
    {
        if (anime == null)
        {
            return BadRequest("Anime cannot be null");
        }
        
        try
        {
            var res = await _animeRepository.AddAnimesAsync(anime);
            // return CreatedAtActionResult("GetAnime", new { id = anime.Id }, anime);
            if (res is null)
            {
                return BadRequest("Anime already exists");
            }
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("id")]
    public async Task<IActionResult> UpdateAnime(string id, [FromBody] Anime? anime)
    {
        if (anime == null)
        {
            return BadRequest("Anime cannot be null");
        }

        try
        {
            var res = await _animeRepository.updateAnime(id, anime);
            if (res is null)
            {
                return NotFound("Anime not found");
            }
            return Ok("Anime updated Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAnime(string? id)
    {
        if (id == null)
        {
            return BadRequest("Anime Id cannot be null");
        }

        try
        {
            var result = await _animeRepository.DeleteAnime(id);
            // if (result.)
            // {
            //     return Ok("Anime deleted Successfully");
            // }
            if (result is NotFoundResult)
            {
             return BadRequest("Please Provide Valid Anime Id");   
            }
            return Ok(result);
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}