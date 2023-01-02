using AutoMapper;
using FluentValidation;
using Tasky.Dtos.Request.Category;
using Tasky.Dtos.Request.User;
using Tasky.Dtos.Response;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;
using Tasky.Models;

namespace Tasky.Services;

public class CategoryService : GenericService, ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IValidator<CategoryModificationRequestDto> _categoryModifyValidator;
    private readonly IValidator<CategoryRequestDto> _categoryGetValidator;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository repository,
        IValidator<CategoryRequestDto> categoryGetValidator,
        IValidator<CategoryModificationRequestDto> categoryModifyValidator, 
        IValidator<UserRequestDto> userValidator, 
        IMapper mapper) : base(userValidator)
    {
        _repository = repository;
        _categoryGetValidator = categoryGetValidator;
        _categoryModifyValidator = categoryModifyValidator;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto> CreateCategory(int userId, CategoryModificationRequestDto categoryDto)
    {
        await ValidateUserIdAndEntityFields(userId, categoryDto);

        var createdCategory = await _repository.AddCategoryByUser(userId, categoryDto);

        return GetMappedResponse(createdCategory);
    }

    public async Task<bool> DeleteCategory(int userId, int id)
    {
        await ValidateUserAndEntityIds(userId, id);

        var wasRemoved = await _repository.RemoveCategoryByUser(userId, id);

        return wasRemoved;
    }

    public async Task<CategoryResponseDto> GetCategoryById(int userId, int id)
    {
        await ValidateUserAndEntityIds(userId, id);

        var category = await _repository.GetByIdByUser(userId, id);

        return GetMappedResponse(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> ListCategories(int userId)
    {
        await this.ValidateUserId(userId);

        var allCategories = await _repository.GetAllCategoriesForUser(userId);

        var categoryList = new List<CategoryResponseDto>();

        foreach (var category in allCategories.ToEnumerable())
        {
            categoryList.Add(GetMappedResponse(category));
        }

        return categoryList;
    }

    public async Task<CategoryResponseDto> UpdateCategory(int userId, int id, CategoryModificationRequestDto categoryDto)
    {
        await ValidateEntireEntity(userId, id, categoryDto);

        var updatedCategory = await _repository.UpdateCategoryByUser(userId, categoryDto, id);

        return GetMappedResponse(updatedCategory);
    }

    private async Task ValidateUserAndEntityIds(int userId, int entityId)
    {
        await this.ValidateUserId(userId);
        await _categoryGetValidator.ValidateAndThrowAsync(new CategoryRequestDto { CategoryId = entityId });
    }

    private async Task ValidateEntireEntity(int userId, int entityId, CategoryModificationRequestDto dto)
    {
        await ValidateUserAndEntityIds(userId, entityId);
        await _categoryModifyValidator.ValidateAndThrowAsync(dto);
    }

    private async Task ValidateUserIdAndEntityFields(int userId, CategoryModificationRequestDto dto)
    {
        await this.ValidateUserId(userId);
        await _categoryModifyValidator.ValidateAndThrowAsync(dto);
    }
    
    private CategoryResponseDto GetMappedResponse(Category categoryModel)
    {
        return _mapper.Map<CategoryResponseDto>(categoryModel);
    }
}
