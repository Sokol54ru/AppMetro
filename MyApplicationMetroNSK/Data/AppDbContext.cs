using Microsoft.EntityFrameworkCore;
using MyApplicationMetroNSK.Data.Models;

namespace MyApplicationMetroNSK.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {

    }
    public DbSet<TimeCard> TimeCards { get; set; }
    public DbSet<WorkedTimeCard> WorkedTimeCards { get; set; }
    public DbSet<Salary> Salary { get; set; }
    public DbSet<DataForCalculation> DataForCalculation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimeCard>()
            .HasKey(tc => new { tc.NumberTimeCard, tc.WorkType, tc.DayofWeek });
    }
}
