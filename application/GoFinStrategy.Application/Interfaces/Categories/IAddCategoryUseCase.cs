using GoFinStrategy.Application.Requests;

namespace GoFinStrategy.Application.Interfaces.Categories
{
    public interface IAddCategoryUseCase
    {
        Task<Guid> Execute(AddOrUpdateCategoryRequest category);
    }
}
