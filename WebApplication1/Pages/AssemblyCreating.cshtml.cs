using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
namespace WebApplication1.Pages;

public class AssemblyCreate : PageModel
{
    [BindProperty]
    public Assembly Assembly { get; set; } = new Assembly() {Name = ""};
    public string Message { get; set; } = "";

    public void OnGet(AssemblyContext db)
    {
    }

    public IActionResult OnPost(AssemblyContext db)
    {
        Assembly? dublicat = db.Assemblies.FirstOrDefault(u=>u.Name==Assembly.Name);
        if (dublicat == null)
        {
            db.Assemblies.Add(Assembly);
            db.SaveChanges();
            return Redirect($"/PartCreating/{Assembly.Name}");
        }
        else
        {
            Message = "Деталь с таким наименованием уже существует";
            return Redirect("/Index");
        }
    }
}