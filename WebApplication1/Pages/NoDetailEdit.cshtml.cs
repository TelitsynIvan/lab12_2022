using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class ThereNoDetail : PageModel
{
    public void OnGet()
    {
        
    }

    public IActionResult OnPostYes()
    {
        return Redirect("/DetailCreating");
    }
    public IActionResult OnPostNo()
    {
        return Redirect("/Index");
    }
}