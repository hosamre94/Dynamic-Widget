using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.DynamicWidget.Entities.BlockItems;

namespace MrCMS.Web.Apps.DynamicWidget.Entities.ContentBlocks;

[Display(Name = "Template")]
public class TemplateContentBlock: IContentBlock
{
    public IReadOnlyList<BlockItem> Items => new BlockItem[] { Template };
    public TemplateBlock Template { get; set; } = new() { Name = "Settings" };
}