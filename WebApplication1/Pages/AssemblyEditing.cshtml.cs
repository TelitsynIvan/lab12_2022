using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AssemblyEditing : PageModel
{
    [BindProperty] 
    public string AssemblyName { get; set; } = "";
    public void OnGet()
    {
    }

    public IActionResult OnPost(AssemblyContext db)
    {
        Assembly? find = db.Assemblies.FirstOrDefault(u => u.Name == AssemblyName);
        if (find != null)
        {
            return Redirect($"/AssemblyEditingForm/{AssemblyName}");
        }
        else
        {
            return Redirect($"/NoDetailEdit/{AssemblyName}");  
        }
        
    }
}