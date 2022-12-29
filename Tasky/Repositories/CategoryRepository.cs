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

        public async Task<Category> AddNewCategory(CategoryRequestDto categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);

            return await _genericRepository.Add(newCategory);
        }

        public async Task<IAsyncEnumerable<Category>> GetAllCategories(int userId)
        {
            return await _genericRepository.GetAll();
        }

        public async Task<Category?> GetById(int userId, int id)
        {
            return await _genericRepository.FindBy(c => c.UserId == userId && c.Id == id);
        }

        public async Task<bool> RemoveCategory(int userId, int id)
        {
            var category = await _genericRepository.GetById(id);

            if (category == null) return false;

            await _genericRepository.Remove(category);

            return true;
        }

        public async Task<Category> UpdateCategory(CategoryRequestDto categoryDto, int id)
        {
            var category = await _genericRepository.GetById(id);

            if (category == null) return null;

            category.Name = categoryDto.Name;

            return await _genericRepository.Update(category);
        }
    }
}
