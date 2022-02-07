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
        where TEntityDto:Domain.Entities.Entity
        where TEntityDtoForCreate:Domain.Entities.Entity
        where TEntityDtoForUpdate:Domain.Entities.Entity
    {
        protected readonly IAppService<TEntityDto, TEntityDtoForCreate, TEntityDtoForUpdate> AppService;

        protected Expression<Func<TEntityDto, object>> Includes;
        protected List<Expression<Func<TEntityDto, object>>> IncludesList = new();

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
            var items = await AppService.GetAllAsync(IncludesList);
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
            var item = await AppService.GetForUpdateAsync(id);
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
            var item = await AppService.GetAsync(id);
            return await Task.FromResult(View(item));
        }
        [HttpGet]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var item = await AppService.GetAsync(id);
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
