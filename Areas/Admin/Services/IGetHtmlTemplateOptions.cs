using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.DynamicWidget.Entities;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

public interface IGetHtmlTemplateOptions
{
    Task<IList<SelectListItem>> GetOptions(Func<HtmlTemplate, Task<bool>> selected = null,
        string emptyItemText = null);
}