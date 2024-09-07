using anime.Data;
using anime.Models;
using anime.Repositories;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IAnimeRepository, AnimeRepository>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoService>();


// builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
// {
//     var settings = serviceProvider.GetRequiredService<IOptions<DataContext>>().Value;
//     return new MongoClient(settings.ConnectionString);
// });

// builder.Services.AddScoped(serviceProvider =>
// {
//     var settings = serviceProvider.GetRequiredService<IOptions<DataContext>>().Value;
//     var client = serviceProvider.GetRequiredService<IMongoClient>();
//     return client.GetDatabase(settings.DatabaseName);
// });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();
app.Run();