﻿using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;

namespace Tasky.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> CreateCategory(CategoryRequestDto category)
        {
            var createdCategory = await _repository.AddNewCategory(category);

            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await _repository.RemoveCategory(id);
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _repository.GetById(id);

            if (category == null) throw new Exception($"Category with id {id} not found");

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
            var updatedCategory = await _repository.UpdateCategory(category, id);

            if (updatedCategory == null) throw new Exception($"Category with id {id} not found");

            return _mapper.Map<CategoryResponseDto>(updatedCategory);
        }
    }
}
