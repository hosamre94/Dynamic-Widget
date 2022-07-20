using MrCMS.Web.Admin.Infrastructure.Services.Content;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Content;
using MrCMS.Web.Apps.DynamicWidget.Entities.BlockItems;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services.Content;

public class SettingsBlockAdminConfiguration : BlockItemAdminConfigurationBase<SettingsBlock, UpdateSettingsBlockModel>
{
    public override UpdateSettingsBlockModel GetEditModel(SettingsBlock block)
    {
        return new UpdateSettingsBlockModel
        {
            Properties = block.Properties,
            Text = block.Text
        };
    }

    public override void UpdateBlockItem(SettingsBlock block, UpdateSettingsBlockModel editModel)
    {
        block.Properties = editModel.Properties;
    }
}