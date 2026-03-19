using Microsoft.EntityFrameworkCore;
using PraktykiAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Break> Breaks { get; set; }
    public DbSet<DayOff> DaysOff { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkDay> WorkSchedule { get; set; }
    public DbSet<WorkSettings> WorkSettings { get; set; }
}