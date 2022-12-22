using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class BuildingDeleting : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost(ApplicationContext db)
    {
        Building? temp = db.Buildings.FirstOrDefault(u => u.Name == Name);
        db.AuditoriumGroups.Where(u => u.BuildingId == temp.Id).Load();
        if (temp != null)
        {
            db.Buildings.Remove(temp);
            db.SaveChanges();
            return Redirect("/BuildingDeletedOk");
        }
        else
        {
            return Redirect("/NoBuildingDelete");
        }
    }
}