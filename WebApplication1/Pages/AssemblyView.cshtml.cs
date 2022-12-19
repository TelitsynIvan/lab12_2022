using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AssemblyView : PageModel
{
    [BindProperty]
    public string AssemblyName { get; set; } = "";
    public void OnGet()
    {
    }
    public IActionResult OnPostID()
    {
        return Redirect($"/AssemblyViewID/{AssemblyName}");
    }
    public IActionResult OnPostAll()
    {
        return Redirect("/AssemblyViewAll");
    }
}