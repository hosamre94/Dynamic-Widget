using MrCMS.Web.Admin.Infrastructure.Services.Content;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Content;
using MrCMS.Web.Apps.DynamicWidget.Entities.ContentBlocks;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services.Content;

public class TemplateContentBlockAdminConfiguration : ContentBlockAdminConfigurationBase<TemplateContentBlock,
    UpdateTemplateContentBlockModel>
{
    public override UpdateTemplateContentBlockModel GetEditModel(TemplateContentBlock block)
    {
        return new UpdateTemplateContentBlockModel
            { TemplateId = block.Template.TemplateId };
    }

    public override void UpdateBlock(TemplateContentBlock block, UpdateTemplateContentBlockModel editModel)
    {
        block.Template.TemplateId = editModel.TemplateId;
    }
}