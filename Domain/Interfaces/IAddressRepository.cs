using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<IEnumerable<Address>> GetAllByUserIdAsync(int userId);
        Task<Address?> GetByIdWithUserAsync(int id);
    }
}
