using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AuditoriumDelete : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost(ApplicationContext db)
    {
        Auditorium? temp = db.Auditoriums.FirstOrDefault(u => u.Name == Name);
        if (temp != null)
        {
            db.Auditoriums.Remove(temp);
            db.SaveChanges();
            return Redirect("/AuditoriumDeletedOk");
        }
        else
        {
            return Redirect("/NoAuditoriumDelete");
        }
    }
}