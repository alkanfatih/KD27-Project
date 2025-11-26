using Application.DTOs.CategoryDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.UnitOfWorks;

namespace Application.Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateCategoryDto dto)
        {
            bool exists = await _unitOfWork.CategoryRepository.AnyAsync(c => c.Name == dto.Name);

            if (exists)
                throw new BusinessRuleViolationException("Bu isimde bir kategori zaten var.");

            var category = new Category(dto.Name);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Silinecek kategori bulunamadı (id: {id})");

            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllDeletedAsync()
        {
            var deletedCategory = await _unitOfWork.CategoryRepository.FindConditionAsync(c => c.IsDeleted, igonereFilters: true);
            if (deletedCategory == null)
                throw new NotFoundException("Silinmiş kategori bulunamadı!");

            return _mapper.Map<IEnumerable<CategoryDto>>(deletedCategory);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Kategori bulunamadı. (id: {id})");

            return _mapper.Map<CategoryDto?>(category);
        }

        public async Task RestoreAsync(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id, true);
            if (entity == null)
                throw new NotFoundException($"Silincen kayıt bulunamadı. Id: {id}");

            entity.Restore();
            _unitOfWork.CategoryRepository.Update(entity);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Silincen kayıt bulunamadı. Id: {id}");

            //entity.SoftDelete();
            _unitOfWork.CategoryRepository.SoftDelete(entity);
            await _unitOfWork.SaveChangesAscyn();
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new NotFoundException($"Güncellenecek kayıt bulunamadı. Id: {dto.Id}");

            entity.Rename(dto.Name);
            _unitOfWork.CategoryRepository.Update(entity);
            await _unitOfWork.SaveChangesAscyn();
        }
    }
}
