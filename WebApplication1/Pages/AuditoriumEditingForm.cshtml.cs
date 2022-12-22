using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class EditingForm : PageModel
{
    private ApplicationContext context;
    [BindProperty] 
    public Auditorium? Auditorium { get; set; } = new Auditorium() { Name = "", NumberOfSeats = 0, Description = ""};

    public EditingForm(ApplicationContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
      object? data = RouteData.Values["Name"];
      string? name = data.ToString();
      Auditorium = context.Auditoriums.FirstOrDefault(u => u.Name == name);
    }

    public IActionResult OnPost()
    {
        context.Auditoriums.Update(Auditorium);
        context.SaveChanges();
        return Redirect("/AuditoriumEditedOK");
    }
}