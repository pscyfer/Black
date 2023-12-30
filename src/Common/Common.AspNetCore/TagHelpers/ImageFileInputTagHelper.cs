using Common.AspNetCore.RootDirectory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Common.AspNetCore.TagHelpers
{
    [HtmlTargetElement("div", Attributes = ForModelAttributeName)]
    public class ImageFileInputTagHelper : TagHelper
    {
        private const string ForModelAttributeName = "asp-img-for";
        private const string ForImageWidthAttributeName = "asp-img-width-for";
        private const string ForImageHeightAttributeName = "asp-img-height-for";

        private const string ForAcceptFileAttributeName = "asp-img-acceptFile-for";
        private const string ForValueAttributeName = "asp-img-value-for";
        public ImageFileInputTagHelper(IHtmlGenerator generator)
            => Generator = generator;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        protected IHtmlGenerator Generator { get; }
        [HtmlAttributeName(ForModelAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ForImageWidthAttributeName)]
        public int ImageWidth { get; set; } = 150;

        [HtmlAttributeName(ForImageHeightAttributeName)]
        public int ImageHeight { get; set; } = 150;


        [HtmlAttributeName(ForAcceptFileAttributeName)]
        public string AcceptFile { get; set; } = ".jpg, .png, .jpeg, .gif, .tif, .tiff, .JPG,";

        [HtmlAttributeName(ForValueAttributeName)]
        public ModelExpression Value { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var metaData = For.Metadata;
            //var id = $"{metaData.ContainerType.Name}_{metaData.PropertyName}";
            var hasModelValue = Value.ModelExplorer.Model == null ||
                                string.IsNullOrWhiteSpace(Value.ModelExplorer.Model.ToString())
                                 ? $"<img alt='{metaData.DisplayName}' src=\"{RootDirctories.UserDefaultAvatar}\" />" :
                                 $"<img alt='{metaData.DisplayName}' src=\"{Value.ModelExplorer.Model}\" width=\"{ImageWidth}\" height=\"{ImageHeight}\" />";

            var htmlResult = $"""
                <p>
                  {metaData.DisplayName}
                </p>
                <div class="fileinput fileinput-new" data-provides="fileinput">
                    <div class="fileinput-new thumbnail" style="height: 150px;">
                        {hasModelValue}
                    </div>
                    <div class="fileinput-preview fileinput-exists thumbnail" style="height:200px"> </div>
                    <div>
                        <span class="btn default btn-file">
                            <span class="fileinput-new btn btn-success"> انتخاب کنید </span>
                            <span class="fileinput-exists btn btn-primary ms-3"> تغییر عکس </span>
                            <input type="file" class="fileinput-filename"   name="{metaData.PropertyName}" accept="{AcceptFile}" />
                        </span>
                        <a href="javascript:;" class="btn btn-danger fileinput-exists" data-dismiss="fileinput"> حذف </a>
                    </div>
                </div></br>
                """;
            output.Content.AppendHtml(htmlResult);
            await Task.CompletedTask;

        }
    }
}
