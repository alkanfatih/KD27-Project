using Application.DTOs.AdressDTOs;
using Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllByCustomerIdAsync(int customerId);
        Task<AddressDto?> GetByIdAsync(int id);
        Task AddAsync(CreateAddressDto dto);
        Task UpdateAsync(UpdateAddressDto dto);
        Task DeleteAsync(int id);
    }
}
