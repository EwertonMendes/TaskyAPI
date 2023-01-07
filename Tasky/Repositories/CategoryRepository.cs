using AutoMapper;
using Tasky.Dtos.Request.Category;
using Tasky.Interfaces.Repositories;
using Tasky.Models;

namespace Tasky.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Category> _genericRepository;

    public CategoryRepository(IMapper mapper, IGenericRepository<Category> genericRepository)
    {
        _mapper = mapper;
        _genericRepository = genericRepository;
    }

    public async Task<Category> AddCategoryByUser(int userId, CategoryModificationRequestDto categoryDto)
    {
        var newCategory = _mapper.Map<Category>(categoryDto, opt =>
            opt.AfterMap((src, dest) => dest.UserId = userId));

        return await _genericRepository.Add(newCategory);
    }

    public async Task<IAsyncEnumerable<Category>> GetAllCategoriesForUser(int userId)
    {
        var categories = await _genericRepository.Where(c => c.UserId == userId);
        return categories.ToAsyncEnumerable();
    }

    public async Task<Category?> GetById(int id)
    {
        return await _genericRepository.GetById(id);
    }

    public async Task<Category?> GetByIdByUser(int userId, int id)
    {
        return await _genericRepository.FindBy(c => c.UserId == userId && c.Id == id);
    }

    public async Task<bool> RemoveCategoryByUser(int userId, int id)
    {
        var category = await GetByIdByUser(userId, id);

        if (category == null) return false;

        await _genericRepository.Remove(category);

        return true;
    }

    public async Task<Category> UpdateCategoryByUser(int userId, CategoryModificationRequestDto categoryDto, int id)
    {
        var category = await GetByIdByUser(userId, id);

        if (category == null) return null;

        category.Name = categoryDto.Name;

        return await _genericRepository.Update(category);
    }
}
