using MrCMS.Web.Admin.Breadcrumbs;
using MrCMS.Web.Admin.Infrastructure.Breadcrumbs;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Breadcrumbs;

public class HtmlTemplatesBreadcrumb : Breadcrumb<SystemBreadcrumb>
{
    public override string Name => "Html Templates";
    public override string Controller => "HtmlTemplate";
    public override string Action => "Index";
    public override bool IsNav => true;
    public override decimal Order => 4;

}