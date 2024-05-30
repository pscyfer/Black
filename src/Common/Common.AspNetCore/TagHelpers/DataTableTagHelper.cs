using Common.AspNetCore.DataTableConfig;
using Common.AspNetCore.RazorService;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Common.AspNetCore.TagHelpers;
[HtmlTargetElement("Dt-Table")]
public class DataTableTagHelper : TagHelper
{
    private readonly IRazorViewService _viewService;
    private readonly ITempDataService _tempDataService;
    public DataTableTagHelper(IRazorViewService viewService, ITempDataService tempDataService)
    {
        _viewService = viewService;
        _tempDataService = tempDataService;
    }

    [HtmlAttributeName("TableModel")]
    public DataTableModel Table { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await Task.CompletedTask;
        var tableName = $"MGT_{Table.Name}";
        var createTable = new TagBuilder("Table");
        createTable.Attributes.Add("Id", tableName);
        createTable.Attributes.Add("class", Table.ClassNames);
        output.TagName = null;
        #region ButtomsAddedToTable

        string htmlButtonResult = "";
        foreach (var buttonHeader in Table.ButtonHeaders)
        {
            if (buttonHeader.IsAjaxRender)
            {
                htmlButtonResult += $"""
                <div class="col-12 col-md-2">
                <button id="{buttonHeader.Name}" class="btn  btn-primary {buttonHeader.ClassName}" type="button" data-toggle="ajax-modal" data-url="{buttonHeader.DataUrl.GetUrl()}"><span> <span class="d-none d-sm-inline-block">{buttonHeader.TextHtml}</span></span></button>
                </div>
                """;
            }
            else
            {
                htmlButtonResult += $"""
                <div class="col-12 col-md-2">
                <a id="{buttonHeader.Name}" class="btn btn-primary {buttonHeader.ClassName}" href="{buttonHeader.DataUrl.GetUrl()}"><span> <span class="d-none d-sm-inline-block">{buttonHeader.TextHtml}</span></span></a>
                </div>
                """;
            }

        }

        string bottomsPlaceHolder = $"""
            <div class="row m-2">{htmlButtonResult}</div>
            """;
        #region InputHeaderAddedToTable
        string inputsResultHtml = string.Empty;
        string SelectListOptionResultHtml = string.Empty;
        foreach (var input in Table.InputHeaders)
        {
            switch (input.InputType)
            {
                case InputType.Text:
                    {
                        inputsResultHtml += $"""
                            <div class="col-4">
                                  <div class="form-floating form-floating-outline">
                              <input type="text" class="form-control {input.ClassNames}" id="{input.Id}" placeholder="{input.PlaceHolder}">
                              <label for="{input.Id}">{input.Title}</label>
                            </div>
                            </div>
                            """;
                        break;
                    }
                case InputType.SelectList:
                    {
                        var getSelectListValue = await _tempDataService.GetSelectListItems(input.TempDataKey);

                        foreach (var item in getSelectListValue)
                        {
                            SelectListOptionResultHtml += $"""
                                <option value="{item.Value}">{item.Text}</option>
                                """;
                        }

                        inputsResultHtml += $"""
                            <div class="col-4">
                                             <div class="form-floating form-floating-outline">
                              <select id="{input.Id}" class="form-select form-select-lg {input.ClassNames}" >
                                <option selected value>انتخاب کنید</option>
                              {SelectListOptionResultHtml}
                              </select>
                              <label for="{input.Id}">{input.Title}</label>
                            </div></div>
                            """;
                        break;
                    }
                default:
                    break;
            }
        }
        string InputsPlaceHolder = $"""
            <hr/>
            <div class="row mt-3 m-2">{inputsResultHtml}</div>
            """;
        #endregion

        #endregion
        var scriptsBuilder = await _viewService.RenderViewToString("_GenerateDataTableScript", Table);
        output.Content.AppendHtml(bottomsPlaceHolder);
        output.Content.AppendHtml(InputsPlaceHolder);
        output.Content.AppendHtml(createTable);
        output.Content.AppendHtml(scriptsBuilder);
    }

}
