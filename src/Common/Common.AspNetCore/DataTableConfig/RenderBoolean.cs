namespace Common.AspNetCore.DataTableConfig
{
    /// <summary>
    /// Represents Boolean render for DataTables column
    /// </summary>
    public class RenderBoolean : IRender
    {
        public RenderBoolean()
        {
            TrueResult = "<p class=\"text-center text-success\"><i class=\"mdi mdi-checkbox-multiple-marked-circle\"></i></p>";
            FalseResult = "<p class=\"text-center text-danger\"><i class=\"mdi mdi-close-circle-multiple\"></i></p>";
        }
        public RenderBoolean(string trueResult, string falseResult)
        {
            FalseResult = falseResult;
            TrueResult = trueResult;
        }
        public string FalseResult { get; set; }
        public string TrueResult { get; set; }
    }
}