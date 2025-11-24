using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Contracts
{
    public class AddressRepository : EfRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Address>> GetAllByUserIdAsync(int userId)
        {
            return await _dbSet.Where(a => a.CustomerId == userId && !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<Address?> GetByIdWithUserAsync(int id)
        {
            return await _dbSet.Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }
    }
}
