using AutoMapper;
using FluentValidation;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;

namespace Tasky.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IValidator<CategoryRequestDto> _categoryValidator;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IValidator<CategoryRequestDto> categoryValidator, IMapper mapper)
        {
            _repository = repository;
            _categoryValidator = categoryValidator;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> CreateCategory(CategoryRequestDto category)
        {
            _categoryValidator.ValidateAndThrow(category);

            var createdCategory = await _repository.AddNewCategory(category);

            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var wasRemoved = await _repository.RemoveCategory(id);

            if (wasRemoved == false) throw new NotFoundException($"A category with id {id} was not found");

            return wasRemoved;
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _repository.GetById(id);

            if (category == null) throw new NotFoundException($"A category with id {id} was not found");

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<IEnumerable<CategoryResponseDto>> ListCategories()
        {
            var allCategories = await _repository.GetAllCategories();

            var categoryList = new List<CategoryResponseDto>();

            foreach (var category in allCategories.ToEnumerable())
            {
                categoryList.Add(_mapper.Map<CategoryResponseDto>(category));
            }

            return categoryList;
        }

        public async Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto category)
        {
            _categoryValidator.ValidateAndThrow(category);

            var updatedCategory = await _repository.UpdateCategory(category, id);

            if (updatedCategory == null) throw new NotFoundException($"A category with id {id} was not found");

            return _mapper.Map<CategoryResponseDto>(updatedCategory);
        }
    }
}
