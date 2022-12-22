using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class BuildingViewID : PageModel
{
    [BindProperty]
    public Building? Building { get; set; }

    public List<AuditoriumGroup> AuditoriumGroups { get; set; } = new List<AuditoriumGroup>();

    public void OnGet(ApplicationContext db)
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        Building = db.Buildings.FirstOrDefault(u => u.Name == name);
        db.AuditoriumGroups.Where(u=>u.BuildingId==Building.Id).Load();
        AuditoriumGroups = Building.AuditoriumGroups;
    }
}