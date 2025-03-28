using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApplicationMetroNSK.Data; // Замените на ваш namespace

class Program
{
    static void Main()
    {
        Console.WriteLine("Recreating Database from scratch...");

        // Загружаем конфигурацию из appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Создаем объект DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        using var context = new AppDbContext(optionsBuilder.Options);
        context.Database.EnsureDeleted();  // Удаляем БД
        context.Database.EnsureCreated();  // Создаем заново

        Console.WriteLine("Database successfully recreated!");
    }
}
