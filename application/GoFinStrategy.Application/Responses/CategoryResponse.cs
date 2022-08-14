using GoFinStrategy.Domain.Entites;

namespace GoFinStrategy.Application.Responses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static implicit operator CategoryResponse(Category category)
        {
            return new CategoryResponse()
            {
                Id = category.Id,
                Name = category.Name.ToString()
            };
        }
    }
}
