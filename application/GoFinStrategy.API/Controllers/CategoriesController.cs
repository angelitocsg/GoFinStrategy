using GoFinStrategy.Application.Interfaces.Categories;
using GoFinStrategy.Application.Requests;
using GoFinStrategy.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace GoFinStrategy.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(ILogger<CategoriesController> logger) : base(logger) { }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> Get([FromServices] IGetAllCategoriesUseCase getAllCategoriesUseCase)
        {
            return Ok(SuccessResponse.Content(await getAllCategoriesUseCase.Execute()));
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="getCategoryByIdUseCase"></param>
        /// <param name="id">category id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetById([FromServices] IGetCategoryByIdUseCase getCategoryByIdUseCase,
            [FromRoute] string id)
        {
            return Ok(SuccessResponse.Content(await getCategoryByIdUseCase.Execute(Guid.Parse(id))));
        }

        /// <summary>
        /// Add a category
        /// </summary>
        /// <param name="addCategoryUseCase"></param>
        /// <param name="category">request body</param>
        /// <returns></returns>
        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BaseResponse>> AddAsync([FromServices] IAddCategoryUseCase addCategoryUseCase,
            [Required][FromBody] AddOrUpdateCategoryRequest category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateErrors());

            var id = await addCategoryUseCase.Execute(category);

            return Created($"{HttpContext.Request.Path}/{id}", SuccessResponse.Content($"Item {id} created."));
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="updateCategoryUseCase"></param>
        /// <param name="id">id</param>
        /// <param name="category">request body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult<BaseResponse>> UpdateAsync([FromServices] IUpdateCategoryUseCase updateCategoryUseCase,
            [Required][FromRoute] string id,
            [Required][FromBody] AddOrUpdateCategoryRequest category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateErrors());

            await updateCategoryUseCase.Execute(Guid.Parse(id), category);

            return Accepted($"{HttpContext.Request.Path}/{id}", SuccessResponse.Content($"Item {id} updated."));
        }

        /// <summary>
        /// Remove a category
        /// </summary>
        /// <param name="deleteCategoryByIdUse"></param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult<BaseResponse>> DeleteAsync([FromServices] IDeleteCategoryByIdUseCase deleteCategoryByIdUse,
            [Required][FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelStateErrors());

            await deleteCategoryByIdUse.Execute(Guid.Parse(id));

            return Accepted($"{HttpContext.Request.Path}/{id}", SuccessResponse.Content($"Item {id} deleted."));
        }
    }
}
