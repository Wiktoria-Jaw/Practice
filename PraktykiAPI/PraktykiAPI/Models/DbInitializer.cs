namespace PraktykiAPI.Models
{
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
                Name = "Test",
                Surname = "Employee",
                Email = "test@gmail.com",
                Address = "Test Street",
                Phone_Number = "1234567890",
            };

            context.Employees.Add(employee);
            context.SaveChanges();
        }
    }
}
