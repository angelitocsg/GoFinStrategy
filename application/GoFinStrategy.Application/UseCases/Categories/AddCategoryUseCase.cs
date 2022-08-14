using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.Application.Requests;
using GoFinStrategy.CrossCutting.Shared.Interfaces;
using GoFinStrategy.Domain.Interfaces;
using GoFinStrategy.Domain.Interfaces.Repositories;

namespace GoFinStrategy.Application.UseCases.Categories
{
    public class AddCategoryUseCase : IAddCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationContext _notificationContext;
        readonly ICategoryRepository _categoryRepository;

        public AddCategoryUseCase(IUnitOfWork unitOfWork, INotificationContext notificationContext, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Execute(AddOrUpdateCategoryRequest category)
        {
            try
            {
                var id = await _categoryRepository.Add(category);
                _unitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                    if (ex.InnerException.Message.StartsWith("23505"))
                    {
                        _notificationContext.AddNotification("23505", "Registro já existe.");
                        return Guid.Empty;
                    }

                _notificationContext.AddNotification("-1", "Erro desconhecido ao salvar registro.");
                return Guid.Empty;
            }
        }
    }
}
