using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.Application.Responses;
using GoFinStrategy.Domain.Interfaces.Repositories;

namespace GoFinStrategy.Application.UseCases.Categories
{
    public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase
    {
        readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> Execute(Guid id)
        {
            return await _categoryRepository.GetById(id);
        }
    }
}
