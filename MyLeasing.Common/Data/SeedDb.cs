using Microsoft.AspNetCore.Identity;
using MyLeasing.Common.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Common.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private Random _random;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userManager.FindByEmailAsync("luispatricio.info@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Document = _random.Next(1000000, 9999999).ToString(),
                    FirstName = "Luis",
                    LastName = "Patricio",
                    Email = "luispatricio.info@gmail.com",
                    UserName = "MrSiul",
                    PhoneNumber = "123456789",
                };

                var result = await _userManager.CreateAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!_context.Owners.Any())
            {
                await AddOwner("Luis", "Patricio", user);
                await AddOwner("Catarina", "Esperanca", null);
                await AddOwner("Ruben", "Mateus", null);
                await AddOwner("Filipe", "Baptista", null);
                await AddOwner("Claudio", "Carvalho", null);
                await AddOwner("Joao", "Pereira", null);
                await AddOwner("Francisco", "Mendes", null);
                await AddOwner("Iris", "Relvas", null);
                await AddOwner("Evandro", "Costa", null);
                await AddOwner("Goncalo", "Antunes", null);

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddOwner(string fName, string lName, User user)
        {
            if (user == null)
            {
                user = await AddUser(fName, lName);
            }

            _context.Owners.Add(new Owner
            {
                Document = user.Document,
                FirstName = fName,
                LastName = lName,
                CellPhone = user.PhoneNumber,
                User = user
            });
        }

        private async Task<User> AddUser(string fName, string lName)
        {
            var user = new User
            {
                Document = _random.Next(1000000, 9999999).ToString(),
                FirstName = fName,
                LastName = lName,
                Email = $"{fName}{lName}@gmail.com",
                UserName = $"{fName}{lName}@gmail.com",
                PhoneNumber = _random.Next(100000000, 999999999).ToString()
            };

            var result = await _userManager.CreateAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            return user;
        }
    }
}
