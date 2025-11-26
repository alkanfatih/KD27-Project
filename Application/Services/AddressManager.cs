using Application.DTOs.AdressDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AddressManager : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> GetAllByCustomerIdAsync(int customerId)
        {
            var addresses = await _unitOfWork.AddressRepository.FindConditionAsync(a => a.CustomerId == customerId);
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

        public async Task<AddressDto?> GetByIdAsync(int id)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
                throw new NotFoundException($"Adres bulunamadı. (id: {id})");

            return _mapper.Map<AddressDto>(address);
        }

        public async Task AddAsync(CreateAddressDto dto)
        {
            // İş kuralı: Aynı kullanıcıya ait aynı başlıkta adres var mı?
            bool exists = await _unitOfWork.AddressRepository.AnyAsync(a => a.CustomerId == dto.UserId && a.Title == dto.Title);
            if (exists)
                throw new Exceptions.BusinessRuleViolationException("Bu başlıkta bir adres zaten kayıtlı.");

            var address = new Address(dto.UserId, dto.Title, dto.Country, dto.City, dto.District, dto.PostalCode, dto.Line);
            await _unitOfWork.AddressRepository.AddAsync(address);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task UpdateAsync(UpdateAddressDto dto)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(dto.Id);
            if (address == null)
                throw new NotFoundException($"Güncellenecek adres bulunamadı. (id: {dto.Id})");

            _mapper.Map(dto, address);
            _unitOfWork.AddressRepository.Update(address);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task DeleteAsync(int id)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
                throw new NotFoundException($"Silinecek adres bulunamadı. (id: {id})");

            _unitOfWork.AddressRepository.Delete(address);
            await _unitOfWork.SaveChangesAscyn();
        }
    }
}
