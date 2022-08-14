using GoFinStrategy.Application.Responses;

namespace GoFinStrategy.Application.Interfaces.Categories
{
    public interface IGetCategoryByIdUseCase
    {
        Task<CategoryResponse> Execute(Guid id);
    }
}
