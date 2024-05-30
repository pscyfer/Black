using Microsoft.AspNetCore.Mvc;

namespace Common.AspNetCore.DataTableConfig
{
    public class DataTableModel
    {
        public DataTableModel()
        {
            ButtonHeaders = new();
            InputHeaders = new();
        }
        public string Name { get; set; }
        public string ClassNames { get; set; }
        public List<ColumnProperty> ColumnCollection { get; set; }
        public List<ButtonHeader> ButtonHeaders { get; set; }
        public List<InputHeader> InputHeaders { get; set; }
        public DataUrl UrlRead { get; set; }
        public DataUrl UrlDelete { get; set; }
        public DataUrl UrlUpdate { get; set; }

        public bool EnableExport { get; set; }

        public string Dom { get; set; }

        public string GetInputHeaderIds()
        {
            if (InputHeaders.Count > 0)
            {
                string result = "";
                string operatorCharacter = ",";
                int CountOfCharacter = InputHeaders.Count - 1;
                foreach (var column in InputHeaders)
                {
                    string emptyOrChar = CountOfCharacter > 0 ? operatorCharacter : string.Empty;
                    result += $"#{column.Id}{emptyOrChar}";
                    CountOfCharacter--;
                }

                return result;
            }
            return string.Empty;
        }
    }
}
