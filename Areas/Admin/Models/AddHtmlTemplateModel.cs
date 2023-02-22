using System.ComponentModel.DataAnnotations;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;

public class AddHtmlTemplateModel
{
    [Required] public string Name { get; set; }

    public string Text { get; set; }
}

public class UpdateHtmlTemplateModel : AddHtmlTemplateModel
{
    public int Id { get; set; }
}