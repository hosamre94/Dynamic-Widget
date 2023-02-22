using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;

public class AddHtmlTemplateModel
{
    [Required,Remote("IsUniqueName", "HtmlTemplate", AdditionalFields = "Id")] public string Name { get; set; }
    
    public string Text { get; set; }
}

public class UpdateHtmlTemplateModel : AddHtmlTemplateModel
{
    public int Id { get; set; }
}