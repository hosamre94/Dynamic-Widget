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
        await _htmlTemplateAdminService.AddAsync(addHtmlTemplateModel);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<ViewResult> Edit(int id)
    {
        var htmlTemplate = await _htmlTemplateAdminService.GetAsync(id);
        return View(htmlTemplate);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Edit(UpdateHtmlTemplateModel model)
    {
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
}