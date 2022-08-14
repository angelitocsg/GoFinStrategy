using GoFinStrategy.Application.Responses;

namespace GoFinStrategy.Application.Interfaces.Categories
{
    public interface IGetAllCategoriesUseCase
    {
        Task<IEnumerable<CategoryResponse>> Execute();
    }
}
