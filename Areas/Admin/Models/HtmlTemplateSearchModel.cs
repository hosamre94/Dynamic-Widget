namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;

public class HtmlTemplateSearchModel
{
    public HtmlTemplateSearchModel()
    {
        Page = 1;
    }
    
    public int Page { get; set; }
    public string Name { get; set; }
}