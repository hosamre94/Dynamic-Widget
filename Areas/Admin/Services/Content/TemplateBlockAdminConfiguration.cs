using MrCMS.Web.Admin.Infrastructure.Services.Content;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Content;
using MrCMS.Web.Apps.DynamicWidget.Entities.BlockItems;
using MrCMS.Web.Apps.DynamicWidget.Services;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services.Content;

public class TemplateBlockAdminConfiguration : BlockItemAdminConfigurationBase<TemplateBlock, UpdateSettingsBlockModel>
{
    private readonly IGetHtmlTemplateService _htmlTemplateService;

    public TemplateBlockAdminConfiguration(IGetHtmlTemplateService htmlTemplateService)
    {
        _htmlTemplateService = htmlTemplateService;
    }

    public override UpdateSettingsBlockModel GetEditModel(TemplateBlock block)
    {
        var template = _htmlTemplateService.Get(block.TemplateId).GetAwaiter().GetResult();
        return new UpdateSettingsBlockModel
        {
            Properties = block.Properties,
            Text = template?.Text
        };
    }

    public override void UpdateBlockItem(TemplateBlock block, UpdateSettingsBlockModel editModel)
    {
        block.Properties = editModel.Properties;
    }
}