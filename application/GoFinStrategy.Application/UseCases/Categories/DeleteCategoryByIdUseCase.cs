using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.CrossCutting.Shared.Interfaces;
using GoFinStrategy.Domain.Interfaces;
using GoFinStrategy.Domain.Interfaces.Repositories;

namespace GoFinStrategy.Application.UseCases.Categories
{
    public class DeleteCategoryByIdUseCase : IDeleteCategoryByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationContext _notificationContext;
        readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryByIdUseCase(IUnitOfWork unitOfWork, INotificationContext notificationContext, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Execute(Guid id)
        {
            try
            {
                await _categoryRepository.Remove(id);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _notificationContext.AddNotification("-1", $"Erro desconhecido ao excluir registro {id}. Exception: {ex.Message}");
                return false;
            }
        }
    }
}
