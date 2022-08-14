using GoFinStrategy.Application.Requests;

namespace GoFinStrategy.Application.Interfaces.Categories
{
    public interface IUpdateCategoryUseCase
    {
        Task<Guid> Execute(Guid id, AddOrUpdateCategoryRequest category);
    }
}
