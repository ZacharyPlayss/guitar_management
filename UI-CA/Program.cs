using System.Reflection;
using GuitarManagement.BL;
using GuitarManagement.DAL;
using GuitarManagement.DAL.EF;
using GuitarManagement.UI.CA;
using Microsoft.EntityFrameworkCore;


DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<GuitarDbContext>();
optionsBuilder.UseSqlite("Data Source=Guitar.db");
GuitarDbContext context = new GuitarDbContext(optionsBuilder.Options);
IRepository repo = new Repository(context);
IManager manager = new Manager(repo);
ConsoleUi consoleUi = new ConsoleUi(manager);

if (context.CreateDatabase(dropDataBase: true))
{
    DataSeeder.Seed(context);
}

consoleUi.Run();