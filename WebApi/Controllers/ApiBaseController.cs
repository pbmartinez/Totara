using Application.IAppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace WebApi.Controllers
{    
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public abstract class ApiBaseController<TEntityDto> : ControllerBase where TEntityDto : Domain.Entities.Entity
    {
        protected readonly IAppService<TEntityDto> AppService;

        /// <summary>
        /// List of related entities (navigation properties) to be included. Index view.
        /// </summary>
        protected List<Expression<Func<TEntityDto, object>>> Includes = new();
        /// <summary>
        /// List of related entities (navigation properties) to be included. Details view.
        /// </summary>
        protected List<Expression<Func<TEntityDto, object>>> DetailsIncludes = new();
        /// <summary>
        /// List of related entities (navigation properties) to be included. Edit view.
        /// </summary>
        protected List<Expression<Func<TEntityDto, object>>> EditIncludes = new();
        /// <summary>
        /// List of related entities (navigation properties) to be included. Delete view.
        /// </summary>
        protected List<Expression<Func<TEntityDto, object>>> DeleteIncludes = new();
        /// <summary>
        /// Defines the fields by which the list items will be ordered. The list items will be ordered multiple levels, 
        /// in the same order that the diccionary is feeded. For specifying Ascending / Descending order specify true / false in each field.
        /// Multiple related fields can be specified as long as none of them be a collection navigation property.
        /// To Do: Known Problem: Navigation properties belonging to collection properties can not be digged.
        /// </summary>
        protected Dictionary<string, bool> DefaultOrderBy = new();

        protected readonly ILogger<ApiBaseController<TEntityDto>> _logger;

        

        public ApiBaseController(IAppService<TEntityDto> appService,
            ILogger<ApiBaseController<TEntityDto>> logger)
        {
            AppService = appService;
            _logger = logger;
            
        }
        [HttpGet]
        [HttpHead]
        public virtual async Task<ActionResult<List<TEntityDto>>> Get()
        {
            var items = await AppService.GetAllAsync(Includes, DefaultOrderBy);
            return Ok(items);
        }

        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public virtual async Task<ActionResult<TEntityDto>> Get(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty)
                return BadRequest();
            var item = await AppService.GetAsync(id.Value, Includes);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(TEntityDto item)
        {
            var result = await AppService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(Guid? id, TEntityDto item)
        {
            if (id == null || id == Guid.Empty)
                return BadRequest();
            var itemTarget = await AppService.GetAsync(id.Value);
            if (itemTarget == null)
                return NotFound();
            var result = await AppService.UpdateAsync(item);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(Guid? id, JsonPatchDocument<TEntityDto> patchDocument)
        {
            if (id == null || id == Guid.Empty)
                return BadRequest();
            var item = await AppService.GetAsync(id.Value);
            if (item == null)
                return NotFound();            
            //TODO: Check client errors vs server error response
            patchDocument.ApplyTo(item);
            if(!TryValidateModel(item))
                return ValidationProblem(ModelState);
            var result = await AppService.UpdateAsync(item);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty)
                return NotFound();
            var item = await AppService.GetAsync(id.Value);
            if (item==null)
                return NotFound();
            var result = await AppService.RemoveAsync(id.Value);
            return NoContent();
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
