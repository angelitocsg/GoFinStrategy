using GoFinStrategy.Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace GoFinStrategy.Application.Requests
{
    public class AddOrUpdateCategoryRequest
    {
        public Guid? Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public static implicit operator Category(AddOrUpdateCategoryRequest request)
        {
            return new Category(request.Name).SetId(request.Id);
        }
    }
}
