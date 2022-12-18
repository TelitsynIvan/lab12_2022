using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class DetailEditing : PageModel
{
    [BindProperty] public string DetailName { get; set; } = "";
    public void OnGet()
    {
    }

    public IActionResult OnPost(AssemblyContext db)
    {
        Detail? find = db.Details.FirstOrDefault(u => u.Name == DetailName);
        if (find != null)
        {
            return Redirect($"/EditingForm/{DetailName}");
        }
        else
        {
            return Redirect("/NoDetailEdit");  
        }
        
    }
}