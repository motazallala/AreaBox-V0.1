namespace AreaBox_V0._1.Models.Pagination;

public class PageViewModel
{
    public int CurrentPage { get; set; }
    public int PagesCount { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public Dictionary<string, string> paramss { get; set; }
}
