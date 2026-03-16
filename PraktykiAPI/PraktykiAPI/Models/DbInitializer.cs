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
            PhoneNumber = "1234567890",
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();
    }
}
