using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PraktykiAPI.Models;

public class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        var employee = new Employee()
        {
            FirstName = "Test",
            LastName = "Employee",
            Email = "test@gmail.com",
            PhoneNumber = "123456789",
        };

        var employee2 = new Employee()
        {
            FirstName = "Stanley",
            MiddleName = "Bucket",
            LastName = "Parable",
            Email = "stanley@gmail.com",
            PhoneNumber = "987654321",
        };

        var employee3 = new Employee()
        {
            FirstName = "The",
            LastName = "Narrator",
            Email = "narrator@gmail.com",
            PhoneNumber = "000000000",
        };

        if (!await context.Employees.AnyAsync())
        {
            context.Employees.AddRange(employee, employee2, employee3);
            await context.SaveChangesAsync();
        }

        var dayOff = new DayOff()
        {
            StartDate = new DateOnly(2026, 3, 18),
            EndDate = new DateOnly(2026, 3, 18),
            EmployeeID = employee.ID,
        };

        var dayOff2 = new DayOff()
        {
            StartDate = new DateOnly(2026, 3, 18),
            EndDate = new DateOnly(2026, 3, 22),
            EmployeeID = employee2.ID,
        };

        var dayOff3 = new DayOff()
        {
            StartDate = new DateOnly(2026, 3, 30),
            EndDate = new DateOnly(2026, 4, 3),
            AcceptStatus = "accepted",
            EmployeeID = employee.ID,
        };

        if (!await context.DaysOff.AnyAsync()) 
        {
            context.DaysOff.AddRange(dayOff, dayOff2, dayOff3);
            await context.SaveChangesAsync();
        }

        var settings = new WorkSettings()
        {
            MinWorkdayLengthInMinutes = 5,
            AutoEndWorkdayLengthInMinutes = 720,
            MinBreakBetweenWorkdaysInMinutes = 480,
            MinWorkdayLengthForBreakInMinutes = 5,
            MinBreakLengthInMinutes = 2,
        };

        if (!await context.WorkSettings.AnyAsync())
        {
            context.WorkSettings.Add(settings);
            await context.SaveChangesAsync();
        }

        var user = new User()
        {
            Login = "TEmployee",
            Permission = "employee",
            IsActive = 1,
            IsLogIn = 0,
            EmployeeID = employee.ID,
        };

        var hasher = new PasswordHasher<User>();
        user.Password = hasher.HashPassword(user, "abcd");

        var user2 = new User()
        {
            Login = "SBParable",
            Permission = "employee",
            IsActive = 1,
            IsLogIn = 0,
            EmployeeID = employee2.ID,
        };

        user2.Password = hasher.HashPassword(user2, "1234");

        var user3 = new User()
        {
            Login = "TNarrator",
            Permission = "admin",
            IsActive = 1,
            IsLogIn = 0,
            EmployeeID = employee3.ID,
        };

        user3.Password = hasher.HashPassword(user3, "admin");

        if (!await context.Users.AnyAsync())
        {
            context.Users.AddRange(user, user2, user3);
            await context.SaveChangesAsync();
        }
    }
}
