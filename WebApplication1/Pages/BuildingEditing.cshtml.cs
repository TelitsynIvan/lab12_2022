using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class BuildingEditing : PageModel
{
    [BindProperty] 
    public string BuildingName { get; set; } = "";
    public void OnGet()
    {
    }

    public IActionResult OnPost(ApplicationContext db)
    {
        Building? find = db.Buildings.FirstOrDefault(u => u.Name == BuildingName);
        if (find != null)
        {
            return Redirect($"/BuildingEditingForm/{BuildingName}");
        }
        else
        {
            return Redirect($"/NoDetailEdit/{BuildingName}");  
        }
        
    }
}