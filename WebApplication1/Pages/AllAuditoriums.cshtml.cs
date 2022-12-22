using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AllAuditoriums : PageModel
{
    public List<Auditorium> DisplayedAuditoriums = new List<Auditorium>();
    public void OnGet(ApplicationContext db)
    {
        DisplayedAuditoriums = db.Auditoriums.ToList();
    }
}