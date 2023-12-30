using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Common.AspNetCore.TagHelpers
{
    [HtmlTargetElement("input", Attributes =ForModelAttributeName)]
    public class DateTimePickerTagHelper : TagHelper
    {
        private const string ForModelAttributeName = "asp-picker-for";
        private const string ForEnableTimeAttributeName = "asp-enableTime-for";
        public DateTimePickerTagHelper(IHtmlGenerator generator)
        => Generator = generator;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        protected IHtmlGenerator Generator { get; }
        [HtmlAttributeName(ForModelAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ForEnableTimeAttributeName)]
        public bool EnableTime { get; set; } = false;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var modelExplorer = For.ModelExplorer;
            var metaData = For.Metadata;
            var id = $"{metaData.ContainerType.Name}_{metaData.PropertyName}";
             var hasModelValue = For.ModelExplorer.Model == null||
                                string.IsNullOrWhiteSpace(For.ModelExplorer.Model.ToString()) 
                                 ? "":$"value='{For.ModelExplorer.Model}'";

            var htmlResult = $"""
          <div class="form-floating form-floating-outline">
              <input type="text" {hasModelValue} name="{metaData.PropertyName}" id="{id}" class="form-control" placeholder="{metaData.DisplayName}" />
              <label id="{id}">{metaData.DisplayName}</label>
          </div>
          """;
            var timeHtml = EnableTime ? "H:i" : "";
            var scriptResult = $$"""
                <script>
                $(document).ready(function() {
                   $("#{{id}}").flatpickr({
                   enableTime: {{EnableTime.ToString().ToLower()}},
                   locale: 'fa',
                   dateFormat: "Y/m/d {{timeHtml}}"
                   });
                });
                </script>
                """;
            //output.Content.AppendHtml($"<p class='mt-3'>{metaData.DisplayName}</p>");
            output.Content.AppendHtml(htmlResult);
            output.Content.AppendHtml(scriptResult);
            await Task.CompletedTask;

        }
    }
}
