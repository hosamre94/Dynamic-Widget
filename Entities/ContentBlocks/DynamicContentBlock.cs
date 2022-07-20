using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.DynamicWidget.Entities.BlockItems;

namespace MrCMS.Web.Apps.DynamicWidget.Entities.ContentBlocks;

[Display(Name = "Dynamic")]
public class DynamicContentBlock : IContentBlock
{
    public IReadOnlyList<BlockItem> Items => new BlockItem[] { Settings };
    public SettingsBlock Settings { get; set; } = new() { Name = "Settings" };
}