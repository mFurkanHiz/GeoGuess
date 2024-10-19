using GeoGuess.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GeoGuess.DataAccess.Context;

public class GeoGuessContext : DbContext
{
    public GeoGuessContext(DbContextOptions<GeoGuessContext> op) : base(op)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<OrderDetails>()
        //    .HasKey(od => new { od.OrderId, od.FoodId });
        //// bu ikisi kompozit key olacak 
    }
    public DbSet<Panorama> Panoramas { get; set; }
    public DbSet<PseudoPlayer> PseudoPlayers { get; set; }
    public DbSet<PlayerPanorama> PlayerPanoramas { get; set; }

}

