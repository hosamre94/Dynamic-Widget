using MrCMS.Web.Admin.Infrastructure.Services.Content;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Content;
using MrCMS.Web.Apps.DynamicWidget.Entities.ContentBlocks;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services.Content;

public class DynamicContentBlockAdminConfiguration : ContentBlockAdminConfigurationBase<DynamicContentBlock,
    UpdateDynamicContentBlockModel>
{
    public override UpdateDynamicContentBlockModel GetEditModel(DynamicContentBlock block)
    {
        return new UpdateDynamicContentBlockModel
            { Text = block.Settings.Text };
    }

    public override void UpdateBlock(DynamicContentBlock block, UpdateDynamicContentBlockModel editModel)
    {
        block.Settings.Text = editModel.Text;
    }
}