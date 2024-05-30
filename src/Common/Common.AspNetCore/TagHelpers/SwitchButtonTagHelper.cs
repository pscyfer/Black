using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Common.AspNetCore.TagHelpers
{
    [HtmlTargetElement("input", Attributes = ForAttributeName)]
    public class SwitchButtonTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-switch-for";
        public SwitchButtonTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        protected IHtmlGenerator Generator { get; }
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var modelExplorer = For.ModelExplorer;
            var metaData = For.Metadata;
            bool resultProperty = Convert.ToBoolean(modelExplorer.Model);
            string isChecked = resultProperty ? "checked" : "";

            var checkBoxGenerating =  Generator.GenerateCheckBox(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                resultProperty,
                htmlAttributes: new
                {
                    
                    @class = "switch-input",
                    @style = "margin-right: 3px;"
                });
            //output.Content.AppendHtml($"<p class='mt-3'>{metaData.DisplayName}</p>");
            output.Content.AppendHtml("<label class=\"switch\">");
            output.Content.AppendHtml(checkBoxGenerating);
            string switchHtml = $"""
            <span class="switch-toggle-slider">
              <span class="switch-on"></span>
              <span class="switch-off"></span>
            </span>
            <span class="switch-label">{metaData.DisplayName}</span>
          </label>
          """;
            output.Content.AppendHtml(switchHtml);
            await Task.CompletedTask;

        }
    }
}
