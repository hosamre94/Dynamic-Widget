using MrCMS.Entities.Widget;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using MrCMS.Website;

namespace MrCMS.Web.Apps.DynamicWidget.Widgets;

[WidgetOutputCacheable]
public class TemplateWidget : Widget
{
    public virtual HtmlTemplate HtmlTemplate { get; set; }

    public virtual string Properties { get; set; }
}