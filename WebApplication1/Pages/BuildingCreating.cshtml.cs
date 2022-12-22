using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
namespace WebApplication1.Pages;

public class BuildingCreate : PageModel
{
    [BindProperty]
    public Building Building { get; set; } = new Building() {Name = ""};
    public string Message { get; set; } = "";

    public void OnGet(ApplicationContext db)
    {
    }

    public IActionResult OnPost(ApplicationContext db)
    {
        Building? dublicat = db.Buildings.FirstOrDefault(u=>u.Name==Building.Name);
        if (dublicat == null)
        {
            db.Buildings.Add(Building);
            db.SaveChanges();
            return Redirect($"/AuditoriumGroupCreating/{Building.Name}");
        }
        else
        {
            Message = "Здание с таким наименованием уже существует";
            return Redirect("/Index");
        }
    }
}