using System;
using GRES.Data.Models;
using Microsoft.EntityFrameworkCore;
using GRES.Services.Lifetime.PlatformServices;
using System.IO;

namespace GRES.Data.DB;

public class LocalDBContext : DbContext
{
    private readonly Platform  _platform;
    public DbSet<Game> Games => Set<Game>();
    public LocalDBContext(Platform platform) 
    {
        _platform = platform; 
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        string path = GetOrCreatePathLocalDB();
        optionsBuilder.UseSqlite($"DataSource={path}");
    }
    private static string GetOrCreatePathLocalDB()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string dbFolder = Path.Combine(appDataPath, "GRES");
        Directory.CreateDirectory(dbFolder);
        string dbPath = Path.Combine(dbFolder, "local.db");

        return dbPath;
    }

    
}
