using Application.DTOs.ProductDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RequestParameters;
using Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateProductDto dto)
        {
            //1. Aynı isimde ürün var mı?
            bool nameExists = await _unitOfWork.ProductRepository.AnyAsync(p => p.Name == dto.Name);
            if (nameExists)
                throw new BusinessRuleViolationException("Aynı isimde bir ürün zaten var.");

            //2. Kategori var mı?
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new NotFoundException("Kategori bulunamadı!");

            //3. Product oluştur.
            var entity = _mapper.Map<Product>(dto);
            entity.SetImage(dto.ImageFile?.FileName ?? "Default.png");

            await _unitOfWork.ProductRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAscyn();

        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if(product is null)
                throw new NotFoundException($"{id} is not a product");

            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetAllDeletedAsync()
        {
            var deletedProduct = await _unitOfWork.ProductRepository.FindConditionAsync(p => p.IsDeleted, igonereFilters: true);
            if (deletedProduct is null)
                throw new NotFoundException("Silinmiş ürün bulunamadı!");

            return _mapper.Map<IEnumerable<ProductDto>>(deletedProduct);
        }

        public async Task<IEnumerable<ProductDto>> GetAllWithParametersAsync(ProductRequestParameters parameters)
        {
            var products = await _unitOfWork.ProductRepository.GetAllProductsWithParametersAsync(parameters);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if(product is null)
                throw new NotFoundException($"Product {id} is null");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDetailDto> GetByIdWithDetailsAsync(int id)
        {
            var productDetail = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<ProductDetailDto>(productDetail);
        }

        public async Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync(int count)
        {
            var products = await _unitOfWork.ProductRepository.GetRandomFeaturedProductsAsync(count);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<int> GetProductCount(int? catId = null)
        {
            if(!catId.HasValue)
                return await _unitOfWork.ProductRepository.CountAsync();
            else
                return await _unitOfWork.ProductRepository.CountAsync(p => p.CategoryId == catId);
        }

        public async Task<List<RecentProductDto>> GetRecentProductsAsync(int count)
        {
            var recentProducts = await _unitOfWork.ProductRepository.GetRecentProductsAsync(count);
            return _mapper.Map<List<RecentProductDto>>(recentProducts);
        }

        public async Task<List<ProductListDto>> GetRecommendedProductsAsync(int count = 4)
        {
            var products = await _unitOfWork.ProductRepository.GetTopSellingProductsAsync(count);
            return _mapper.Map<List<ProductListDto>>(products);
        }

        public async Task<List<ProductListDto>> GetSimilarProductsAsync(int categoryId, int excludeProductId, decimal price, int count = 4)
        {
            var allProducts = await _unitOfWork.ProductRepository.GetAllAsync();

            decimal minPrice = price * 0.8m; //%20 alt sınır.
            decimal maxPrice = price * 1.2m; //%20 üst sınır.

            var filtered = allProducts
                .Where(p => p.CategoryId == categoryId &&
                p.Id != excludeProductId &&
                p.Price >= minPrice &&
                p.Price <= maxPrice)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToList();

            return _mapper.Map<List<ProductListDto>>(filtered);
        }

        public async Task<IEnumerable<ProductSummaryDto>> GetSummariesAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductSummaryDto>>(products);
        }

        public async Task<UpdateProductDto?> GetUpdateDtoByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);
            return product is null ? null : _mapper.Map<UpdateProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> LowStockProducts(int stock)
        {
            var products = await _unitOfWork.ProductRepository.FindConditionAsync(p => p.Stock <= stock);
            products = products.Take(5).OrderByDescending(x => x.Stock);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task RestoreAsync(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(id, igonereFilters: true);
            if (entity is null)
                throw new NotFoundException($"Silinen kayıt bulunamadı! Id: {id}");

            entity.Restore();
            _unitOfWork.ProductRepository.Update(entity);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string name)
        {
            var products = await _unitOfWork.ProductRepository.FindConditionAsync(p => p.Name.ToLower().Contains(name.ToLower()));
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task SoftDeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product is null)
                throw new NotFoundException($"Silinicek ürün bulunamadı (id: {id})");

            _unitOfWork.ProductRepository.SoftDelete(product);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(dto.Id);
            if (entity is null)
                throw new NotFoundException("Ürün bulunamadı!");

            _mapper.Map(dto, entity);

            if(!string.IsNullOrEmpty(dto.NewImageFile?.FileName))
                entity.SetImage(dto.NewImageFile.FileName);

            _unitOfWork.ProductRepository.Update(entity);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task UpdateIsFeatured(int id, bool isFeatured)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (isFeatured)
                product.MarkAsFeatured();
            else
                product.UnMarkAsFeatured();

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();
        }
    }
}
