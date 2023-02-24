using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Content;

public class UpdateTemplateContentBlockModel
{
    [Required]
    [DisplayName("Template")]
    public int TemplateId { get; set; } 
}