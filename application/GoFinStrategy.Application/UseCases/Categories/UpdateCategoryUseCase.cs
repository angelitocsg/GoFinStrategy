using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.Application.Requests;
using GoFinStrategy.CrossCutting.Shared.Interfaces;
using GoFinStrategy.Domain.Interfaces;
using GoFinStrategy.Domain.Interfaces.Repositories;

namespace GoFinStrategy.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationContext _notificationContext;
        readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryUseCase(IUnitOfWork unitOfWork, INotificationContext notificationContext, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Execute(Guid id, AddOrUpdateCategoryRequest category)
        {
            try
            {
                if (category.Id == null)
                {
                    _notificationContext.AddNotification("-1", "Identificação do registro não informado.");
                    return Guid.Empty;
                }

                await _categoryRepository.Update(id, category);
                _unitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                _notificationContext.AddNotification("-1", $"Erro desconhecido ao salvar registro {id}. Exception: {ex.Message}");
                return Guid.Empty;
            }
        }
    }
}
