using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Repositories;
using Tasky.Models;

namespace Tasky.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _genericRepository;

        public CategoryRepository(IMapper mapper, IGenericRepository<Category> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public Category? AddNewCategory(CategoryRequestDto categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);

            return _genericRepository.Add(newCategory);
        }

        public IEnumerable<Category?> GetAllCategories()
        {
            return _genericRepository.GetAll();
        }

        public Category? GetById(int id)
        {
            return _genericRepository.GetById(id);
        }

        public bool RemoveCategory(int id)
        {
            var category = _genericRepository.GetById(id);

            if (category == null) return false;

            _genericRepository.Remove(category);

            return true;
        }

        public Category? UpdateCategory(CategoryRequestDto categoryDto, int id)
        {
            var category = _genericRepository.GetById(id);

            if (category == null) return null;

            category.Name = categoryDto.Name;

            return _genericRepository.Update(category);
        }
    }
}
