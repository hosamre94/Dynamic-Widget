using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrCMS.Web.Admin.Infrastructure.BaseControllers;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin;

public class WebpageUrlSelectorController:MrCMSAdminController
{
    private readonly IWebpageUrlSelectorService _webpageUrlSelectorService;

    public WebpageUrlSelectorController(IWebpageUrlSelectorService webpageUrlSelectorService)
    {
        _webpageUrlSelectorService = webpageUrlSelectorService;
    }
    public async Task<JsonResult> WebpageSearch(string term, int page = 1)
    {
        var data = await _webpageUrlSelectorService.Search(term, page);
        return Json(new
        {
            total = data.TotalItemCount, items = data.ToList()
        });
    }
}