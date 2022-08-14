namespace GoFinStrategy.Application.Interfaces.Categories
{
    public interface IDeleteCategoryByIdUseCase
    {
        Task<bool> Execute(Guid id);
    }
}
