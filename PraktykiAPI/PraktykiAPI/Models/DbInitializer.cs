using Microsoft.EntityFrameworkCore;

namespace PraktykiAPI.Models;

public class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        if (await context.Employees.AnyAsync())
        {
            return;
        }

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

        context.Employees.AddRange(employee, employee2);
        await context.SaveChangesAsync();

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

        context.DaysOff.AddRange(dayOff, dayOff2, dayOff3);
        await context.SaveChangesAsync();
    }
}
