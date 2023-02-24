using MrCMS.Entities;

namespace MrCMS.Web.Apps.DynamicWidget.Entities;

public class HtmlTemplate : SiteEntity
{
    public virtual string Name { get; set; }
    public virtual string Text { get; set; }
}