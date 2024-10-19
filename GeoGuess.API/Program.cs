using GeoGuess.Core.Repository;
using GeoGuess.Core.UnityOfWork;
using GeoGuess.DataAccess.Context;
using GeoGuess.Model.Entities;
using GeoGuess.Service.Interfaces;
using GeoGuess.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBaseRepository<Panorama>, BaseRepository<Panorama>>();
builder.Services.AddScoped<IBaseRepository<PlayerPanorama>, BaseRepository<PlayerPanorama>>();
builder.Services.AddScoped<IBaseRepository<PseudoPlayer>, BaseRepository<PseudoPlayer>>();


builder.Services.AddHttpClient<IStreetViewService, StreetViewService>();
builder.Services.AddHttpClient<IDistanceService, DistanceService>();

//builder.Services.AddScoped<IBaseRepository, BaseRepository>();  
builder.Services.AddScoped<IDateTimeHandlerService, DateTimeHandlerService>();  
builder.Services.AddScoped<IStreetViewService, StreetViewService>();  
builder.Services.AddScoped<IDistanceService, DistanceService>();     
builder.Services.AddScoped<IPanoramaService, PanoramaService>();      
builder.Services.AddScoped<IGameService, GameService>();      
builder.Services.AddScoped<IPseudoPlayerService, PseudoPlayerService>();      
builder.Services.AddScoped<IRandomService, RandomService>();      

// CORS Settings
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Content-Length", "Content-Type"); // Expose headers

    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GeoGuessContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("GeoGuessDB_SQL_MS")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
