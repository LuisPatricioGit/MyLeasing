using MyLeasing.Common.Data.Entities;
using System.Linq;

namespace MyLeasing.Common.Data
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        public IQueryable GetAllWithUsers();

    }
}
