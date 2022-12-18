using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class EditingForm : PageModel
{
    private AssemblyContext context;
    [BindProperty] 
    public Detail? detail { get; set; } = new Detail() { Name = "", Quantity = 0 };

    public EditingForm(AssemblyContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
      object? data = RouteData.Values["Name"];
      string? name = data.ToString();
      detail = context.Details.FirstOrDefault(u => u.Name == name);
    }

    public IActionResult OnPost(AssemblyContext db)
    {
        context.Details.Update(detail);
        context.SaveChanges();
        return Redirect("/DetailEditedOK");
    }
}