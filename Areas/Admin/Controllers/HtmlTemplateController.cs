using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrCMS.Web.Admin.Infrastructure.BaseControllers;
using MrCMS.Web.Admin.Infrastructure.Helpers;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Controllers;

public class HtmlTemplateController : MrCMSAppAdminController<DynamicWidgetApp>
{
    private readonly IHtmlTemplateAdminService _htmlTemplateAdminService;

    public HtmlTemplateController(IHtmlTemplateAdminService htmlTemplateAdminService)
    {
        _htmlTemplateAdminService = htmlTemplateAdminService;
    }
    
    [HttpGet]
    public async Task<ViewResult> Index(HtmlTemplateSearchModel searchModel)
    {
        ViewData["items"] = await _htmlTemplateAdminService.SearchAsync(searchModel);
        return View(searchModel);
    }

    [HttpGet]
    public ActionResult Add()
    {
        return View(new AddHtmlTemplateModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddHtmlTemplateModel addHtmlTemplateModel)
    {
        if (!ModelState.IsValid) return View(addHtmlTemplateModel);

        if (!await _htmlTemplateAdminService.IsUniqueName(addHtmlTemplateModel.Name, null))
        {
            ModelState.AddModelError(string.Empty,$"{addHtmlTemplateModel.Name} already registered.");
            View(addHtmlTemplateModel);
        }
        
        await _htmlTemplateAdminService.AddAsync(addHtmlTemplateModel);
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<ViewResult> Edit(int id)
    {
        var UpdateHtmlTemplateModel = await _htmlTemplateAdminService.GetUpdateModel(id);
        return View(UpdateHtmlTemplateModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateHtmlTemplateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        if (!await _htmlTemplateAdminService.IsUniqueName(model.Name, model.Id))
        {
            ModelState.AddModelError(string.Empty,$"{model.Name} already registered.");
            View(model);
        }
        
        await _htmlTemplateAdminService.UpdateAsync(model);
        TempData.AddSuccessMessage($"'{model.Name}' updated");
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<ViewResult> Delete(int id)
    {
        return View(await _htmlTemplateAdminService.GetAsync(id));
    }

    [HttpPost]
    [ActionName(nameof(Delete))]
    public async Task<RedirectToActionResult> Delete_POST(int id)
    {
        await _htmlTemplateAdminService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<JsonResult> IsUniqueName(string name, int? id)
    {
        return await _htmlTemplateAdminService.IsUniqueName(name, id)
            ? Json(true)
            : Json($"{name} already registered.");
    }
}