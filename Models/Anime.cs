using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace anime.Models;

public class Anime
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string name {get; set;} = string.Empty!;
    public string logoUrl { get; set; } = string.Empty!;
    public string title { get; set; } = string.Empty!;
    public string? description {get; set;} = string.Empty;
    public Double rating { get; set; } = 0!;
    public string genre {get; set;} = string.Empty;
    public string studio {get; set;} = string.Empty;
    public DateTime releaseDate {get; set;} = DateTime.MinValue;
}