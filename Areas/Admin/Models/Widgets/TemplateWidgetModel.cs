using System.ComponentModel;
using MrCMS.Web.Admin.Infrastructure.ModelBinding;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using MrCMS.Web.Apps.DynamicWidget.Widgets;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Widgets;

public class TemplateWidgetAddModel : IAddPropertiesViewModel<TemplateWidget>
{
    [DisplayName("Template")]
    public int TemplateId { get; set; }
}

public class TemplateWidgetUpdateModel : IUpdatePropertiesViewModel<TemplateWidget>
{
    public HtmlTemplate Template { get; set; }
    public string Properties { get; set; }
}