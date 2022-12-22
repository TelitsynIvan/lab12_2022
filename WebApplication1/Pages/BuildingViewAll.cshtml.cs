using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class BuildingViewAll : PageModel
{
    [BindProperty] 
    public List<Building> Buildings { get; set; } = new List<Building>();

    public void OnGet(ApplicationContext db)
    {
        Buildings = db.Buildings.ToList();
        db.AuditoriumGroups.Load();
    }
}