using Microsoft.EntityFrameworkCore;
using TrainSystem.Data.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var context = new TrainSystemContext();

        var employee = await context.Employees.ToListAsync<Employee>();

        foreach (var item in employee)
        {
            Console.WriteLine(item.Name);
        }
        Console.Read();
    }
}