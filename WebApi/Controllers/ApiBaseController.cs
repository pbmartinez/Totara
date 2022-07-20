using Application.IAppServices;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using WebApi.Helpers;
using WebApi.Parameters;

namespace WebApi.Controllers
{    
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public abstract class ApiBaseController<TEntityDto, TKey> : ControllerBase where TEntityDto : Domain.Entities.Base.Entity
    {
        protected readonly IAppService<TEntityDto, TKey> AppService;


        /// <summary>
        /// List of related entities (navigation properties) to be included. Index view.
        /// </summary>
        protected List<string> Includes = new ();
        
        /// <summary>
        /// List of related entities (navigation properties) to be included. Details view.
        /// </summary>
        protected List<string> DetailsIncludes = new ();
        
        /// <summary>
        /// List of related entities (navigation properties) to be included. Edit view.
        /// </summary>
        protected List<string> EditIncludes = new ();
        
        /// <summary>
        /// List of related entities (navigation properties) to be included. Delete view.
        /// </summary>
        protected List<string> DeleteIncludes = new ();
        /// <summary>
        /// Defines the fields by which the list items will be ordered. The list items will be ordered multiple levels, 
        /// in the same order that the diccionary is feeded. For specifying Ascending / Descending order specify true / false in each field.
        /// Multiple related fields can be specified as long as none of them be a collection navigation property.
        /// To Do: Known Problem: Navigation properties belonging to collection properties can not be digged.
        /// </summary>
        protected Dictionary<string, bool> DefaultOrderBy = new();

        protected readonly ILogger<ApiBaseController<TEntityDto, TKey>> _logger;

        protected readonly IPropertyCheckerService _propertyCheckerService;

        protected ApiBaseController(IAppService<TEntityDto, TKey> appService, ILogger<ApiBaseController<TEntityDto, TKey>> logger, IPropertyCheckerService propertyCheckerService)
        {
            AppService = appService ?? throw new ArgumentNullException(nameof(appService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _propertyCheckerService = propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery]QueryStringParameters queryStringParameters, CancellationToken cancellationToken = default)
        {
            if (!_propertyCheckerService.TypeHasProperties<TEntityDto>(queryStringParameters.Fields))
                return BadRequest();
            var includes = queryStringParameters.Includes.Split(',').ToList();
            var items = await AppService.GetAllAsync(includes, DefaultOrderBy, cancellationToken);
            var result = items.ShapeDataOnIEnumerable(queryStringParameters.Fields);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntityDto>> Get(TKey id, [FromQuery] QueryStringParameters queryStringParameters, CancellationToken cancellationToken = default)
        {
            if (!_propertyCheckerService.TypeHasProperties<TEntityDto>(queryStringParameters.Fields))
                return BadRequest();
            if (id == null)
                return BadRequest();
            var includes = queryStringParameters.Includes.Split(',').ToList();
            var item = await AppService.GetAsync(id, includes, cancellationToken);
            
            if (item == null)
                return NotFound();
            return Ok(item.ShapeDataOnObject(queryStringParameters.Fields ?? string.Empty));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(TEntityDto item, CancellationToken cancellationToken = default)
        {
            if(item.IsTransient())
                item.GenerateIdentity();
            var result = await AppService.AddAsync(item, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(TKey id, TEntityDto item, CancellationToken cancellationToken = default)
        {
            if (id == null)
                return BadRequest();
            var itemTarget = await AppService.GetAsync(id,cancellationToken: cancellationToken);
            if (itemTarget == null)
                return NotFound();
            var result = await AppService.UpdateAsync(item, cancellationToken);
            return NoContent();
        }        
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            if (id == null )
                return NotFound();
            var item = await AppService.GetAsync(id,cancellationToken: cancellationToken );
            if (item==null)
                return NotFound();
            var result = await AppService.RemoveAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
