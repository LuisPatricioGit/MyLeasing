using Microsoft.AspNetCore.Identity;
using MyLeasing.Common.Data.Entities;
using System.Threading.Tasks;

namespace MyLeasing.Common.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
