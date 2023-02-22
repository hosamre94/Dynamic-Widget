using MrCMS.Entities.Documents.Web;

namespace MrCMS.Web.Apps.DynamicWidget.Entities.BlockItems;

public class TemplateBlock : BlockItem
{
    public int TemplateId { get; set; }
    public string Properties { get; set; }
}