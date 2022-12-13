using AutoMapper;
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

        public CategoryResponseDto CreateCategory(CategoryRequestDto category)
        {
            var createdCategory = _repository.AddNewCategory(category);

            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public bool DeleteCategory(int id)
        {
            return _repository.RemoveCategory(id);
        }

        public CategoryResponseDto GetCategoryById(int id)
        {
            var category = _repository.GetById(id);

            if (category == null) throw new Exception($"Category with id {id} not found");

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public IEnumerable<CategoryResponseDto> ListCategories()
        {
            var allCategories = _repository.GetAllCategories().ToList();

            var categoryList = new List<CategoryResponseDto>();

            foreach (var category in allCategories)
            {
                categoryList.Add(_mapper.Map<CategoryResponseDto>(category));
            }

            return categoryList;
        }

        public CategoryResponseDto UpdateCategory(int id, CategoryRequestDto category)
        {
            var updatedCategory = _repository.UpdateCategory(category, id);

            if (updatedCategory == null) throw new Exception($"Category with id {id} not found");

            return _mapper.Map<CategoryResponseDto>(updatedCategory);
        }
    }
}
