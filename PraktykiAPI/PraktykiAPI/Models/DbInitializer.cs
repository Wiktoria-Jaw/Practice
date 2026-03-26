using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PraktykiAPI.Models;

public class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        var EmployeesData = new[]
        {
            new Employee { FirstName = "Test", LastName = "Employee", Email = "test@gmail.com", PhoneNumber = "123456789"},
            new Employee { FirstName = "Stanley", MiddleName = "Bucket", LastName = "Parable", Email = "stanley@gmail.com", PhoneNumber = "987654321"},
            new Employee { FirstName = "The", LastName = "Narrator", Email = "narrator@gmail.com", PhoneNumber = "000000000" }
        };

        foreach (var emp in EmployeesData)
        {
            var exist = await context.Employees.FirstOrDefaultAsync(e => e.Email == emp.Email);
            if (exist == null)
            {
                context.Employees.Add(emp);
            }
        }

        await context.SaveChangesAsync();

        var allEmpl = await context.Employees.ToListAsync();

        var dayOffData = new (DateOnly Start, DateOnly End, string EmlpEmail, string? Status)[]
        {
            (new DateOnly(2026, 3, 18), new DateOnly(2026, 3, 18), "test@gmail.com", null),
            (new DateOnly(2026, 3, 18), new DateOnly(2026, 3, 22), "stanley@gmail.com", null),
            (new DateOnly(2026, 3, 30), new DateOnly(2026, 4, 3), "test@gmail.com", "accepted")
        };

        foreach (var d in dayOffData)
        {
            var employee = allEmpl.First(e => e.Email == d.EmlpEmail);
            var exists = await context.DaysOff.AnyAsync(x => x.EmployeeID == employee.ID && x.StartDate == d.Start && x.EndDate == d.End);

            if (!exists)
            {
                context.DaysOff.Add(new DayOff
                {
                    StartDate = d.Start,
                    EndDate = d.End,
                    AcceptStatus = d.Status ?? "pending", 
                    EmployeeID = employee.ID,
                });
            }
        };

        await context.SaveChangesAsync();

        if (!await context.WorkSettings.AnyAsync())
        {
            context.WorkSettings.Add(new WorkSettings
            {
                MinWorkdayLengthInMinutes = 5,
                AutoEndWorkdayLengthInMinutes = 720,
                MinBreakBetweenWorkdaysInMinutes = 480,
                MinWorkdayLengthForBreakInMinutes = 5,
                MinBreakLengthInMinutes = 2,
            });
            await context.SaveChangesAsync();
        }

        var hasher = new PasswordHasher<User>();

        var UsersData = new[]
        {
            new {Login = "TEmployee", Password="abcd", Permission = "employee", emplEmail = "test@gmail.com"},
            new {Login = "SBParable", Password="1234", Permission = "employee", emplEmail = "stanley@gmail.com"},
            new {Login = "TNarrator", Password="admin", Permission = "admin", emplEmail = "narrator@gmail.com"}
        };

        foreach(var u in UsersData)
        {
            var exist = await context.Users.FirstOrDefaultAsync(x => x.Login == u.Login);
            if(exist == null)
            {
                var employee = allEmpl.First(e => e.Email == u.emplEmail);
                context.Users.Add(new User
                {
                    Login = u.Login,
                    Password = hasher.HashPassword(null, u.Password),
                    Permission = u.Permission,
                    IsActive = 1,
                    IsLogIn = 0,
                    EmployeeID = employee.ID,
                });
            }
        }
        await context.SaveChangesAsync();
    }
}
