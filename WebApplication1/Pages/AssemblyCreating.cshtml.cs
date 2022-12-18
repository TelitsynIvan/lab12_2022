using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
namespace WebApplication1.Pages;

public class AssemblyCreate : PageModel
{
    [BindProperty]
    public Assembly Assembly { get; set; } = new Assembly() {Name = ""};

    public string Name { get; set; } = "";
    public string Message { get; set; } = "";

    public void OnGet(AssemblyContext db)
    {
    }

    public void OnPost(AssemblyContext db)
    {
        Assembly? dublicat = db.Assemblies.FirstOrDefault(u=>u.Name==Name);
        if (dublicat == null)
        {
            db.Assemblies.Add(Assembly);
            db.SaveChanges();
        }
        else
        {
            Message = "Деталь с таким наименованием уже существует";
        }
    }
}