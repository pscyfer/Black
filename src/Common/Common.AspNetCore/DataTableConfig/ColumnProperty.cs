namespace Common.AspNetCore.DataTableConfig;
public class ColumnProperty
{
    public ColumnProperty()
    {
        
    }
    public ColumnProperty(string data)
    {
        Data = data;
        Width = "10%";
        //set default values
        Visible = true;
    }
    public string Data { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Width { get; set; }
    public bool AutoWidth { get; set; }
    public string ClassName { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public bool Visible { get; set; }
    public IRender Render { get; set; }
}
