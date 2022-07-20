using System.ComponentModel.DataAnnotations;
using MrCMS.Entities.Documents.Web;

namespace MrCMS.Web.Apps.DynamicWidget.Entities.BlockItems;

[Display(Name = "Settings")]
public class SettingsBlock : BlockItem
{
    public string Text { get; set; }
    public string Properties { get; set; }
}