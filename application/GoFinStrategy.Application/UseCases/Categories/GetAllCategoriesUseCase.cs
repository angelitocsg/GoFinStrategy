using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.Application.Responses;
using GoFinStrategy.Domain.Interfaces.Repositories;

namespace GoFinStrategy.Application.UseCases.Categories
{
    public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
    {
        readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponse>> Execute()
        {
            return (await _categoryRepository.GetAll()).Select(x => (CategoryResponse)x);
        }
    }
}
