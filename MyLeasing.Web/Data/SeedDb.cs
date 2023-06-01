using System;
using System.Linq;
using System.Threading.Tasks;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Owners.Any())
            {
                AddOwner("Luis", "Patricio");
                AddOwner("Catarina", "Esperança");
                AddOwner("Ruben", "Mateus");
                AddOwner("Filipe", "Baptista");
                AddOwner("Claudio", "Carvalho");
                AddOwner("João", "Pereira");
                AddOwner("Francisco", "Mendes");
                AddOwner("Iris", "Relvas");
                AddOwner("Evandro", "Costa");
                AddOwner("Gonçalo", "Antunes");

                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string fName, string lName)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(1000000, 9999999),
                FirstName = fName,
                LastName = lName
            });
        }
    }
}
