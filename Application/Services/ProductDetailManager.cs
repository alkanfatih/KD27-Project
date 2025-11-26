using Application.DTOs.ProductDetailsDTOs;
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
    public class ProductDetailManager : IProductDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductDetailManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(UpdateProductDetailDto dto)
        {
            var detail = _mapper.Map<ProductDetail>(dto);
            await _unitOfWork.ProductDetailRepository.AddAsync(detail);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _unitOfWork.ProductDetailRepository.AnyAsync(x => x.Id == id);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateProductDetailDto?> FirstOrDefaultAsync(int id)
        {
            var productDetail = await _unitOfWork.ProductDetailRepository.FirstOrDefaultAsync(x => x.ProductId == id);
            if (productDetail is null)
                return null;

            return _mapper.Map<UpdateProductDetailDto>(productDetail);
        }

        public async Task<UpdateProductDetailDto?> GetByIdOrDefaultAsync(int id)
        {
            var productDetail = await _unitOfWork.ProductDetailRepository.GetByIdAsync(id);
            if (productDetail is null)
                return null;

            return _mapper.Map<UpdateProductDetailDto>(productDetail);
        }

        public async Task UpdateAsync(UpdateProductDetailDto dto)
        {
            var productDetail = await _unitOfWork.ProductDetailRepository.GetByIdAsync(dto.ProductId);
            productDetail.Update(dto.Description, dto.Features, dto.Usage, dto.Warranty);
            _unitOfWork.ProductDetailRepository.Update(productDetail);
            _unitOfWork.SaveChanges();
        }
    }
}
