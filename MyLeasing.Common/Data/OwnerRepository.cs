using MyLeasing.Common.Data.Entities;

namespace MyLeasing.Common.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(DataContext context) : base(context)
        {

        }
    }
}
