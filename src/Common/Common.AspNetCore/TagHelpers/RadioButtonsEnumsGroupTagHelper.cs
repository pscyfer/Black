using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Common.AspNetCore.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;enum-radio-button&gt; elements with an <c>asp-for</c> attribute, <c>value</c> attribute.
    /// </summary>
    [HtmlTargetElement("radioButtonGroup-Enum", Attributes = RadioButtonEnumForAttributeName)]
    public class CMSRadioButtonsEnumsGroupTagHelper : TagHelper
    {
        //Attributes
        private const string RadioButtonEnumForAttributeName = "asp-for";
        private const string RadioButtonEnumValueAttributeName = "value";
        private const string RadioButtonEnumDisplayName = "display-Name";

        /// <summary>
        /// Creates a new <see cref="CMSRadioButtonsEnumsGroupTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        public CMSRadioButtonsEnumsGroupTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <inheritdoc />
        public override int Order => -1000;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(RadioButtonEnumForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(RadioButtonEnumValueAttributeName)]
        public Enum value { get; set; }

        [HtmlAttributeName(RadioButtonEnumDisplayName)]
        public string DisplayName { get; set; }
        /// <inheritdoc />
        /// <remarks>Does nothing if <see cref="For"/> is <c>null</c>.</remarks>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            //var childContent = await output.GetChildContentAsync();
            //string innerContent = childContent.GetContent();
            //output.Content.AppendHtml(innerContent);

            //output.TagName = "div";
            //output.TagMode = TagMode.StartTagAndEndTag;
            //output.Attributes.Add("class", "btn-group btn-group-radio");

            output.TagName = null;
            //output.TagMode = TagMode.StartTagAndEndTag;
            //output.Attributes.Add("class", "btn-group btn-group-radio");

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var modelExplorer = For.ModelExplorer;
            var metaData = For.Metadata;
            string display = string.IsNullOrWhiteSpace(DisplayName) ? metaData.DisplayName : DisplayName;
            output.Content.AppendHtml($"<p class='mt-3'>{display}</p>");
            foreach (var item in metaData.EnumNamesAndValues)
            {
               
                string enum_id = $"{metaData.ContainerType.Name}_{metaData.PropertyName}_{item.Key}";

                bool enum_ischecked = false;
                if (value != null)
                {
                    if (value != null && item.Key.ToString() == value.ToString())
                    {
                        enum_ischecked = true;
                    }
                }
                else
                {
                    if (For.Model != null && item.Key.ToString() == For.Model.ToString())
                    {
                        enum_ischecked = true;
                    }
                }

                string enumInputLabelName = item.Key;
                var enumResourcedName = metaData.EnumGroupedDisplayNamesAndValues.FirstOrDefault(x => x.Value == item.Value);
                if (enumResourcedName.Value != null)
                {
                    enumInputLabelName = enumResourcedName.Key.Name;
                }
                //Gerating button For EnumType
                var enumRadio = Generator.GenerateRadioButton(
                    ViewContext,
                    For.ModelExplorer,
                    metaData.PropertyName,
                    item.Key,
                    false,
                    htmlAttributes: new { @id = enum_id, @class = "form-check-input",
                         });
                enumRadio.Attributes.Remove("checked");
                if (enum_ischecked)
                {
                    enumRadio.MergeAttribute("checked", "checked");
                }
             
                output.Content.AppendHtml("<div class='form-check form-check-inline '>");
                output.Content.AppendHtml(enumRadio);

                //Getting Label For EnumType
                var enumLabel = Generator.GenerateLabel(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    enumInputLabelName,
                    htmlAttributes: new { @for = enum_id, @class = "form-check-label", 
                        @style = "margin-right: 3px;" });

                
                output.Content.AppendHtml(enumLabel);
                output.Content.AppendHtml("</div>");
                await Task.CompletedTask;
            }
        }
    }
}
