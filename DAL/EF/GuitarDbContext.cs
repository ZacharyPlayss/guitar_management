using System.Diagnostics;
using GuitarManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GuitarManagement.DAL.EF;

public class GuitarDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<GuitarModel> GuitarModels { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    
    public GuitarDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Guitar.db");
        }

        optionsBuilder.LogTo(message =>
            Debug.WriteLine(message), LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Manufacturer>()
            .ToTable("Manufacturer");
        modelBuilder.Entity<Manufacturer>()
            .HasMany(manufacturer => manufacturer.GuitarModels)
            .WithOne(guitarModel => guitarModel.Manufacturer);
        modelBuilder.Entity<GuitarModel>()
            .HasMany(guitarModel => guitarModel.Stock)
            .WithOne(stock => stock.GuitarModel);
        modelBuilder.Entity<Store>()
            .HasMany(store => store.Stock)
            .WithOne(stock => stock.Store);
    }

    public bool CreateDatabase(bool dropDataBase)
    {
        if (dropDataBase)
        {
            Database.EnsureDeleted();
        }

        return Database.EnsureCreated();
    }
}