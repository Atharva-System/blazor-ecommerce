using BlazorEcommerce.Application.Features.Category.Commands.AddCategory;
using BlazorEcommerce.Application.Features.Category.Commands.DeleteCategory;
using BlazorEcommerce.Application.Features.Category.Commands.UpdateCategory;
using BlazorEcommerce.Application.Features.Category.Query.GetCategories;
using BlazorEcommerce.Shared.Category;
using BlazorEcommerce.Shared.Response.Concrete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<CategoryDto>>>> GetCategories()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            return Ok(response);
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<DataResponse<List<CategoryDto>>>> GetAdminCategories()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }

        [HttpDelete("admin/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<DataResponse<List<CategoryDto>>>> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommandRequest(id));

            if (!result.Success)
            {
                var responseCast = (ErrorResponse)result;

                return new DataResponse<List<CategoryDto>> (new List<CategoryDto>(), responseCast.StatusCode, responseCast.Errors.FirstOrDefault());
            }

            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<DataResponse<List<CategoryDto>>>> AddCategory(CategoryDto category)
        {
            await _mediator.Send(new AddCategoryCommandRequest(category));

            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }

        [HttpPut("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<DataResponse<List<CategoryDto>>>> UpdateCategory(CategoryDto category)
        {
            var result = await _mediator.Send(new UpdateCategoryCommandRequest(category));

            if (!result.Success)
            {
                var responseCast = (ErrorResponse)result;

                return new DataResponse<List<CategoryDto>>(new List<CategoryDto>(), responseCast.StatusCode, responseCast.Errors.FirstOrDefault());
            }

            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }
    }
}
