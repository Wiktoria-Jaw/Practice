using Microsoft.EntityFrameworkCore;
using PraktykiAPI.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    
    public DbSet<Break> Break_Timetable { get; set; }
    public DbSet<Day_Off> Days_Off { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkDay> Work_Timetable { get; set; }
}