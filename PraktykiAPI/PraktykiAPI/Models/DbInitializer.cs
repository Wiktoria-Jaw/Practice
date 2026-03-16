namespace PraktykiAPI.Models;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Employees.Any())
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
        context.SaveChanges();
    }
}
