using Application.IAppServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public abstract class BaseController<TEntityDto, TEntityDtoForCreate, TEntityDtoForUpdate> : Controller
        where TEntityDto : Domain.Entities.Entity
        where TEntityDtoForCreate : Domain.Entities.Entity
        where TEntityDtoForUpdate : Domain.Entities.Entity
    {
        protected readonly IAppService<TEntityDto, TEntityDtoForCreate, TEntityDtoForUpdate> AppService;

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

        public BaseController(IAppService<TEntityDto, TEntityDtoForCreate, TEntityDtoForUpdate> appService)
        {
            AppService = appService;

        }

        public virtual async Task CargarViewBagsCreate()
        {
            await Task.CompletedTask;
        }
        public virtual async Task CargarViewBagsEdit(Guid id)
        {
            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> Index()
        {
            var items = await AppService.GetAllAsync(Includes, DefaultOrderBy);
            return View(items);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Create()
        {
            await CargarViewBagsCreate();
            return await Task.FromResult(View());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntityDtoForCreate item)
        {
            if (ModelState.IsValid)
            {
                var commit = await AppService.AddAsync(item);
                if (commit)
                    return RedirectToAction(nameof(Index));
            }
            await CargarViewBagsCreate();
            return View(item);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid id)
        {
            var item = await AppService.GetForUpdateAsync(id, EditIncludes);
            await CargarViewBagsEdit(id);
            return await Task.FromResult(View(item));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(TEntityDtoForUpdate item)
        {
            if (ModelState.IsValid)
            {
                var commit = await AppService.UpdateAsync(item);
                if (commit)
                    return RedirectToAction(nameof(Index));
            }
            //await CargarViewBagsEdit(item.Id);
            return View(item);
        }
        [HttpGet]
        public virtual async Task<IActionResult> Details(Guid id)
        {
            var item = await AppService.GetAsync(id, DetailsIncludes);
            return await Task.FromResult(View(item));
        }
        [HttpGet]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var item = await AppService.GetAsync(id, DeleteIncludes);
            return await Task.FromResult(View(item));
        }
        [HttpGet]
        public virtual async Task<IActionResult> DeleteConfirm(Guid id)
        {
            var commit = await AppService.RemoveAsync(id);
            if (commit)
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(Delete), id);
        }
    }
}
