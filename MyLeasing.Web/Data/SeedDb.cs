using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("adminSudo@mail.com");
            if (user == null)
            {
                user = new User
                {
                    Document = _random.Next(1000000, 9999999).ToString(),
                    FirstName = "Admin",
                    LastName = "Administrador",
                    Email = "adminSudo@mail.com",
                    UserName = "SudoSu@mail.com",
                    PhoneNumber = "123456789",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (_context.Users.Count() < 2)
            {
                await AddUser("Luis", "Patricio");
                await AddUser("Catarina", "Esperanca");
                await AddUser("Ruben", "Mateus");
                await AddUser("Filipe", "Baptista");
                await AddUser("Claudio", "Carvalho");
                await AddUser("Joao", "Pereira");
                await AddUser("Francisco", "Mendes");
                await AddUser("Iris", "Relvas");
                await AddUser("Evandro", "Costa");
                await AddUser("Goncalo", "Antunes");

                await _context.SaveChangesAsync();
            }

            if (!_context.Owners.Any())
            {
                await AddOwner("Luis", "Patricio");
                await AddOwner("Catarina", "Esperanca");
                await AddOwner("Ruben", "Mateus");

                await _context.SaveChangesAsync();
            }

            if (!_context.Lessees.Any())
            {
                await AddLessees("Filipe", "Baptista");
                await AddLessees("Claudio", "Carvalho");
                await AddLessees("Joao", "Pereira");
                await AddLessees("Francisco", "Mendes");
                await AddLessees("Iris", "Relvas");
                await AddLessees("Evandro", "Costa");
                await AddLessees("Goncalo", "Antunes");

                await _context.SaveChangesAsync();
            }
        }

        private async Task AddLessees(string fName, string lName)
        {
            User userFound = await GetUser(fName, lName);

            _context.Lessees.Add(new Lessee
            {
                Document = userFound.Document,
                FirstName = fName,
                LastName = lName,
                CellPhone = userFound.PhoneNumber,
                User = userFound
            });
        }

        private async Task AddOwner(string fName, string lName)
        {
            User userFound = await GetUser(fName, lName);

            _context.Owners.Add(new Owner
            {
                Document = userFound.Document,
                FirstName = fName,
                LastName = lName,
                CellPhone = userFound.PhoneNumber,
                User = userFound
            });
        }

        private async Task<User> GetUser(string fName, string lName)
        {
            string email = $"{fName}{lName}@mail.com";
            User userFound = null;

            userFound = await _userHelper.GetUserByEmailAsync(email);

            if (userFound == null)
                userFound = await AddUser(fName, lName);

            return userFound;
        }

        private async Task<User> AddUser(string fName, string lName)
        {
            string email = $"{fName}{lName}@mail.com";
            User user = await _userHelper.GetUserByEmailAsync(email);
            
            if (user != null)
            {
                return user;
            }

            user = new User
            {
                Document = _random.Next(1000000, 9999999).ToString(),
                FirstName = fName,
                LastName = lName,
                Email = email,
                UserName = email,
                PhoneNumber = _random.Next(100000000, 999999999).ToString()
            }; 

            var result = await _userHelper.AddUserAsync(user, "123456");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException($"Could not create the user in seeder - {result.Errors.First()}");
            }

            return user;
        }
    }
}
