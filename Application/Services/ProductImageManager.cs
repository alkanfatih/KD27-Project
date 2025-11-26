using Application.DTOs.ProductImageDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductImageManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductImageDto>> GetAllByProductIdAsync(int productId)
        {
            var images = await _unitOfWork.ProductImageRepository.FindConditionAsync(i => i.ProductId == productId);
            return _mapper.Map<IEnumerable<ProductImageDto>>(images);
        }

        public async Task<ProductImageDto?> GetByIdAsync(int id)
        {
            var image = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            if (image == null)
                throw new NotFoundException($"Ürün görseli bulunamadı. (id: {id})");

            return _mapper.Map<ProductImageDto>(image);
        }

        public async Task AddAsync(CreateProductImageDto dto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
            if (product is null)
                throw new NotFoundException($"Ürün bulunamadı (id: {dto.ProductId})");

            var image = new ProductImage(dto.ProductId, dto.ImageUrl);
            await _unitOfWork.ProductImageRepository.AddAsync(image);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task UpdateAsync(UpdateProductImageDto dto)
        {
            var existing = await _unitOfWork.ProductImageRepository.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new NotFoundException($"Güncellenecek görsel bulunamadı. (id: {dto.Id})");

            _mapper.Map(dto, existing);
            _unitOfWork.ProductImageRepository.Update(existing);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Silinecek görsel bulunamadı. (id: {id})");

            _unitOfWork.ProductImageRepository.Delete(entity);

            await _unitOfWork.SaveChangesAscyn();
        }
        public async Task AddImagesAsync(int productId, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine("wwwroot/images/products", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                await _unitOfWork.ProductImageRepository.AddAsync(new ProductImage(productId, fileName));
            }

            await _unitOfWork.SaveChangesAscyn();
        }
    }
}
