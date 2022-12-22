using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class BuildingView : PageModel
{
    [BindProperty]
    public string BuildingName { get; set; } = "";
    public void OnGet()
    {
    }
    public IActionResult OnPostID()
    {
        return Redirect($"/BuildingViewID/{BuildingName}");
    }
    public IActionResult OnPostAll()
    {
        return Redirect("/BuildingViewAll");
    }
}