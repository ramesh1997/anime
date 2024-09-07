
using anime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace anime.Data;

public class MongoService : DbContext
{

    private IMongoCollection<Anime> _animesCollection;
    
    public MongoService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _animesCollection = database.GetCollection<Anime>(mongoDbSettings.Value.CollectionName);
    }

    public async Task<List<Anime>> GetAllAnimes()
    {
        return await _animesCollection.Find(a => true).ToListAsync();
    }

    public async Task<Anime?> GetAnime(string id)
    {
        var filter = await  _animesCollection.FindAsync(anime => anime.Id == id).Result.FirstOrDefaultAsync();
        if(filter is null)
        {
            return null;
        }
        return filter;
    }
    public async Task<Anime?> CreateAsyncAnimes(Anime anime)
    {
        var collection = await _animesCollection.Find(_ => true).ToListAsync();
        var isAlreadyExists = collection.Any(a => a.Id == anime.Id);
        if (isAlreadyExists)
        {
            return null;
        }
        await _animesCollection.InsertOneAsync(anime);
        return anime;
    }

    public async Task<Anime?> UpdateAsyncAnime(string id, Anime anime)
    {
        var collection = await _animesCollection.Find(_ => true).ToListAsync();
        var isAlreadyExists = collection.Any(a => a.Id == anime.Id);
        if (isAlreadyExists)
        {
            await _animesCollection.ReplaceOneAsync(val => val.Id == id, anime);
        }

        return null;
    }

    public async Task<IActionResult> DeleteAnime(string id)
    {
        var res =  await _animesCollection.DeleteOneAsync(anime => anime.Id == id);
        if (res.DeletedCount == 0)
        {
            return new NotFoundResult();
        }
        return new OkResult();
    }
}