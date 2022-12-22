using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AuditoriumEditing : PageModel
{
    [BindProperty] 
    public string AuditoriumName { get; set; } = "";
    public void OnGet()
    {
    }

    public IActionResult OnPost(ApplicationContext db)
    {
        Auditorium? find = db.Auditoriums.FirstOrDefault(u => u.Name == AuditoriumName);
        if (find != null)
        {
            return Redirect($"/AuditoriumEditingForm/{AuditoriumName}");
        }
        else
        {
            return Redirect($"/NoAuditoriumEdit/{AuditoriumName}");  
        }
        
    }
}